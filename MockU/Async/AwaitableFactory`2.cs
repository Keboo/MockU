using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU.Async;

/// <summary>
///   Abstract base class that facilitates type-safe implementation of <see cref="IAwaitableFactory"/>
///   for awaitables that produce a result when awaited.
/// </summary>
internal abstract class AwaitableFactory<TAwaitable, TResult> : IAwaitableFactory
{
    public Type ResultType => typeof(TResult);

    public abstract TAwaitable CreateCompleted(TResult result);

    object IAwaitableFactory.CreateCompleted(object? result)
    {
        Debug.Assert(result is TResult || result == null);

        return CreateCompleted((TResult)result);
    }

    public abstract TAwaitable CreateFaulted(Exception exception);

    object IAwaitableFactory.CreateFaulted(Exception exception)
    {
        Debug.Assert(exception != null);

        return CreateFaulted(exception);
    }

    public abstract TAwaitable CreateFaulted(IEnumerable<Exception> exceptions);

    object IAwaitableFactory.CreateFaulted(IEnumerable<Exception> exceptions)
    {
        Debug.Assert(exceptions != null);
        Debug.Assert(exceptions.Any());

        return CreateFaulted(exceptions);
    }

    public abstract bool TryGetResult(TAwaitable awaitable, out TResult? result);

    public abstract Expression CreateResultExpression(Expression awaitableExpression);

    bool IAwaitableFactory.TryGetResult(object awaitable, out object result)
    {
        Debug.Assert(awaitable is TAwaitable);

        if (TryGetResult((TAwaitable)awaitable, out var r))
        {
            result = r;
            return true;
        }

        result = null;
        return false;
    }
}
