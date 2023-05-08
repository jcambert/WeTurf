using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class GetParameterHandler : AbpHandler.With<GetParameterQuery, GetParameterResponse, Parameter, ParameterDto>
{
    public GetParameterHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

#if MEDIATOR

    public override async ValueTask<Result<GetParameterResponse>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
   
#endif

#if MEDIATR

    public override async Task<Result<GetParameterResponse>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
#endif
    {
        var result = await Repository.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
        {
            return NotFound($"{nameof(Parameter)} didn't exists");
        }
        return new GetParameterResponse(MapToDto(result));
    }

}
