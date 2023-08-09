using System.Diagnostics;

namespace MockU.Behaviors;

internal sealed class Callback : Behavior
{
    private readonly Action<IInvocation> callback;

    public Callback(Action<IInvocation> callback)
    {
        Debug.Assert(callback != null);

        this.callback = callback;
    }

    public override void Execute(Invocation invocation)
    {
        callback.Invoke(invocation);
    }
}
