using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MockU;

internal abstract class Invocation : IInvocation

/* Unmerged change from project 'Moq(netstandard2.0)'
Before:
        private object[] arguments;
        private MethodInfo method;
        private MethodInfo methodImplementation;
        private readonly Type proxyType;
        private object result;
        private Setup matchingSetup;
        private bool verified;
After:
        object[] arguments;
        MethodInfo method;
        MethodInfo methodImplementation;
        readonly Type proxyType;
        object result;
        Setup matchingSetup;
        bool verified;
*/

/* Unmerged change from project 'Moq(netstandard2.1)'
Before:
        private object[] arguments;
        private MethodInfo method;
        private MethodInfo methodImplementation;
        private readonly Type proxyType;
        private object result;
        private Setup matchingSetup;
        private bool verified;
After:
        object[] arguments;
        MethodInfo method;
        MethodInfo methodImplementation;
        readonly Type proxyType;
        object result;
        Setup matchingSetup;
        bool verified;
*/

/* Unmerged change from project 'Moq(net6.0)'
Before:
        private object[] arguments;
        private MethodInfo method;
        private MethodInfo methodImplementation;
        private readonly Type proxyType;
        private object result;
        private Setup matchingSetup;
        private bool verified;
After:
        object[] arguments;
        MethodInfo method;
        MethodInfo methodImplementation;
        readonly Type proxyType;
        object result;
        Setup matchingSetup;
        bool verified;
*/
{
    private MethodInfo methodImplementation;
    private object result;
    private Setup matchingSetup;

    /// <summary>
    /// Initializes a new instance of the <see cref="Invocation"/> class.
    /// </summary>
    /// <param name="proxyType">The <see cref="Type"/> of the concrete proxy object on which a method is being invoked.</param>
    /// <param name="method">The method being invoked.</param>
    /// <param name="arguments">The arguments with which the specified <paramref name="method"/> is being invoked.</param>
    protected Invocation(Type proxyType, MethodInfo method, params object[] arguments)
    {
        Debug.Assert(proxyType != null);
        Debug.Assert(arguments != null);
        Debug.Assert(method != null);

        Arguments = arguments;
        Method = method;
        ProxyType = proxyType;
    }

    /// <summary>
    /// Gets the method of the invocation.
    /// </summary>
    public MethodInfo Method { get; }

    public MethodInfo MethodImplementation
    {
        get
        {
            if (methodImplementation == null)
            {
                methodImplementation = Method.GetImplementingMethod(ProxyType);
            }

            return methodImplementation;
        }
    }

    /// <summary>
    /// Gets the arguments of the invocation.
    /// </summary>
    /// <remarks>
    /// Arguments may be modified. Derived classes must ensure that by-reference parameters are written back
    /// when the invocation is ended by a call to any of the three <c>Returns</c> methods.
    /// </remarks>
    public object[] Arguments { get; }

    IReadOnlyList<object> IInvocation.Arguments => Arguments;

    public ISetup MatchingSetup => matchingSetup;

    public Type ProxyType { get; }

    public object ReturnValue
    {
        get => result is ExceptionResult ? null : result;
        set
        {
            Debug.Assert(result == null);
            result = value;
        }
    }

    public Exception Exception
    {
        get => result is ExceptionResult r ? r.Exception : null;
        set
        {
            Debug.Assert(result == null);
            result = new ExceptionResult(value);
        }
    }

    public void ConvertResultToAwaitable(IAwaitableFactory awaitableFactory)
    {
        if (result is ExceptionResult r)
        {
            result = awaitableFactory.CreateFaulted(r.Exception);
        }
        else if (!Method.ReturnType.IsAssignableFrom(result?.GetType()))
        {
            result = awaitableFactory.CreateCompleted(result);
        }
    }

    public bool IsVerified { get; private set; }

    /// <summary>
    ///   Calls the <see langword="base"/> method implementation
    ///   and returns its return value (or <see langword="null"/> for <see langword="void"/> methods).
    /// </summary>
    protected internal abstract object CallBase();

    internal void MarkAsMatchedBy(Setup setup)
    {
        Debug.Assert(matchingSetup == null);

        matchingSetup = setup;
    }

    internal void MarkAsVerified() => IsVerified = true;

    internal void MarkAsVerifiedIfMatchedBy(Func<Setup, bool> predicate)
    {
        if (matchingSetup != null && predicate(matchingSetup))
        {
            IsVerified = true;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var method = Method;

        var builder = new StringBuilder();
        builder.AppendNameOf(method.DeclaringType);
        builder.Append('.');

        if (method.IsGetAccessor())
        {
            builder.Append(method.Name, 4, method.Name.Length - 4);
        }
        else if (method.IsSetAccessor())
        {
            builder.Append(method.Name, 4, method.Name.Length - 4);
            builder.Append(" = ");
            builder.AppendValueOf(Arguments[0]);
        }
        else
        {
            builder.AppendNameOf(method, includeGenericArgumentList: true);

            // append argument list:
            builder.Append('(');
            for (int i = 0, n = Arguments.Length; i < n; ++i)
            {
                if (i > 0)
                {
                    builder.Append(", ");
                }
                builder.AppendValueOf(Arguments[i]);
            }

            builder.Append(')');
        }

        return builder.ToString();

        

        

        
    }

    /// <summary>
    /// Internal type to mark invocation results as "exception occurred during execution". The type just
    /// wraps the Exception so a thrown exception can be distinguished from an <see cref="System.Exception"/>
    /// return value.
    /// </summary>
    private readonly struct ExceptionResult
    {
        public ExceptionResult(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}
