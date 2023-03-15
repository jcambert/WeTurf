using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace We.Bootswatch.Components.Web.BasicTheme;

public interface ISelectorProvider<T>
    where T:INameable
{
    IReadOnlyList<T> GetAll();
    IReadOnlyList<T> GetOthers(T item);
    T GetByName(string name);
    T GetCurrent();

    T GetDefault();
    //void SetCurrent(T item);

    //void SetCurrent(string name);

    void Apply(T item, string uriPath);
}

public abstract class SelectorProvider<T> : ISelectorProvider<T>
    where T : class,INameable
{
    public SelectorProvider(IAbpLazyServiceProvider serviceProvider, IHttpContextAccessor context, NavigationManager navigationManager)
    {
        this.ServiceProvider=serviceProvider;
        this.Context=context;   
        this.NavigationManager=navigationManager;

        Task.Run( InternalInitialization);
    }

    private async Task InternalInitialization()
    {
        OnInitialized();
        await OnInitializedAsync();
    }

    protected IOptions<LayoutOptions> _options => ServiceProvider.LazyGetRequiredService<IOptions<LayoutOptions>>();
    protected IHttpContextAccessor Context { get; }
    protected NavigationManager NavigationManager { get; }
    protected abstract T Default { get; }
    protected abstract List<T> Values { get; }
    protected abstract string CookieName { get; }
    public IAbpLazyServiceProvider ServiceProvider { get; }

    protected virtual Task OnInitializedAsync() => Task.CompletedTask;

    protected virtual void OnInitialized() { }

    public void Apply(T item,string uriPath)
    {
        var relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');

        NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
    }

    public virtual IReadOnlyList<T> GetAll()
        => Values.OrderBy(x => x.Name).DistinctBy(x => x.Name).ToImmutableList();


    public virtual T GetByName(string name)
        => Values.FirstOrDefault(t => t.Name == name) ?? Default;

    public virtual T GetCurrent()
    {

        var httpContext = Context.HttpContext;
        if (!(httpContext?.Request.Cookies.TryGetValue(CookieName, out var _name) ?? false))
        {
            return Default;
        }
        return GetByName(_name);
    }


    public  IReadOnlyList<T> GetOthers(T item)
     => Values?.Where(t => t.Name != item?.Name).OrderBy(t => t.Name).DistinctBy(t => t.Name).ToImmutableList() ??new List<T>().ToImmutableList();

    public T GetDefault() => Default;
    /*public virtual void SetCurrent(T item)
    {
        var options = new CookieOptions()
        {
            Expires = DateTime.UtcNow.AddYears(10)
        };
        var httpContext = Context.HttpContext;
        httpContext.Response.Cookies.Append(CookieName, item.Name, options);
    }
    public virtual void SetCurrent(string name)
        => SetCurrent(GetByName(name));*/
}