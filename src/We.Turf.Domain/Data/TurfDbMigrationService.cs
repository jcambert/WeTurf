using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace We.Turf.Data;

public class TurfDbMigrationService : ITransientDependency
{
    public ILogger<TurfDbMigrationService> Logger { get; set; }

    private readonly IDataSeeder _dataSeeder;
    private readonly IEnumerable<ITurfDbSchemaMigrator> _dbSchemaMigrators;
    private readonly ITenantRepository _tenantRepository;
    private readonly ICurrentTenant _currentTenant;

    public TurfDbMigrationService(
        IDataSeeder dataSeeder,
        IEnumerable<ITurfDbSchemaMigrator> dbSchemaMigrators,
        ITenantRepository tenantRepository,
        ICurrentTenant currentTenant
    )
    {
        _dataSeeder = dataSeeder;
        _dbSchemaMigrators = dbSchemaMigrators;
        _tenantRepository = tenantRepository;
        _currentTenant = currentTenant;

        Logger = NullLogger<TurfDbMigrationService>.Instance;
    }

    public async Task MigrateAsync()
    {
        var initialMigrationAdded = AddInitialMigrationIfNotExist();

        if (initialMigrationAdded)
        {
            return;
        }

        Logger.LogInformation("Started database migrations...");

        await MigrateDatabaseSchemaAsync();
        await SeedDataAsync();

        Logger.LogInformation($"Successfully completed host database migrations.");

        var tenants = await _tenantRepository.GetListAsync(includeDetails: true);

        var migratedDatabaseSchemas = new HashSet<string>();
        foreach (var tenant in tenants)
        {
            using (_currentTenant.Change(tenant.Id))
            {
                if (tenant.ConnectionStrings.Any())
                {
                    var tenantConnectionStrings = tenant.ConnectionStrings
                        .Select(x => x.Value)
                        .ToList();

                    if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                    {
                        await MigrateDatabaseSchemaAsync(tenant);

                        migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                    }
                }

                await SeedDataAsync(tenant);
            }

            Logger.LogInformation(
                "Successfully completed {Name} tenant database migrations.",
                tenant.Name
            );
        }

        Logger.LogInformation("Successfully completed all database migrations.");
        Logger.LogInformation("You can safely end this process...");
    }

    private async Task MigrateDatabaseSchemaAsync(Tenant? tenant = null)
    {
        Logger.LogInformation(
            "Migrating schema for {Name} database...",
            (tenant == null ? "host" : tenant.Name + " tenant")
        );

        foreach (var migrator in _dbSchemaMigrators)
        {
            await migrator.MigrateAsync();
        }
    }

    private async Task SeedDataAsync(Tenant? tenant = null)
    {
        Logger.LogInformation(
            "Executing {Name} database seed...",
            (tenant == null ? "host" : tenant.Name + " tenant")
        );

        await _dataSeeder.SeedAsync(
            new DataSeedContext(tenant?.Id)
                .WithProperty(
                    IdentityDataSeedContributor.AdminEmailPropertyName,
                    IdentityDataSeedContributor.AdminEmailDefaultValue
                )
                .WithProperty(
                    IdentityDataSeedContributor.AdminPasswordPropertyName,
                    IdentityDataSeedContributor.AdminPasswordDefaultValue
                )
        );
    }

    private bool AddInitialMigrationIfNotExist()
    {
        try
        {
            if (!DbMigrationsProjectExists())
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }

        try
        {
            if (!MigrationsFolderExists())
            {
                AddInitialMigration();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Logger.LogWarning(
                "Couldn't determinate if any migrations exist : {Message} ",
                e.Message
            );
            return false;
        }
    }

    private static bool DbMigrationsProjectExists()
    {
        var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();

        return dbMigrationsProjectFolder != null;
    }

    private static bool MigrationsFolderExists()
    {
        string? dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();
        if (dbMigrationsProjectFolder is null)
            return false;
        return Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
    }

    private void AddInitialMigration()
    {
        Logger.LogInformation("Creating initial migration...");

        string argumentPrefix;
        string fileName;

        if (
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
            || RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
        )
        {
            argumentPrefix = "-c";
            fileName = "/bin/bash";
        }
        else
        {
            argumentPrefix = "/C";
            fileName = "cmd.exe";
        }

        var procStartInfo = new ProcessStartInfo(
            fileName,
            $"{argumentPrefix} \"abp create-migration-and-run-migrator \"{GetEntityFrameworkCoreProjectFolderPath()}\"\""
        );

        try
        {
            Process.Start(procStartInfo);
        }
        catch (Exception)
        {
            throw new Exception("Couldn't run ABP CLI...");
        }
    }

    private static string? GetEntityFrameworkCoreProjectFolderPath()
    {
        var slnDirectoryPath =
            GetSolutionDirectoryPath() ?? throw new Exception("Solution folder not found!");
        var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

        return Directory
            .GetDirectories(srcDirectoryPath)
            .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
    }

    private static string? GetSolutionDirectoryPath()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        if (currentDirectory is null)
            return null;
        while (Directory.GetParent(currentDirectory?.FullName ?? string.Empty) != null)
        {
            currentDirectory = Directory.GetParent(currentDirectory?.FullName ?? string.Empty);

            if (
                currentDirectory is not null
                && Directory
                    .GetFiles(currentDirectory.FullName)
                    .FirstOrDefault(f => f.EndsWith(".sln")) != null
            )
            {
                return currentDirectory.FullName;
            }
        }

        return null;
    }
}
