using System.Diagnostics;

namespace MockU.Async;

internal static class AwaitableFactory
{
    private static readonly Dictionary<Type, Func<Type, IAwaitableFactory?>> Providers;

    static AwaitableFactory()
    {
        Providers = new Dictionary<Type, Func<Type, IAwaitableFactory?>>
        {
            [typeof(Task)] = awaitableType => TaskFactory.Instance,
            [typeof(ValueTask)] = awaitableType => ValueTaskFactory.Instance,
            [typeof(Task<>)] = awaitableType => Create(typeof(TaskFactory<>), awaitableType),
            [typeof(ValueTask<>)] = awaitableType => Create(typeof(ValueTaskFactory<>), awaitableType),
        };
    }

    private static IAwaitableFactory? Create(Type awaitableFactoryType, Type awaitableType)
    {
        return (IAwaitableFactory?)Activator.CreateInstance(
            awaitableFactoryType.MakeGenericType(
                awaitableType.GetGenericArguments()));
    }

    public static IAwaitableFactory? TryGet(Type type)
    {
        Debug.Assert(type != null);

        var key = type.IsConstructedGenericType ? type.GetGenericTypeDefinition() : type;

        return Providers.TryGetValue(key, out var provider) ? provider.Invoke(type) : null;
    }
}
