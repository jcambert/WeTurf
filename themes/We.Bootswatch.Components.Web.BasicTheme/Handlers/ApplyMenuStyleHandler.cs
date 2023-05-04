using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class ApplyMenuStyleHandler : AbpHandler.With<ApplyMenuStyleCommand, ApplyMenuStyleResult>
{
    public ApplyMenuStyleHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    private NavigationManager NavigationManager => GetRequiredService<NavigationManager>();
#if MEDIATOR
    public override ValueTask<Result<ApplyMenuStyleResult>> Handle(
        ApplyMenuStyleCommand request,
        CancellationToken cancellationToken
    )
#else
    public override Task<Result<ApplyMenuStyleResult>> Handle(
        ApplyMenuStyleCommand request,
        CancellationToken cancellationToken
    )
#endif
    {
        try
        {
            string relativeUrl = NavigationManager.Uri
                .RemovePreFix(NavigationManager.BaseUri)
                .EnsureStartsWith('/')
                .EnsureStartsWith('~');
            string name = request.Name; //DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_MENUSTYLE_URL, relativeUrl, name);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Result.Success<ApplyMenuStyleResult>();
        }
        catch (Exception ex)
        {
            return Result.Failure<ApplyMenuStyleResult>(ex);
        }
    }
}
