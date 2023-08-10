using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using MockU.Expressions.Visitors;

namespace MockU;

/// <summary>
///   Allows creating custom value matchers that can be used on setups and verification,
///   completely replacing the built-in <see cref="It"/> class with your own
///   argument matching rules.
/// </summary>
/// <remarks>
///   Argument matching is used to determine whether a concrete invocation in the mock
///   matches a given setup. This matching mechanism is fully extensible.
/// </remarks>
/// <example>
///   Creating a custom matcher is straightforward. You just need to create a method
///   that returns a value from a call to <see cref="Create{T}(Predicate{T})"/>
///   with your matching condition and optional friendly render expression:
///   <code>
///     public Order IsBigOrder()
///     {
///         return Match.Create&lt;Order&gt;(
///                    o => o.GrandTotal &gt;= 5000,
///                    () => IsBigOrder());  // a friendly expression to render on failures
///     }
///   </code>
///   This method can be used in any mock setup invocation:
///   <code>
///     mock.Setup(m => m.Submit(IsBigOrder())
///         .Throws&lt;UnauthorizedAccessException&gt;();
///   </code>
///   At runtime, Moq knows that the return value was a matcher and
///   evaluates your predicate with the actual value passed into your predicate.
///   <para>
///     Another example might be a case where you want to match a lists of orders
///     that contains a particular one. You might create matcher like the following:
///   </para>
///   <code>
///     public static class Orders
///     {
///         public static IEnumerable&lt;Order&gt; Contains(Order order)
///         {
///             return Match.Create&lt;IEnumerable&lt;Order&gt;&gt;(orders => orders.Contains(order));
///         }
///     }
///   </code>
///   Now we can invoke this static method instead of an argument in an invocation:
///   <code>
///     var order = new Order { ... };
///     var mock = new Mock&lt;IRepository&lt;Order&gt;&gt;();
///
///     mock.Setup(x =&gt; x.Save(Orders.Contains(order)))
///         .Throws&lt;ArgumentException&gt;();
///   </code>
/// </example>
public abstract class Match : IMatcher
{
    /// <devdoc>
    /// Provided for the sole purpose of rendering the delegate passed to the 
    /// matcher constructor if no friendly render lambda is provided.
    /// </devdoc>
    internal static TValue? Matcher<TValue>()
    {
        return default;
    }

    internal abstract bool Matches(object argument, Type parameterType);

    internal abstract void SetupEvaluatedSuccessfully(object argument, Type parameterType);

    bool IMatcher.Matches(object argument, Type parameterType) => Matches(argument, parameterType);

    void IMatcher.SetupEvaluatedSuccessfully(object value, Type parameterType) => SetupEvaluatedSuccessfully(value, parameterType);

    internal Expression? RenderExpression { get; set; }

    /// <summary>
    ///   Initializes the matcher with the condition that will be checked
    ///   in order to match invocation values.
    /// </summary>
    /// <param name="condition">The condition to match against actual values.</param>
    public static T? Create<T>(Predicate<T> condition)
    {
        Register(new Match<T>(condition, () => Matcher<T>()));
        return default;
    }

    /// <summary>
    ///   Initializes the matcher with the condition that will be checked
    ///   in order to match invocation values.
    /// </summary>
    /// <param name="condition">The condition to match against actual values.</param>
    /// <param name="renderExpression">
    ///   A lambda representation of the matcher, to be used when rendering error messages,
    ///   such as <c>() => It.IsAny&lt;string&lt;()</c>.
    /// </param>
    public static T? Create<T>(Predicate<T> condition, Expression<Func<T>> renderExpression)
    {
        Register(new Match<T>(condition, renderExpression));
        return default;
    }

    /// <summary>
    ///   Initializes the matcher with the condition that will be checked in order to match invocation values.
    ///   <para>
    ///     The <paramref name="condition"/> predicate of this overload will not only be provided with a
    ///     method argument, but also with the associated parameter's type. This parameter type essentially
    ///     overrides <typeparamref name="T"/> in cases where the latter is a type matcher. Therefore,
    ///     use this method overload if you want your custom matcher to work together with type matchers.
    ///   </para>
    /// </summary>
    /// <param name="condition">
    ///   The condition to match against actual values.
    ///   <para>
    ///     This function will be passed the invocation argument, as well as the type of the associated parameter.
    ///   </para>
    /// </param>
    /// <param name="renderExpression">
    ///   A lambda representation of the matcher.
    /// </param>
    public static T? Create<T>(Func<object, Type, bool> condition, Expression<Func<T>> renderExpression)
    {
        Guard.NotNull(condition, nameof(condition));
        Guard.NotNull(renderExpression, nameof(renderExpression));

        Register(new MatchFactory(condition, renderExpression));
        return default;
    }

