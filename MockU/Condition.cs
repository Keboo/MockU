namespace MockU;

internal sealed class Condition

{
    private Func<bool> condition;
    private Action success;

    public Condition(Func<bool> condition, Action success = null)
    {
        this.condition = condition;
        this.success = success;
    }

    public bool IsTrue => condition?.Invoke() == true;

    public void SetupEvaluatedSuccessfully() => success?.Invoke();
}
