namespace MockU;

/// <summary>
///   A delegate-like type for use with `setup.Callback` which instructs the `Callback` verb
///   to provide the callback with the current <see cref="IInvocation"/>, instead of
///   with a list of arguments.
///   <para>
///     This type is useful in scenarios involving generic type argument matchers (such as
///     <see cref="It.IsAnyType" />) as <see cref="IInvocation"/> allows the discovery of both
///     arguments and type arguments.
///   </para>
/// </summary>
public readonly struct InvocationAction
{
    internal readonly Action<IInvocation> Action;

    /// <summary>
    ///   Initializes a new instance of the <see cref="InvocationAction"/> type.
    /// </summary>
    /// <param name="action">The delegate that should be wrapped by this instance.</param>
    public InvocationAction(Action<IInvocation> action)
    {
        Action = action;
    }
}
