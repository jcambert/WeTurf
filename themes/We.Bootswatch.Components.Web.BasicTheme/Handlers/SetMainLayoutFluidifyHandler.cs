using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class SetMainLayoutFluidifyHandler
    : AbpHandler.With<SetMainLayoutFluidifyCommand, SetMainLayoutFluidifyResult>
{
    public SetMainLayoutFluidifyHandler(
        IAbpLazyServiceProvider serviceProvider,
        IHttpContextAccessor context
    ) : base(serviceProvider)
    {
        this.Context = context;
    }

    private string CookieName => BootswatchConsts.FluidCookie;
    private IHttpContextAccessor Context { get; init; }
    private IFluidProvider FluidProvider => GetRequiredService<IFluidProvider>();
#if MEDIATOR
    public override ValueTask<Result<SetMainLayoutFluidifyResult>> Handle(
        SetMainLayoutFluidifyCommand request,
        CancellationToken cancellationToken
    )
#else
    public override Task<Result<SetMainLayoutFluidifyResult>> Handle(
        SetMainLayoutFluidifyCommand request,
        CancellationToken cancellationToken
    )
#endif
    {
        try
        {
            var f =
                FluidProvider.GetAll().Where(x => x.Name == request.IsFluid).FirstOrDefault()
                ?? FluidProvider.GetDefault();
            if (f is null)
                return Result.Failure<SetMainLayoutFluidifyResult>(
                    new Error("Cannot access to fluid provider")
                );
            var options = new CookieOptions() { Expires = DateTime.UtcNow.AddYears(10) };
            var httpContext = Context.HttpContext;

            httpContext?.Response.Cookies.Append(CookieName, f.Name, options);

            return Result.Success(new SetMainLayoutFluidifyResult(f));
        }
        catch (Exception ex)
        {
            return Result.Failure<SetMainLayoutFluidifyResult>(ex);
        }
    }
}
