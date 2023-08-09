namespace MockU.Behaviors;

internal sealed class ReturnBase : Behavior
{
    public static readonly ReturnBase Instance = new ReturnBase();

    

    

    
    private ReturnBase()
    {
    }

    public override void Execute(Invocation invocation)
    {
        invocation.ReturnValue = invocation.CallBase();
    }
}
