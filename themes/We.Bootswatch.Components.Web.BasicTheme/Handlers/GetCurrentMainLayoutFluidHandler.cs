using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class GetCurrentMainLayoutFluidHandler : AbpHandler.With<GetCurrentMainLayoutFluidCommand, GetCurrentMainLayoutFluidResult>
{

    private IFluidProvider FluidProvider => GetRequiredService<IFluidProvider>();
    public GetCurrentMainLayoutFluidHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
#if MEDIATOR
    public override ValueTask<Result<GetCurrentMainLayoutFluidResult>> Handle(GetCurrentMainLayoutFluidCommand request, CancellationToken cancellationToken)
#else
    public override Task<Result<GetCurrentMainLayoutFluidResult>> Handle(GetCurrentMainLayoutFluidCommand request, CancellationToken cancellationToken)
#endif
    {
        //FOR TEST
        //return Result.Failure< GetCurrentMainLayoutFluidResult>(new Error("cannot get if it is Fluid or not"));
        return Result.Success(new GetCurrentMainLayoutFluidResult(FluidProvider.GetCurrent()));
    }
}
