using System.Diagnostics;

namespace MockU.Behaviors;

internal sealed class ReturnComputedValue : Behavior

{
    private readonly Func<IInvocation, object> valueFactory;

    public ReturnComputedValue(Func<IInvocation, object> valueFactory)
    {
        Debug.Assert(valueFactory != null);

        this.valueFactory = valueFactory;
    }

    public override void Execute(Invocation invocation)
    {
        invocation.ReturnValue = valueFactory.Invoke(invocation);
    }
}
