using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace We.Turf.Service;

public class PwShellPmuWorker : BackgroundService
{
    private IServiceProvider ServiceProvider { get; init; }
    private readonly Runspace runspace;
    private readonly PowerShell PS;
    public PwShellPmuWorker(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
        runspace = RunspaceFactory.CreateRunspace();
        runspace.Open();
        PS=PowerShell.Create(runspace);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (Pipeline pipeline = runspace.CreatePipeline())
        {
            pipeline.Commands.Add(@".\full-pipeline.ps1");
            pipeline.Invoke();
        }
        await Task.Delay(500);

        
    }
}
