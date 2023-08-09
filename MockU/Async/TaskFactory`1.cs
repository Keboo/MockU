using System.Linq.Expressions;

namespace MockU.Async;

internal sealed class TaskFactory<TResult> : AwaitableFactory<Task<TResult>, TResult>
{
    public override Task<TResult> CreateCompleted(TResult result)
    {
        return Task.FromResult(result);
    }

    public override Task<TResult> CreateFaulted(Exception exception)
    {
        var tcs = new TaskCompletionSource<TResult>();
        tcs.SetException(exception);
        return tcs.Task;
    }

    public override Task<TResult> CreateFaulted(IEnumerable<Exception> exceptions)
    {
        var tcs = new TaskCompletionSource<TResult>();
        tcs.SetException(exceptions);
        return tcs.Task;
    }

    public override Expression CreateResultExpression(Expression awaitableExpression)
    {
        return Expression.MakeMemberAccess(
            awaitableExpression,
            typeof(Task<TResult>).GetProperty(nameof(Task<TResult>.Result)));
    }

    public override bool TryGetResult(Task<TResult> task, out TResult result)
    {
        if (task.Status == TaskStatus.RanToCompletion)
        {
            result = task.Result;
            return true;
        }

        result = default;
        return false;
    }
}
