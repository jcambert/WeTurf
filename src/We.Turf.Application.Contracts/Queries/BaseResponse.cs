namespace We.Turf.Queries;

public abstract  record BaseResponse
{
    public string ErrorMessage { get; init; } = string.Empty;
}