    internal static void Register(Match match)
    {
        // This method is used to set an expression as the last matcher invoked,
        // which is used in the SetupSet to allow matchers in the prop = value
        // delegate expression. This delegate is executed in "fluent" mode in
        // order to capture the value being set, and construct the corresponding
        // methodcall.
        // This is also used in the MatcherFactory for each argument expression.
        // This method ensures that when we execute the delegate, we
        // also track the matcher that was invoked, so that when we create the
        // methodcall we build the expression using it, rather than the null/default
        // value returned from the actual invocation.

        if (MatcherObserver.IsActive(out var observer))
        {
            observer.OnMatch(match);
        }
    }
}

/// <summary>
///   Allows creating custom value matchers that can be used on setups and verification,
///   completely replacing the built-in <see cref="It"/> class with your own
///   argument matching rules.
/// </summary>
/// <typeparam name="T">Type of the value to match.</typeparam>
public class Match<T> : Match, IEquatable<Match<T>>
{
    internal Predicate<T> Condition { get; set; }
    internal Action<T>? Success { get; set; }

    internal Match(Predicate<T> condition, Expression<Func<T>> renderExpression, Action<T>? success = null)
    {
        Condition = condition;
        RenderExpression = renderExpression.Body.Apply(EvaluateCaptures.Rewriter);
        Success = success;
    }

    internal override bool Matches(object argument, Type parameterType)
    {
        return CanCast(argument) && Condition((T)argument);
    }

    internal override void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));
        Debug.Assert(CanCast(argument));

        Success?.Invoke((T)argument);

        

        

        
    }

    private static bool CanCast(object value)
    {
        if (value != null)
        {
            return value is T;
        }
        else
        {
            var t = typeof(T);
            return !t.IsValueType || t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Match<T> other && Equals(other);
    }

    /// <inheritdoc/>
    public bool Equals(Match<T>? other)
    {
        if (Condition == other?.Condition)
        {
            return true;
        }
        else if (Condition.GetMethodInfo() != other?.Condition.GetMethodInfo())
        {
            return false;
        }
        else if (!(RenderExpression is MethodCallExpression ce && ce.Method.DeclaringType == typeof(Match)))
        {
            return ExpressionComparer.Default.Equals(RenderExpression, other.RenderExpression);
        }
        else
        {
            return false;  // The test documented in `MatchFixture.Equality_ambiguity` is caused by this.
                           // Returning true would break equality even worse. The only way to resolve the
                           // ambiguity is to either add a render expression to your custom matcher, or
                           // to test both `Condition.Target` objects for structural equality.
        }
    }

    /// <inheritdoc/>
    public override int GetHashCode() => 0;

    

    

    
}

internal sealed class MatchFactory : Match

{
    private readonly Func<object, Type, bool> condition;

    internal MatchFactory(Func<object, Type, bool> condition, LambdaExpression renderExpression)
    {
        Debug.Assert(condition != null);
        Debug.Assert(renderExpression != null);

        this.condition = condition;
        RenderExpression = renderExpression.Body.Apply(EvaluateCaptures.Rewriter);
    }

    internal override bool Matches(object argument, Type parameterType)
    {
        var canCast = (Predicate<object>)Delegate.CreateDelegate(typeof(Predicate<object>), canCastMethod.MakeGenericMethod(parameterType));
        return canCast(argument) && condition(argument, parameterType);
    }

    internal override void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));

        

        

        
    }

    private static bool CanCast<T>(object value)
    {
        if (value != null)
        {
            return value is T;
        }
        else
        {
            var t = typeof(T);
            return !t.IsValueType || t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);

            /* Unmerged change from project 'Moq(netstandard2.0)'
            Before:
                    private static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            After:
                    static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            */

            /* Unmerged change from project 'Moq(netstandard2.1)'
            Before:
                    private static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            After:
                    static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            */

            /* Unmerged change from project 'Moq(net6.0)'
            Before:
                    private static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            After:
                    static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
            */
        }
    }

    private static readonly MethodInfo canCastMethod = typeof(MatchFactory).GetMethod("CanCast", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);

    // TODO: Check whether we need to implement `IEquatable<>` to make this work with delegate-based
    // setup & verification methods such as `SetupSet`!
}
