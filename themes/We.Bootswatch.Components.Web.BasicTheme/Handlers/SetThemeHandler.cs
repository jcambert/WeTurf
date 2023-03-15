using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class SetThemeHandler : BaseHandler<SetThemeCommand, SetThemeCommandResult>
{
    private IHttpContextAccessor Context { get; }
    private string CookieName => BootswatchConsts.ThemeCookie;
    public SetThemeHandler(IHttpContextAccessor context)
    {
        this.Context = context;
    }


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
            return Result.Sucess<SetThemeCommandResult>();

        }
        catch (Exception ex)
        {
            return Result.Failure<SetThemeCommandResult>( ex);
        }
    }
}
