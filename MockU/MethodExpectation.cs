using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using MockU.Expressions.Visitors;

using E = System.Linq.Expressions.Expression;

namespace MockU;

/// <summary>
///   An <see cref="Expectation"/> that is bound to a single, specific method.
///   <para>
///     <see cref="Expression"/> has the general form
///     <c>`mock => mock.Method(...arguments)`</c>. Because the method and arguments are frequently needed,
///     they are cached in <see cref="Method"/> and <see cref="Arguments"/>
///     for faster access.
///   </para>
/// </summary>
internal sealed class MethodExpectation : Expectation
{
    public static MethodExpectation CreateFrom(Invocation invocation)
    {
        var method = invocation.Method;

        E[] arguments;
        {
            var parameterTypes = method.GetParameterTypes();
            var n = parameterTypes.Count;
            arguments = new E[n];
            for (int i = 0; i < n; ++i)
            {
                var parameterType = parameterTypes[i];
                if (parameterType.IsByRef) parameterType = parameterType.GetElementType();
                arguments[i] = E.Constant(invocation.Arguments[i], parameterType);
            }
        }

        LambdaExpression expression;
        {
            var mock = E.Parameter(method.DeclaringType, "mock");
            expression = E.Lambda(E.Call(mock, method, arguments).Apply(UpgradePropertyAccessorMethods.Rewriter), mock);
        }

        if (expression.IsProperty())
        {
            var property = expression.ToPropertyInfo();
            Guard.CanRead(property);

            Debug.Assert(property.CanRead(out var getter) && method == getter);
        }

        return new MethodExpectation(expression, method, arguments, exactGenericTypeArguments: true);

        /* Unmerged change from project 'Moq(netstandard2.0)'
        Before:
                private static readonly Expression[] noArguments = new Expression[0];
                private static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];
        After:
                static readonly Expression[] noArguments = new Expression[0];
                static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];
        */

        /* Unmerged change from project 'Moq(netstandard2.1)'
        Before:
                private static readonly Expression[] noArguments = new Expression[0];
                private static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];
        After:
                static readonly Expression[] noArguments = new Expression[0];
                static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];
        */

        /* Unmerged change from project 'Moq(net6.0)'
        Before:
                private static readonly Expression[] noArguments = new Expression[0];
                private static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];
        After:
                static readonly Expression[] noArguments = new Expression[0];
                static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];
        */
    }

    private static readonly E[] noArguments = new E[0];
    private static readonly IMatcher[] noArgumentMatchers = new IMatcher[0];

    

    

    
    private LambdaExpression expression;
    public readonly MethodInfo Method;
    public readonly IReadOnlyList<E> Arguments;

    /* Unmerged change from project 'Moq(netstandard2.0)'
    Before:
            private readonly IMatcher[] argumentMatchers;
            private IAwaitableFactory awaitableFactory;
            private MethodInfo methodImplementation;
            private Expression[] partiallyEvaluatedArguments;
    After:
            readonly IMatcher[] argumentMatchers;
            IAwaitableFactory awaitableFactory;
            MethodInfo methodImplementation;
            Expression[] partiallyEvaluatedArguments;
    */

    /* Unmerged change from project 'Moq(netstandard2.1)'
    Before:
            private readonly IMatcher[] argumentMatchers;
            private IAwaitableFactory awaitableFactory;
            private MethodInfo methodImplementation;
            private Expression[] partiallyEvaluatedArguments;
    After:
            readonly IMatcher[] argumentMatchers;
            IAwaitableFactory awaitableFactory;
            MethodInfo methodImplementation;
            Expression[] partiallyEvaluatedArguments;
    */

