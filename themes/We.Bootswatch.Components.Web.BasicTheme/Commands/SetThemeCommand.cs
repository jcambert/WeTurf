namespace We.Bootswatch.Components.Web.BasicTheme.Commands;

public interface ISetThemeCommand : ICommand<SetThemeCommandResult>
{
    string Name { get; init; }
}
public class SetThemeCommand: ISetThemeCommand
{
    public SetThemeCommand(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
}

public sealed record SetThemeCommandResult();
