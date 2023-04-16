using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class SetThemeHandler : AbpHandler.With<SetThemeCommand, SetThemeCommandResult>
{
    public SetThemeHandler(IAbpLazyServiceProvider serviceProvider,IHttpContextAccessor context) : base(serviceProvider)
    {
        Context = context;
    }

    private IHttpContextAccessor Context { get; init; }
    private string CookieName => BootswatchConsts.ThemeCookie;



    public override Task<Result<SetThemeCommandResult>> Handle(SetThemeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddYears(10)
            };
            var httpContext = Context.HttpContext;
            httpContext.Response.Cookies.Append(CookieName, request.Name, options);
            return Result.Success<SetThemeCommandResult>();

        }
        catch (Exception ex)
        {
            return Result.Failure<SetThemeCommandResult>( ex);
        }
    }
}
