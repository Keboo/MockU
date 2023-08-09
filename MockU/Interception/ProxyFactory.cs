using System.Reflection;

namespace MockU.Interception;

internal abstract class ProxyFactory
{
    /// <summary>
    /// Gets the global <see cref="ProxyFactory"/> instance used by Moq.
    /// </summary>
    public static ProxyFactory Instance { get; } = null!; //= new CastleProxyFactory();

    public abstract object CreateProxy(Type mockType, IInterceptor interceptor, Type[] interfaces, object[] arguments);

    public abstract bool IsMethodVisible(MethodInfo method, out string messageIfNotVisible);

    public abstract bool IsTypeVisible(Type type);
}
