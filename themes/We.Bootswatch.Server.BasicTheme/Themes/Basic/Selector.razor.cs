using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
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
    public Func<IAbpLazyServiceProvider,IMediator, TProvider, TItem,Task> OnChange { get; set; }
    


    protected override void OnInitialized()
    {
        base.OnInitialized();
        Current = SelectorProvider.GetCurrent();
        Others = SelectorProvider.GetOthers(Current);
    }
}
