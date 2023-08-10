namespace MockU.Async;

internal sealed class TaskFactory : AwaitableFactory<Task>
{
    public static readonly TaskFactory Instance = new();

    private TaskFactory()
    {
    }

    public override Task CreateCompleted()
    {
        return Task.FromResult<object?>(default);
    }

    public override Task CreateFaulted(Exception exception)
    {
        var tcs = new TaskCompletionSource<object>();
        tcs.SetException(exception);
        return tcs.Task;
    }

    public override Task CreateFaulted(IEnumerable<Exception> exceptions)
    {
        var tcs = new TaskCompletionSource<object>();
        tcs.SetException(exceptions);
        return tcs.Task;
    }
}
