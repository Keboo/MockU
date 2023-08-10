using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace MockU;

/// <summary>
///   Represents a set (or the "shape") of invocations
///   against which concrete <see cref="Invocation"/>s can be matched.
/// </summary>
internal abstract class Expectation : IEquatable<Expectation>
{
    public abstract LambdaExpression Expression { get; }

    public virtual bool HasResultExpression([NotNullWhen(true)] out IAwaitableFactory? awaitableFactory)
    {
        awaitableFactory = null;
        return false;
    }

    public override bool Equals(object? obj)
    {
        return obj is Expectation other && Equals(other);
    }

    public abstract bool Equals(Expectation? other);

    public abstract override int GetHashCode();

    public abstract bool IsMatch(Invocation? invocation);

    public virtual void SetupEvaluatedSuccessfully(Invocation invocation)
    {
    }

    public override string ToString()
    {
        return Expression.ToStringFixed();
    }
}
