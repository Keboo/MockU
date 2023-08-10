using System.ComponentModel;


namespace MockU.Language.Flow;

/// <summary>
/// Implements the fluent API.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsResult<TMock> : ICallback, IRaise<TMock>, IVerifies, IFluentInterface
{
}