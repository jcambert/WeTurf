namespace We.Turf.Handlers;

public class UpdateParameterHandler
    : AbpHandler.With<UpdateParameterQuery, UpdateParameterResponse, Parameter, ParameterDto>
{
    public UpdateParameterHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    { }

    protected override async Task<Result<UpdateParameterResponse>> InternalHandle(
        UpdateParameterQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await Repository.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
            result = await Repository.InsertAsync(
                MapTo(request.Parameter),
                true,
                cancellationToken
            );
        else
        {
            ObjectMapper.Map(request.Parameter, result);
            result = await Repository.UpdateAsync(result, true, cancellationToken);
        }
        return Result.Create<UpdateParameterResponse>(
            new UpdateParameterResponse(MapToDto(result))
        );
    }
}
