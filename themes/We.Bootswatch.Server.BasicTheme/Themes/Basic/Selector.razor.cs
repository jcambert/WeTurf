using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public  partial class Selector<TProvider, TItem>:ComponentBase
    where TProvider : ISelectorProvider<TItem>
    where TItem : class,INameable
{
    protected TItem Current;
    protected IReadOnlyCollection<TItem> Others;

    protected TProvider SelectorProvider => ServiceProvider.LazyGetRequiredService<TProvider>();
    [Parameter]
    public Action<TProvider, TItem> OnChange { get; set; }
    


    protected override void OnInitialized()
    {
        base.OnInitialized();
        Current = SelectorProvider.GetCurrent();
        Others = SelectorProvider.GetOthers(Current);
    }
}
