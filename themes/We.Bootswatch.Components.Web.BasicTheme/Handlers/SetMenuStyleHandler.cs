using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class SetMenuStyleHandler : BaseHandler<SetMenuStyleCommand, SetMenuStyleResult>
{
    private IHttpContextAccessor Context { get; }
    private string CookieName => BootswatchConsts.MenuStyleCookie;
    public SetMenuStyleHandler(IHttpContextAccessor context)
    {
        this.Context = context;
    }
    public override Task<Result<SetMenuStyleResult>> Handle(SetMenuStyleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddYears(10)
            };
            var httpContext = Context.HttpContext;
            httpContext.Response.Cookies.Append(CookieName, request.Style, options);
            return Task.FromResult(Result.Sucess<SetMenuStyleResult>());

        }
        catch (Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(nameof(SetThemeHandler), ex.Message));
            return Task.FromResult(Result.Failure<SetMenuStyleResult>( errors.ToArray()));
        }
    }
}
