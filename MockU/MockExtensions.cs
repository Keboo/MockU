

using System.ComponentModel;

namespace MockU;

/// <summary>
/// Provides additional methods on mocks.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class MockExtensions
{
    /// <summary>
    /// Resets this mock's state. This includes its setups, configured default return values, registered event handlers, and all recorded invocations.
    /// </summary>
    /// <param name="mock">The mock whose state should be reset.</param>
    public static void Reset(this Mock mock)
    {
        mock.ConfiguredDefaultValues.Clear();
        mock.MutableSetups.Clear();
        mock.EventHandlers.Clear();
        mock.Invocations.Clear();
    }
}
