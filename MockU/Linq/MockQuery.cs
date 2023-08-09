using System.Collections;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU.Linq;

/// <summary>
/// A default implementation of IQueryable for use with QueryProvider
/// </summary>
internal class MockQueryable<T> : IQueryable<T>, IQueryProvider

{
    public MockQueryable(Expression expression)
    {
        Debug.Assert(expression != null);

        Guard.ImplementsInterface(typeof(IQueryable<T>), expression.Type, nameof(expression));

        Expression = expression;
    }

    public Type ElementType
    {
        get { return typeof(T); }
    }

    public Expression Expression { get; }

    public IQueryProvider Provider
    {
        get { return this; }
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return CreateQuery<T>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new MockQueryable<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
        return Execute<IQueryable<T>>(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        var replaced = new MockSetupsBuilder().Visit(expression);

        var lambda = Expression.Lambda<Func<TResult>>(replaced);
        return lambda.CompileUsingExpressionCompiler().Invoke();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Provider.Execute<IQueryable<T>>(Expression).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        if (Expression.NodeType == ExpressionType.Constant && ((ConstantExpression)Expression).Value == this)
        {
            return "Query(" + typeof(T) + ")";
        }

        return Expression.ToString();
    }
}
