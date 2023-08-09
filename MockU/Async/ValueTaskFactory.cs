namespace MockU.Async;

internal sealed class ValueTaskFactory : AwaitableFactory<ValueTask>
{
    public static readonly ValueTaskFactory Instance = new ValueTaskFactory();

    

    

    
    private ValueTaskFactory()
    {
    }

    public override ValueTask CreateCompleted()
    {
        return default;
    }

    public override ValueTask CreateFaulted(Exception exception)
    {
        var tcs = new TaskCompletionSource<object>();
        tcs.SetException(exception);
        return new ValueTask(tcs.Task);
    }

    public override ValueTask CreateFaulted(IEnumerable<Exception> exceptions)
    {
        var tcs = new TaskCompletionSource<object>();
        tcs.SetException(exceptions);
        return new ValueTask(tcs.Task);
    }
}
