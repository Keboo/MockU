using System.Diagnostics;

namespace MockU.Behaviors;

internal sealed class ThrowComputedException : Behavior

{
    private readonly Func<IInvocation, Exception> exceptionFactory;

    public ThrowComputedException(Func<IInvocation, Exception> exceptionFactory)
    {
        Debug.Assert(exceptionFactory != null);

        this.exceptionFactory = exceptionFactory;
    }

    public override void Execute(Invocation invocation)
    {
        throw exceptionFactory.Invoke(invocation);
    }
}
