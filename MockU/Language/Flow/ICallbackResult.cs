

using System.ComponentModel;

using Moq.Language;

namespace MockU.Language.Flow;

/// <summary>
/// Implements the fluent API.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackResult : ICallBase, ICallBaseResult, IThrows, IThrowsResult, IFluentInterface
{
}
