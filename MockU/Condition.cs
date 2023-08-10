namespace MockU;

internal sealed class Condition
{
    private readonly Func<bool> condition;
    private readonly Action? success;

    public Condition(Func<bool> condition, Action? success = null)
    {
        this.condition = condition;
        this.success = success;
    }

    public bool IsTrue => condition?.Invoke() == true;

    public void SetupEvaluatedSuccessfully() => success?.Invoke();
}
