namespace MockU;

internal abstract class Behavior
{
    protected Behavior()
    {
    }

    public abstract void Execute(Invocation invocation);
}
