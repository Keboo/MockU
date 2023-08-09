using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

using MockU;

namespace MockU;

public partial class MockRepository
{
    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those 
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public IQueryable<T> Of<T>() where T : class
    {
        return this.Of<T>(this.Behavior);
    }

    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <param name="behavior">Behavior of the mocks.</param>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public IQueryable<T> Of<T>(MockBehavior behavior) where T : class
    {
        return CreateMockQuery<T>(behavior);
    }

    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those 
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <param name="specification">The predicate with the setup expressions.</param>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public IQueryable<T> Of<T>(Expression<Func<T, bool>> specification) where T : class
    {
        return this.Of<T>(specification, this.Behavior);
    }

    /// <summary>
    /// Access the universe of mocks of the given type, to retrieve those
    /// that behave according to the LINQ query specification.
    /// </summary>
    /// <param name="specification">The predicate with the setup expressions.</param>
    /// <param name="behavior">Behavior of the mocks.</param>
    /// <typeparam name="T">The type of the mocked object to query.</typeparam>
    public IQueryable<T> Of<T>(Expression<Func<T, bool>> specification, MockBehavior behavior) where T : class
    {
        return CreateMockQuery<T>(behavior).Where(specification);
    }

    /// <summary>
    /// Creates a mock object of the indicated type.
    /// </summary>
    /// <typeparam name="T">The type of the mocked object.</typeparam>
    /// <returns>The mocked object created.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public T OneOf<T>() where T : class
    {
        return this.OneOf<T>(this.Behavior);
    }

    /// <summary>
    /// Creates a mock object of the indicated type.
    /// </summary>
    /// <param name="behavior">Behavior of the mock.</param>
    /// <typeparam name="T">The type of the mocked object.</typeparam>
    /// <returns>The mocked object created.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public T OneOf<T>(MockBehavior behavior) where T : class
    {
        return CreateMockQuery<T>(behavior).First();
    }

    /// <summary>
    /// Creates a mock object of the indicated type.
    /// </summary>
    /// <param name="specification">The predicate with the setup expressions.</param>
    /// <typeparam name="T">The type of the mocked object.</typeparam>
    /// <returns>The mocked object created.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public T OneOf<T>(Expression<Func<T, bool>> specification) where T : class
    {
        return this.OneOf<T>(specification, this.Behavior);
    }

    /// <summary>
    /// Creates a mock object of the indicated type.
    /// </summary>
    /// <param name="specification">The predicate with the setup expressions.</param>
    /// <param name="behavior">Behavior of the mock.</param>
    /// <typeparam name="T">The type of the mocked object.</typeparam>
    /// <returns>The mocked object created.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public T OneOf<T>(Expression<Func<T, bool>> specification, MockBehavior behavior) where T : class
    {
        return CreateMockQuery<T>(behavior).First(specification);
    }

    /// <summary>
    /// Creates the mock query with the underlying queryable implementation.
    /// </summary>
    internal IQueryable<T> CreateMockQuery<T>(MockBehavior behavior) where T : class
    {
        var method = ((Func<MockBehavior, IQueryable<T>>)CreateQueryable<T>).GetMethodInfo();
        return new MockQueryable<T>(Expression.Call(Expression.Constant(this), method, Expression.Constant(behavior)));
    }

    /// <summary>
    /// Wraps the enumerator inside a queryable.
    /// </summary>
    internal IQueryable<T> CreateQueryable<T>(MockBehavior behavior) where T : class
    {
        return CreateMocks<T>(behavior).AsQueryable();
        
    }

    /// <summary>
    /// Method that is turned into the actual call from .Query{T}, to 
    /// transform the queryable query into a normal enumerable query.
    /// This method is never used directly by consumers.
    /// </summary>
    private IEnumerable<T> CreateMocks<T>(MockBehavior behavior) where T : class
    {
        do
        {
            var mock = this.Create<T>(behavior);
            if (behavior != MockBehavior.Strict)
            {
                mock.SetupAllProperties();
            }

            yield return mock.Object;
        }
        while (true);
    }
}
