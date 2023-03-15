using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using We.Bootswatch.Components.Web.BasicTheme.Commands;
using We.Bootswatch.Components.Web.BasicTheme.Services;
using We.Results;

namespace We.Bootswatch.Components.Web.BasicTheme.Handlers;

public class GetCurrentMainLayoutFluidHandler : BaseHandler<GetCurrentMainLayoutFluidCommand, GetCurrentMainLayoutFluidResult>
{
    private IAbpLazyServiceProvider ServiceProvider { get; }
    private IFluidProvider fluidProvider => ServiceProvider.LazyGetRequiredService<IFluidProvider>();
    public GetCurrentMainLayoutFluidHandler(IAbpLazyServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }
    public override Task<Result<GetCurrentMainLayoutFluidResult>> Handle(GetCurrentMainLayoutFluidCommand request, CancellationToken cancellationToken)
    {
        
        return Result.Sucess(new GetCurrentMainLayoutFluidResult(fluidProvider.GetCurrent()));
    }
}
