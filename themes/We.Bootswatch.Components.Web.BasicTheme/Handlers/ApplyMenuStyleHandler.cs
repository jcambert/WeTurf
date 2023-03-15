using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class ApplyMenuStyleHandler : BaseHandler<ApplyMenuStyleCommand, ApplyMenuStyleResult>
{
    public ApplyMenuStyleHandler(NavigationManager navigationManager)
    {
        this.NavigationManager = navigationManager;
    }

    private NavigationManager NavigationManager { get; }
    public override Task<Result<ApplyMenuStyleResult>> Handle(ApplyMenuStyleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');
            string name = request.Name;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_MENUSTYLE_URL , relativeUrl, name);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Result.Sucess<ApplyMenuStyleResult>();
        }
        catch (Exception ex)
        {
            return Result.Failure<ApplyMenuStyleResult>( ex);
        }
    }
}
