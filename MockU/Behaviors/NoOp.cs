namespace MockU.Behaviors;

internal sealed class NoOp : Behavior
{
    public static readonly NoOp Instance = new NoOp();

    

    

    
    private NoOp()
    {
    }

    public override void Execute(Invocation invocation)
    {
    }
}
