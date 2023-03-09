using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Result;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

internal class ApplyThemeHandler : BaseHandler<ApplyThemeCommand, ApplyThemeResult>
{
    public ApplyThemeHandler(NavigationManager navigationManager)
    {
        this.NavigationManager = navigationManager;
    }

    private NavigationManager NavigationManager { get; }

    public override Task<Result<ApplyThemeResult>> Handle(ApplyThemeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/');//.EnsureStartsWith('~');
            string name = request.Name;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_THEME_URL, relativeUrl, name);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Task.FromResult(Result<ApplyThemeResult>.Sucess());
        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result<ApplyThemeResult>.Failure(new(), errors));
        }
    }
}
