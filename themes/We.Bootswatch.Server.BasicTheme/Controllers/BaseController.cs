using MediatR;
using Volo.Abp.AspNetCore.Mvc;

namespace We.Bootswatch.Server.BasicTheme.Controllers;


public abstract class BaseController : AbpControllerBase
{
    protected  IMediator Mediator { get; init; }
    public BaseController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

}
