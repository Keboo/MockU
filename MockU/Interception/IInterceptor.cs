

namespace MockU.Interception;

/// <summary>
/// This role interface represents a <see cref="Mock"/>'s ability to intercept method invocations for its <see cref="Mock.Object"/>.
/// It is meant for use by <see cref="ProxyFactory"/>.
/// </summary>
internal interface IInterceptor
{
    void Intercept(Invocation invocation);
}
