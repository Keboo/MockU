using System.Reflection;

namespace MockU.Interception;

internal abstract class ProxyFactory
{
    /// <summary>
    /// Gets the global <see cref="ProxyFactory"/> instance used by Moq.
    /// </summary>
    public static ProxyFactory Instance { get; } = new SourceGeneratorProxyFactory(); //= new CastleProxyFactory();

    public abstract object CreateProxy(Type mockType, IInterceptor interceptor, Type[] interfaces, object[] arguments);

    public abstract bool IsMethodVisible(MethodInfo method, out string messageIfNotVisible);

    public abstract bool IsTypeVisible(Type type);
}

internal partial class SourceGeneratorProxyFactory : ProxyFactory
{
    public static Dictionary<Type, Func<object>> Items { get; } = new();

    public override object CreateProxy(Type mockType, IInterceptor interceptor, Type[] interfaces, object[] arguments)
    {
        if (Items.TryGetValue(mockType, out Func<object>? factory))
        {
            var rv = factory();

            if (rv is IProxy proxy)
            {
                proxy.Interceptor = interceptor;
            }

            return rv;
        }
        throw new NotImplementedException();
    }

    public override bool IsMethodVisible(MethodInfo method, out string messageIfNotVisible)
    {
        throw new NotImplementedException();
    }

    public override bool IsTypeVisible(Type type)
    {
        throw new NotImplementedException();
    }
}
