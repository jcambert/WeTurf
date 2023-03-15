using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class ApplyMainLayoutFluidifyHandler : BaseHandler<ApplyMainLayoutFluidifyCommand, ApplyMainLayoutFluidifyResult>
{

    private NavigationManager NavigationManager { get; }
    public ApplyMainLayoutFluidifyHandler(NavigationManager navigationManager)
    {
        this.NavigationManager = navigationManager;
    }

    public override Task<Result<ApplyMainLayoutFluidifyResult>> Handle(ApplyMainLayoutFluidifyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            string relativeUrl = NavigationManager.Uri.RemovePreFix(NavigationManager.BaseUri).EnsureStartsWith('/').EnsureStartsWith('~');
            //string name = request.Value;//DOT NOT CHANGE THIS
            var uri = string.Format(BootswatchConsts.APPLY_MAINLAYOUT_FLUID_URL, relativeUrl, request.Value.Name);
            NavigationManager.NavigateTo(uri, forceLoad: true);
            return Result.Sucess<ApplyMainLayoutFluidifyResult>();
        }
        catch (Exception ex)
        {
            return Result.Failure<ApplyMainLayoutFluidifyResult>(ex);
        }
    }
}

