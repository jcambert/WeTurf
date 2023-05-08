using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We.AbpExtensions;
using We.Results;
using We.Turf.Entities;

namespace We.Turf.Handlers;

public class UpdateParameterHandler : AbpHandler.With<UpdateParameterQuery, UpdateParameterResponse, Parameter, ParameterDto>
{
    public UpdateParameterHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
#if MEDIATR
    public override async Task<Result<UpdateParameterResponse>> Handle(UpdateParameterQuery request, CancellationToken cancellationToken)
#endif
#if MEDIATOR
    public override async ValueTask<Result<UpdateParameterResponse>> Handle(UpdateParameterQuery request, CancellationToken cancellationToken)
#endif
    {
        var result = await Repository.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
            result = await Repository.InsertAsync(MapTo(request.Parameter),true,cancellationToken);
        else
        {
            ObjectMapper.Map(request.Parameter, result);
            result = await Repository.UpdateAsync(result,true, cancellationToken);
        }
        return Result.Create<UpdateParameterResponse>(new UpdateParameterResponse( MapToDto(result) ));
    }
}
