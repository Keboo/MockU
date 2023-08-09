using System.ComponentModel;

using MockU.Language.Flow;

namespace MockU.Language;

/// <summary>
/// Defines the <c>Callback</c> verb for property getter setups.
/// </summary>
/// <seealso cref="Mock{T}.SetupGet"/>
/// <typeparam name="TMock">Mocked type.</typeparam>
/// <typeparam name="TProperty">Type of the property.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallbackGetter<TMock, TProperty> : IFluentInterface
    where TMock : class
{
    /// <summary>
    /// Specifies a callback to invoke when the property is retrieved.
    /// </summary>
    /// <param name="action">Callback method to invoke.</param>
    /// <example>
    /// Invokes the given callback with the property value being set. 
    /// <code>
    /// mock.SetupGet(x => x.Suspended)
    ///     .Callback(() => called = true)
    ///     .Returns(true);
    /// </code>
    /// </example>
    IReturnsThrowsGetter<TMock, TProperty> Callback(Action action);
}
