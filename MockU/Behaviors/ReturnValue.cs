namespace MockU.Behaviors;

internal sealed class ReturnValue : Behavior

{
    public ReturnValue(object value)
    {
        Value = value;
    }

    public object Value { get; }

    public override void Execute(Invocation invocation)
    {
        invocation.ReturnValue = Value;
    }
}
