using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Result;
using Microsoft.AspNetCore.Http;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class SetMenuStyleHandler : BaseHandler<ISetMenuStyleCommand, SetMenuStyleResult>
{
    private IHttpContextAccessor Context { get; }
    private string CookieName => BootswatchConsts.MenuStyleCookie;
    public SetMenuStyleHandler(IHttpContextAccessor context)
    {
        this.Context = context;
    }
    public override Task<Result<SetMenuStyleResult>> Handle(ISetMenuStyleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddYears(10)
            };
            var httpContext = Context.HttpContext;
            httpContext.Response.Cookies.Append(CookieName, request.Style, options);
            return Task.FromResult(Result<SetMenuStyleResult>.Sucess());

        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result<SetMenuStyleResult>.Failure(new(), errors));
        }
    }
}
