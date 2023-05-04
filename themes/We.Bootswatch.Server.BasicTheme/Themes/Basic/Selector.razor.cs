#if MEDIATOR
#endif
#if MEDIATR
using MediatR;
#endif
using Microsoft.AspNetCore.Components;
using We.Bootswatch.Components.Web.BasicTheme;

namespace We.Bootswatch.Server.BasicTheme.Themes.Basic;

public partial class Selector<TProvider, TItem> : ComponentBase
    where TProvider : ISelectorProvider<TItem>
    where TItem : class, INameable
{
    protected TItem? Current;
    protected IReadOnlyCollection<TItem>? Others;

    [Inject]
    IAbpLazyServiceProvider? ServiceProvider { get; set; }

    [Inject]
    NavigationManager? NavigationManager { get; set; }

    [Inject]
    IMediator? Mediator { get; set; }
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
    protected TProvider SelectorProvider => ServiceProvider.LazyGetRequiredService<TProvider>();
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.

    [Parameter]
    public Func<IAbpLazyServiceProvider, IMediator, TProvider, TItem, Task>? OnChange { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Current = SelectorProvider.GetCurrent();
        Others = SelectorProvider.GetOthers(Current);
    }

    protected void OnSelectorClicked(TItem item)
    {
        if (OnChange is not null && Mediator is not null)
            OnChange(Service, Mediator, SelectorProvider, item);
    }
}
