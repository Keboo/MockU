using System.Linq.Expressions;

namespace MockU.Async;

internal interface IAwaitableFactory
{
    Type ResultType { get; }

    object CreateCompleted(object result = null);

    object CreateFaulted(Exception exception);

    object CreateFaulted(IEnumerable<Exception> exceptions);

    Expression CreateResultExpression(Expression awaitableExpression);

    bool TryGetResult(object awaitable, out object result);
}
