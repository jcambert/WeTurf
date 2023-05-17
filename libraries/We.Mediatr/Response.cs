namespace We.Mediatr;

public record Response
{
    public static implicit operator Result<Response>(Response response) => Result.Create(response);
}
