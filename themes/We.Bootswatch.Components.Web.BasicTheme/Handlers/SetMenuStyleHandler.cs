using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class SetMenuStyleHandler : AbpHandler.With<SetMenuStyleCommand, SetMenuStyleResult>
{
    public SetMenuStyleHandler(IAbpLazyServiceProvider serviceProvider, IHttpContextAccessor context) : base(serviceProvider)
    {
        Context = context;
    }

    private IHttpContextAccessor Context { get; }
    private string CookieName => BootswatchConsts.MenuStyleCookie;

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
            return Result.Sucess<SetMenuStyleResult>();

        }
        catch (Exception ex)
        {
            return Result.Failure<SetMenuStyleResult>( ex);
        }
    }
}
