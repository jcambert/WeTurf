using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

internal class ApplyThemeHandler : AbpHandler.With<ApplyThemeCommand, ApplyThemeResult>
{
    public ApplyThemeHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private NavigationManager NavigationManager => GetRequiredService<NavigationManager>();

    public override Task<Result<ApplyThemeResult>> Handle(ApplyThemeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');
            string name = request.Name;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_THEME_URL, relativeUrl, name);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Result.Sucess<ApplyThemeResult>();
        }
        catch (Exception ex)
        {
            return Result.Failure<ApplyThemeResult>( ex);
        }
    }
}
