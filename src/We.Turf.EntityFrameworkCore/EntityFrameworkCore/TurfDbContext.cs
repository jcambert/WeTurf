using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using We.Turf.Entities;

namespace We.Turf.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TurfDbContext :
    AbpDbContext<TurfDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */
    public DbSet<Predicted> Predicteds{ get; set; }
    public DbSet<Resultat> Resultats{ get; set; }
    public DbSet<ResultatOfPredicted> ResultatOfPredicted { get; set; }
    public DbSet<PredictionPerClassifier> PredictionPerClassifier { get; set; }
    public DbSet<ResultatPerClassifier> ResultatPerClassifier { get; set; }
    public DbSet<ScrapTrigger> Triggers { get; set; }

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public TurfDbContext(DbContextOptions<TurfDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(TurfConsts.DbTablePrefix + "YourEntities", TurfConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Predicted>(b =>
        {
            b.ToTable(TurfConsts.DbTablePrefix + nameof(Predicted).ToLower(), TurfConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasIndex(x => x.Date);
            b.HasIndex(x => x.Hippodrome);
            b.HasIndex(x => x.Reunion);
            b.HasIndex(x => x.Course);
            b.HasIndex(x => x.Classifier);
            b.Property(x=>x.Date).HasConversion<DateOnlyConverter,DateOnlyComparer>();
            //...
        });

        builder.Entity<Resultat>(b =>{
            b.ToTable(TurfConsts.DbTablePrefix + nameof(Resultat).ToLower(), TurfConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Date);
            b.HasIndex(x => x.Reunion);
            b.HasIndex(x => x.Course);
            b.Property(x => x.Date).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });
        builder.Entity<ResultatOfPredicted>(b => {
            b.HasNoKey();
            b.ToView("turfresultatofpredicted");
            b.Property(x=>x.Resultat_Id).HasColumnName("resultat_id");
            b.Property(x => x.Dividende).HasColumnName("dividende");
            b.Property(x=>x.Date).HasConversion<DateOnlyConverter, DateOnlyComparer>();
        });
        builder.Entity<PredictionPerClassifier>(b =>
        {
            b.HasNoKey();
            b.ToView("turfpredictionperclassifier");
            b.Property(x => x.Counting).HasColumnName("counting");
        });
        builder.Entity<ResultatPerClassifier>(b =>
        {
            b.HasNoKey();
            b.ToView("turfresultatperclassifier");
            b.Property(x => x.Counting).HasColumnName("counting");
        });

        builder.Entity<ScrapTrigger>(b =>
        {
            b.ToTable(TurfConsts.DbTablePrefix + nameof(ScrapTrigger).ToLower(), TurfConsts.DbSchema);
            b.Property(x=>x.Start).HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            b.HasIndex(x => x.Start);
        });
    }
}
