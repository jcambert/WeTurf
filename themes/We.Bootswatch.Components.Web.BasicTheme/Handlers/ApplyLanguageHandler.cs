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

public class ApplyLanguageHandler : BaseHandler<IApplyLanguageCommand, ApplyLanguageResult>
{
    public ApplyLanguageHandler(NavigationManager navigationManager)
    {
        this.NavigationManager = navigationManager;
    }

    private NavigationManager NavigationManager { get; }
    public override Task<Result<ApplyLanguageResult>> Handle(IApplyLanguageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');
            string cultureName = request.CultureName;//DOT NOT CHANGE THIS
            string uiCultureName = request.UiCultureName;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_LANGUAGE_URL + BootswatchConsts.APPLY_REDIRECT_URL, relativeUrl, cultureName,uiCultureName);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Task.FromResult(Result<ApplyLanguageResult>.Sucess());
        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result<ApplyLanguageResult>.Failure(new(), errors));
        }
    }
}
