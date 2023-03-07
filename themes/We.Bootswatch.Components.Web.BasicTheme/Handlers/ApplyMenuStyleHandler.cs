using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Result;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class ApplyMenuStyleHandler : BaseHandler<IApplyMenuStyleCommand, ApplyMenuStyleResult>
{
    public ApplyMenuStyleHandler(NavigationManager navigationManager)
    {
        this.NavigationManager = navigationManager;
    }

    private NavigationManager NavigationManager { get; }
    public override Task<Result<ApplyMenuStyleResult>> Handle(IApplyMenuStyleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');
            string name = request.Name;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_MENUSTYLE_URL + BootswatchConsts.APPLY_REDIRECT_URL, relativeUrl, name);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Task.FromResult(Result<ApplyMenuStyleResult>.Sucess());
        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result<ApplyMenuStyleResult>.Failure(new(), errors));
        }
    }
}
