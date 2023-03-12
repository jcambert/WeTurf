using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class ApplyLanguageHandler : BaseHandler<ApplyLanguageCommand, ApplyLanguageResult>
{
    public ApplyLanguageHandler(NavigationManager navigationManager)
    {
        this.NavigationManager = navigationManager;
    }

    private NavigationManager NavigationManager { get; }
    public override Task<Result<ApplyLanguageResult>> Handle(ApplyLanguageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');
            string cultureName = request.CultureName;//DOT NOT CHANGE THIS
            string uiCultureName = request.UiCultureName;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_LANGUAGE_URL, relativeUrl, cultureName, uiCultureName);
            //NavigationManager.NavigateTo($"{uriPath}&returnUrl={relativeUrl}", forceLoad: true);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Task.FromResult(Result.Sucess<ApplyLanguageResult>());
        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result.Failure<ApplyLanguageResult>( errors.ToArray()));
        }
    }
}
