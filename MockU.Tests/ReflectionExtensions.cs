using System.Reflection;

namespace MockU.Tests;

public static class ReflectionExtensions
{
    public static IEnumerable<MethodInfo> GetAccessors(this EventInfo @event, bool nonPublic = false)
    {
        yield return @event.GetAddMethod(nonPublic);
        yield return @event.GetRemoveMethod(nonPublic);
    }
}