    /* Unmerged change from project 'Moq(net6.0)'
    Before:
            private readonly IMatcher[] argumentMatchers;
            private IAwaitableFactory awaitableFactory;
            private MethodInfo methodImplementation;
            private Expression[] partiallyEvaluatedArguments;
    After:
            readonly IMatcher[] argumentMatchers;
            IAwaitableFactory awaitableFactory;
            MethodInfo methodImplementation;
            Expression[] partiallyEvaluatedArguments;
    */
    private readonly IMatcher[] argumentMatchers;
    private IAwaitableFactory awaitableFactory;
    private MethodInfo methodImplementation;
    private E[] partiallyEvaluatedArguments;
#if DEBUG

    

    

    
    private Type proxyType;
#endif

    

    

    
    private readonly bool exactGenericTypeArguments;

    public MethodExpectation(LambdaExpression expression, MethodInfo method, IReadOnlyList<E> arguments = null, bool exactGenericTypeArguments = false, bool skipMatcherInitialization = false, bool allowNonOverridable = false)
    {
        Debug.Assert(expression != null);
        Debug.Assert(method != null);

        if (!allowNonOverridable)  // the sole currently known legitimate case where this evaluates to `false` is when setting non-overridable properties via LINQ to Mocks
        {
            Guard.IsOverridable(method, expression);
            Guard.IsVisibleToProxyFactory(method);
        }

        this.expression = expression;
        Method = method;
        if (arguments != null && !skipMatcherInitialization)
        {
            (argumentMatchers, Arguments) = MatcherFactory.CreateMatchers(arguments, method.GetParameters());
        }
        else
        {
            argumentMatchers = noArgumentMatchers;
            Arguments = arguments ?? noArguments;
        }

        this.exactGenericTypeArguments = exactGenericTypeArguments;
    }

    public override LambdaExpression Expression => expression;

    public void AddResultExpression(Func<E, E> add, IAwaitableFactory awaitableFactory)
    {
        expression = E.Lambda(add(Expression.Body), Expression.Parameters);
        this.awaitableFactory = awaitableFactory;
    }

    public override bool HasResultExpression(out IAwaitableFactory awaitableFactory)
    {
        return (awaitableFactory = this.awaitableFactory) != null;
    }

    public void Deconstruct(out LambdaExpression expression, out MethodInfo method, out IReadOnlyList<E> arguments)
    {
        expression = Expression;
        method = Method;
        arguments = Arguments;
    }

    public override bool IsMatch(Invocation invocation)
    {
        if (invocation.Method != Method && !IsOverride(invocation))
        {
            return false;
        }

        var arguments = invocation.Arguments;
        var parameterTypes = invocation.Method.GetParameterTypes();
        for (int i = 0, n = argumentMatchers.Length; i < n; ++i)
        {
            if (argumentMatchers[i].Matches(arguments[i], parameterTypes[i]) == false)
            {
                return false;
            }
        }

        return true;
    }

    public override void SetupEvaluatedSuccessfully(Invocation invocation)
    {
        var arguments = invocation.Arguments;
        var parameterTypes = invocation.Method.GetParameterTypes();
        for (int i = 0, n = argumentMatchers.Length; i < n; ++i)
        {
            argumentMatchers[i].SetupEvaluatedSuccessfully(arguments[i], parameterTypes[i]);

            

            

            
        }
    }

