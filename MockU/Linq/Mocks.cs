using System.Linq.Expressions;
using System.Reflection;

namespace MockU.Linq;

/// <summary>
/// Allows querying the universe of mocks for those that behave 
/// according to the LINQ query specification.
/// </summary>
public static class Mocks
{
    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those 
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public static IQueryable<T> Of<T>() where T : class
    {
        return Of<T>(MockBehavior.Default);
    }

    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <param name="behavior">Behavior of the mocks.</param>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public static IQueryable<T> Of<T>(MockBehavior behavior) where T : class
    {
        return CreateMockQuery<T>(behavior);
    }

    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those 
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <param name="specification">The predicate with the setup expressions.</param>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public static IQueryable<T> Of<T>(Expression<Func<T, bool>> specification) where T : class
    {
        return Of(specification, MockBehavior.Default);
    }

    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <param name="specification">The predicate with the setup expressions.</param>
    /// <param name="behavior">Behavior of the mocks.</param>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public static IQueryable<T> Of<T>(Expression<Func<T, bool>> specification, MockBehavior behavior) where T : class
    {
        return CreateMockQuery<T>(behavior).Where(specification);
    }

    /// <summary>
    /// Creates the mock query with the underlying queryable implementation.
    /// </summary>
    internal static IQueryable<T> CreateMockQuery<T>(MockBehavior behavior) where T : class
    {
        var method = ((Func<MockBehavior, IQueryable<T>>)CreateQueryable<T>).GetMethodInfo();
        return new MockQueryable<T>(Expression.Call(method, Expression.Constant(behavior)));
    }

    /// <summary>
    /// Wraps the enumerator inside a queryable.
    /// </summary>
    internal static IQueryable<T> CreateQueryable<T>(MockBehavior behavior) where T : class
    {
        return CreateMocks<T>(behavior).AsQueryable();
    }

    /// <summary>
    /// Method that is turned into the actual call from .Query{T}, to 
    /// transform the queryable query into a normal enumerable query.
    /// This method is never used directly by consumers.
    /// </summary>
    private static IEnumerable<T> CreateMocks<T>(MockBehavior behavior) where T : class
    {
        do
        {
            var mock = new Mock<T>(behavior);
            if (behavior != MockBehavior.Strict)
            {
                mock.SetupAllProperties();
            }

            yield return mock.Object;
        }
        while (true);
    }
}
