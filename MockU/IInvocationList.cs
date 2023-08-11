namespace MockU;

/// <summary>
/// A list of invocations which have been performed on a mock.
/// </summary>
public interface IInvocationList : IReadOnlyList<IInvocation?>
{
    /// <summary>
    /// Resets all invocations recorded for this mock.
    /// </summary>
    void Clear();
}