    private bool IsOverride(Invocation invocation)
    {
        Debug.Assert(invocation.Method != Method);

        var method = Method;
        var invocationMethod = invocation.Method;

        var proxyType = invocation.ProxyType;
#if DEBUG
        // The following `if` block is a sanity check to ensure this `InvocationShape` always
        // runs against the same proxy type. This is important because we're caching the result
        // of mapping methods into that particular proxy type. We have no cache invalidation
        // logic in place; instead, we simply assume that the cached results will stay valid.
        // If the below assertion fails, that assumption was wrong.
        if (this.proxyType == null)
        {
            this.proxyType = proxyType;
        }
        else
        {
            Debug.Assert(this.proxyType == proxyType);
        }
#endif

        // If not already in the cache, map this `InvocationShape`'s method into the proxy type:
        if (methodImplementation == null)
        {
            methodImplementation = method.GetImplementingMethod(proxyType);
        }

        if (invocation.MethodImplementation != methodImplementation)
        {
            return false;
        }

        if (method.IsGenericMethod || invocationMethod.IsGenericMethod)
        {
            if (!method.GetGenericArguments().CompareTo(invocationMethod.GetGenericArguments(), exact: exactGenericTypeArguments, considerTypeMatchers: true))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(Expectation obj)
    {
        if (obj is not MethodExpectation other) return false;

        if (Method != other.Method)
        {
            return false;
        }

        if (Arguments.Count != other.Arguments.Count)
        {
            return false;
        }

        if (partiallyEvaluatedArguments == null)
        {
            partiallyEvaluatedArguments = PartiallyEvaluateArguments(Arguments);
        }

        if (other.partiallyEvaluatedArguments == null)
        {
            other.partiallyEvaluatedArguments = PartiallyEvaluateArguments(other.Arguments);
        }

        var lastParameter = Method.GetParameters().LastOrDefault();
        var lastParameterIsParamArray = lastParameter != null && lastParameter.ParameterType.IsArray && lastParameter.IsDefined(typeof(ParamArrayAttribute));

        for (int i = 0, li = partiallyEvaluatedArguments.Length - 1; i <= li; ++i)
        {
            // Special case for final `params` parameters, which need to be compared by structural equality,
            // not array reference equality:
            if (i == li && lastParameterIsParamArray)
            {
                // In the following, if we retrieved the `params` arrays via `partiallyEvaluatedArguments`,
                // we might see them either as `NewArrayExpression`s or reduced to `ConstantExpression`s.
                // By retrieving them via `Arguments` we always see them as non-reduced `NewArrayExpression`s,
                // so we don't have to distinguish between two cases. (However, the expressions inside those
                // have already been partially evaluated by `MatcherFactory` earlier on!)
                if (Arguments[li] is NewArrayExpression e1 && other.Arguments[li] is NewArrayExpression e2 && e1.Expressions.Count == e2.Expressions.Count)
                {
                    for (int j = 0, nj = e1.Expressions.Count; j < nj; ++j)
                    {
                        if (!ExpressionComparer.Default.Equals(e1.Expressions[j], e2.Expressions[j]))
                        {
                            return false;
                        }
                    }

                    continue;
                }
            }

            if (!ExpressionComparer.Default.Equals(partiallyEvaluatedArguments[i], other.partiallyEvaluatedArguments[i]))
            {
                return false;
            }
        }

        return true;

        /* Unmerged change from project 'Moq(netstandard2.0)'
        Before:
                private static Expression[] PartiallyEvaluateArguments(IReadOnlyList<Expression> arguments)
        After:
                static Expression[] PartiallyEvaluateArguments(IReadOnlyList<Expression> arguments)
        */

        /* Unmerged change from project 'Moq(netstandard2.1)'
        Before:
                private static Expression[] PartiallyEvaluateArguments(IReadOnlyList<Expression> arguments)
        After:
                static Expression[] PartiallyEvaluateArguments(IReadOnlyList<Expression> arguments)
        */

        /* Unmerged change from project 'Moq(net6.0)'
        Before:
                private static Expression[] PartiallyEvaluateArguments(IReadOnlyList<Expression> arguments)
        After:
                static Expression[] PartiallyEvaluateArguments(IReadOnlyList<Expression> arguments)
        */
    }

    private static E[] PartiallyEvaluateArguments(IReadOnlyList<E> arguments)
    {
        Debug.Assert(arguments != null);

        if (arguments.Count == 0)
        {
            return noArguments;
        }

        var partiallyEvaluatedArguments = new E[arguments.Count];
        for (int i = 0, n = arguments.Count; i < n; ++i)
        {
            partiallyEvaluatedArguments[i] = arguments[i].PartialMatcherAwareEval();
        }

        return partiallyEvaluatedArguments;
    }

    public override int GetHashCode()
    {
        return Method.GetHashCode();
    }
}
