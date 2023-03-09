using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Result;

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
            return Task.FromResult(Result<SetThemeCommandResult>.Sucess());

        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result<SetThemeCommandResult>.Failure(new(), errors));
        }
    }
}
