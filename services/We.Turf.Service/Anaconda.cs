﻿using System;

namespace We.Turf.Service;
public interface IAnacondaCommand:ICommand { }
public interface IAnacondaActivation : IAnacondaCommand
{
    
}
public abstract class AnacondaCommand : BaseCommand, IAnacondaCommand
{

    protected IAnaconda Conda { get; init; }
    protected AnacondaCommand(IServiceProvider serviceProvider):base(serviceProvider) =>
        ( Conda) = (serviceProvider.GetRequiredService<IAnaconda>());

}
public class AnacondaActivation : AnacondaCommand, IAnacondaActivation
{

    public AnacondaActivation(IServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override IEnumerable<string> Commands()
    {
        yield return $@"{Conda.BasePath}\Scripts\activate.bat";
        yield return $"activate {Conda.EnvironmentName}";
    }
    
}
public abstract class AnacondaExecuteStript : AnacondaCommand { 
    public string ScriptName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Arguments { get; set; } = string.Empty;

    public AnacondaExecuteStript(IServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override IEnumerable<string> Commands()
    {
        yield return $@"{Conda.BasePath}\python.exe {ScriptArguments()}";
    }
    

    protected virtual string ScriptArguments() => $@"{Path}\{ScriptName}.py {Arguments}";
}
public interface IAnaconda
{
    string BasePath { get; }
    string EnvironmentName { get; }
}
public class Anaconda : IAnaconda
{
    public Anaconda(DriveService drive)
    {
        this.Drive=drive.ExecutingDriveLetter;
        BasePath = $@"{Drive}anaconda3";
    }
    public string EnvironmentName { get; set; } = "base";
    public string BasePath { get; set; } 
    protected string Drive { get;  }
}
