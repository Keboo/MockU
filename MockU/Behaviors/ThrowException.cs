using System.Diagnostics;

namespace MockU.Behaviors;

internal sealed class ThrowException : Behavior

{
    private readonly Exception exception;

    public ThrowException(Exception exception)
    {
        Debug.Assert(exception != null);

        this.exception = exception;
    }

    public override void Execute(Invocation invocation)
    {
        throw exception;
    }
}
