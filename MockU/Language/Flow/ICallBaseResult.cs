using System.ComponentModel;

namespace MockU.Language.Flow;

/// <summary>
/// Implements the fluent API.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallBaseResult : IThrows, IThrowsResult, IFluentInterface
{
}
