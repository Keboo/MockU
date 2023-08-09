

using System.ComponentModel;

using MockU.Language.Flow;

namespace MockU.Language;

/// <summary>
/// Defines the <c>CallBase</c> verb.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface ICallBase : IFluentInterface
{
    /// <summary>
    /// Calls the real method of the object.
    /// </summary>
    ICallBaseResult CallBase();
}
