using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

internal class SetMainLayoutFluidifyHandler : BaseHandler<SetMainLayoutFluidifyCommand, SetMainLayoutFluidifyResult>
{
    private string CookieName => BootswatchConsts.FluidCookie;
    private IHttpContextAccessor Context { get; init; }
    private IAbpLazyServiceProvider ServiceProvider { get; }
    private IFluidProvider FluidProvider=>ServiceProvider.LazyGetRequiredService<IFluidProvider>();
    public SetMainLayoutFluidifyHandler(IAbpLazyServiceProvider serviceProvider, IHttpContextAccessor context)
    {

        this.Context = context;
        this.ServiceProvider = serviceProvider;
    }
    public override Task<Result<SetMainLayoutFluidifyResult>> Handle(SetMainLayoutFluidifyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var f=FluidProvider.GetAll().Where(x => x.Name   == request.IsFluid).FirstOrDefault() ?? FluidProvider.GetDefault();
            var options = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddYears(10)
            };
            var httpContext = Context.HttpContext;
            httpContext.Response.Cookies.Append(CookieName, f.Name, options);

            return Result.Sucess(new SetMainLayoutFluidifyResult(f));

        }
        catch (Exception ex)
        {
            return Result.Failure<SetMainLayoutFluidifyResult>(ex);
        }
    }
}


        