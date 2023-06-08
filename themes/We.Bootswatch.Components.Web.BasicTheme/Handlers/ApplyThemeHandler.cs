using Microsoft.AspNetCore.Components;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class ApplyThemeHandler : AbpHandler.With<ApplyThemeCommand, ApplyThemeResult>
{
    // public NavigationManager NavigationManager { get; }

    public ApplyThemeHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    private NavigationManager NavigationManager => GetRequiredService<NavigationManager>();

    protected override Task<Result<ApplyThemeResult>> InternalHandle(
        ApplyThemeCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            string relativeUrl = NavigationManager.Uri
                .RemovePreFix(NavigationManager.BaseUri)
                .EnsureStartsWith('/')
                .EnsureStartsWith('~');
            string name = request.Name; //DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_THEME_URL, relativeUrl, name);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Result.Success<ApplyThemeResult>();
        }
        catch (Exception ex)
        {
            return Result.Failure<ApplyThemeResult>(ex);
        }
    }
}
