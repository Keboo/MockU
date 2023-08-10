using System.Linq.Expressions;

namespace MockU.Async;

internal sealed class ValueTaskFactory<TResult> : AwaitableFactory<ValueTask<TResult>, TResult>
{
    public override ValueTask<TResult> CreateCompleted(TResult result)
    {
        return new ValueTask<TResult>(result);
    }

    public override ValueTask<TResult> CreateFaulted(Exception exception)
    {
        var tcs = new TaskCompletionSource<TResult>();
        tcs.SetException(exception);
        return new ValueTask<TResult>(tcs.Task);
    }

    public override ValueTask<TResult> CreateFaulted(IEnumerable<Exception> exceptions)
    {
        var tcs = new TaskCompletionSource<TResult>();
        tcs.SetException(exceptions);
        return new ValueTask<TResult>(tcs.Task);
    }

    public override Expression CreateResultExpression(Expression awaitableExpression)
    {
        return Expression.MakeMemberAccess(
            awaitableExpression,
            typeof(ValueTask<TResult>).GetProperty(nameof(ValueTask<TResult>.Result))!);
    }

    public override bool TryGetResult(ValueTask<TResult> valueTask, out TResult? result)
    {
        if (valueTask.IsCompletedSuccessfully)
        {
            result = valueTask.Result;
            return true;
        }

        result = default;
        return false;
    }
}
