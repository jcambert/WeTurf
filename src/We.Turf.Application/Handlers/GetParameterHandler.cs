namespace We.Turf.Handlers;

public class GetParameterHandler
    : AbpHandler.With<GetParameterQuery, GetParameterResponse, Parameter, ParameterDto>
{
    public GetParameterHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider) { }

    protected override async Task<Result<GetParameterResponse>> InternalHandle(
        GetParameterQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await Repository.FirstOrDefaultAsync(cancellationToken);
        if (result == null)
        {
            return NotFound($"{nameof(Parameter)} didn't exists");
        }
        return new GetParameterResponse(MapToDto(result));
    }
}
