namespace We.Mediatr;

public static class MediatorExtensions
{
#if MEDIATOR
    public static Task<TResult> AsTaskWrap<TResult>(this ValueTask<TResult> result) =>
        result.AsTask();
#endif
#if MEDIATR
    public static Task<TResult> AsTaskWrap<TResult>(this Task<TResult> result) => result;
#endif
}
