using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace We.Bootswatch.Components.Web.BasicTheme;

public enum WeMenuStyle
{
    LeftSide,
    TopSide,
    RightSide, 
    BottomSide,
}

/*
public class WeMenuStyleOptions
{
    public WeMenuStyle DefaultStyle { get; set; }
    public bool ResetStyle { get; set; }
}

public interface IWeMenuStyleManager
{
    WeMenuStyle DefaultStyle { get; }
    void ChangeStyle(WeMenuStyle style);
}
public class WeMenuStyleManager: IWeMenuStyleManager
{
    public WeMenuStyleManager(IOptions<WeMenuStyleOptions> options)
    {
        this.Options = options.Value;
    }
    private WeMenuStyleOptions Options { get; set; }

    public WeMenuStyle DefaultStyle => Options.DefaultStyle;

    public void ChangeStyle(WeMenuStyle style)
    {
        throw new System.NotImplementedException();
    }
}*/