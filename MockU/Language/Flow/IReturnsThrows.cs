using System.ComponentModel;

namespace MockU.Language.Flow;

/// <summary>
/// Implements the fluent API.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsThrows<TMock, TResult> : IReturns<TMock, TResult>, IThrows, IFluentInterface
    where TMock : class
{
}

/// <summary>
/// Implements the fluent API.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IReturnsThrowsGetter<TMock, TProperty> : IReturnsGetter<TMock, TProperty>, IThrows, IFluentInterface
    where TMock : class
{
}
