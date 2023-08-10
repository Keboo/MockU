using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using MockU.Behaviors;
using MockU.Interception;

namespace MockU;

internal sealed partial class MethodCall : SetupWithOutParameterSupport
{
    private VerifyInvocationCount? verifyInvocationCount;
    private Behavior? callback;
    private Behavior? raiseEvent;
    private Behavior? returnOrThrow;
    private Behavior? afterReturnCallback;
    private readonly Condition? condition;
    private readonly string? declarationSite;

    public MethodCall(Expression originalExpression, Mock mock, Condition? condition, MethodExpectation expectation)
        : base(originalExpression, mock, expectation)
    {
        this.condition = condition;

        if ((mock.Switches & Switches.CollectDiagnosticFileInfoForSetups) != 0)
        {
            declarationSite = GetUserCodeCallSite();
        }
    }

    public string? FailMessage { get; private set; }

    public override Condition? Condition => condition;

    public override IEnumerable<Mock> InnerMocks
    {
        get
        {
            var innerMock = TryGetInnerMockFrom((returnOrThrow as ReturnValue)?.Value);
            if (innerMock != null)
            {
                yield return innerMock;
            }
        }
    }

    private static string? GetUserCodeCallSite()
    {
        try
        {
            var thisMethod = MethodBase.GetCurrentMethod();
            var mockAssembly = Assembly.GetExecutingAssembly();
            var frame = new StackTrace(true)
                .GetFrames()
                .SkipWhile(f => f.GetMethod() != thisMethod)
                .SkipWhile(f => f.GetMethod().DeclaringType == null || f.GetMethod().DeclaringType.Assembly == mockAssembly)
                .FirstOrDefault();
            var member = frame?.GetMethod();
            if (member != null)
            {
                var declaredAt = new StringBuilder();
                declaredAt.AppendNameOf(member.DeclaringType).Append('.').AppendNameOf(member, false);
                var fileName = Path.GetFileName(frame.GetFileName());
                if (fileName != null)
                {
                    declaredAt.Append(" in ").Append(fileName);
                    var lineNumber = frame.GetFileLineNumber();
                    if (lineNumber != 0)
                    {
                        declaredAt.Append(": line ").Append(lineNumber);
                    }
                }
                return declaredAt.ToString();
            }
        }
        catch
        {
            // Must NEVER fail, as this is a nice-to-have feature only.
        }

        return null;
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        verifyInvocationCount?.Execute(invocation);

        callback?.Execute(invocation);

        raiseEvent?.Execute(invocation);

        if (returnOrThrow != null)
        {
            returnOrThrow.Execute(invocation);
        }
        else if (invocation.Method.ReturnType != typeof(void))
        {
            if (Mock.Behavior == MockBehavior.Strict)
            {
                throw MockException.ReturnValueRequired(invocation);
            }
            else
            {
                new ReturnBaseOrDefaultValue(Mock).Execute(invocation);
            }
        }
        else
        {
            HandleEventSubscription.Handle(invocation, Mock);  // no-op for everything other than event accessors
        }

        afterReturnCallback?.Execute(invocation);
    }

    public void SetCallBaseBehavior()
    {
        if (Mock.MockedType.IsDelegateType())
        {
            throw new NotSupportedException(Resources.CallBaseCannotBeUsedWithDelegateMocks);
        }

        returnOrThrow = ReturnBase.Instance;
    }

    public void SetCallbackBehavior(Delegate callback)
    {
        if (callback is null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        ref Behavior? behavior = ref returnOrThrow is null ? ref this.callback : ref afterReturnCallback;

        if (callback is Action callbackWithoutArguments)
        {
            behavior = new Callback(_ => callbackWithoutArguments());
        }
        else if (callback.GetType() == typeof(Action<IInvocation>))
        {
            // NOTE: Do NOT rewrite the above condition as `callback is Action<IInvocation>`,
            // because this will also yield true if `callback` is a `Action<object>` and thus
            // break existing uses of `(object arg) => ...` callbacks!
            behavior = new Callback((Action<IInvocation>)callback);
        }
        else
        {
            var expectedParamTypes = Method.GetParameterTypes();
            if (!callback.CompareParameterTypesTo(expectedParamTypes))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.InvalidCallbackParameterMismatch,
                        Method.GetParameterTypeList(),
                        callback.GetMethodInfo().GetParameterTypeList()));
            }

            var callbackMethod = callback.GetMethodInfo();

            if (callbackMethod.ReturnType != typeof(void))
            {
                throw new ArgumentException(Resources.InvalidCallbackNotADelegateWithReturnTypeVoid, nameof(callback));
            }

            if (callbackMethod.GetParameterTypes().Any(Extensions.IsOrContainsTypeMatcher))
            {
                throw new ArgumentException(Resources.TypeMatchersMayNotBeUsedWithCallbacks);
            }

            behavior = new Callback(invocation => callback.InvokePreserveStack(invocation.Arguments));
        }
    }

    public void SetFailMessage(string failMessage)
    {
        FailMessage = failMessage;
    }

    public void SetRaiseEventBehavior<TMock>(Action<TMock> eventExpression, Delegate func)
        where TMock : class
    {
        Guard.NotNull(eventExpression, nameof(eventExpression));

        var expression = ExpressionReconstructor.Instance.ReconstructExpression(eventExpression, Mock.ConstructorArguments);

        // TODO: validate that expression is for event subscription or unsubscription

        raiseEvent = new RaiseEvent(Mock, expression, func, null);
    }

    public void SetRaiseEventBehavior<TMock>(Action<TMock> eventExpression, params object[] args)
        where TMock : class
    {
        Guard.NotNull(eventExpression, nameof(eventExpression));

        var expression = ExpressionReconstructor.Instance.ReconstructExpression(eventExpression, Mock.ConstructorArguments);

        // TODO: validate that expression is for event subscription or unsubscription

        raiseEvent = new RaiseEvent(Mock, expression, null, args);
    }

    public void SetReturnValueBehavior(object value)
    {
        Debug.Assert(Method.ReturnType != typeof(void));
        Debug.Assert(returnOrThrow == null);

        returnOrThrow = new ReturnValue(value);
    }

    public void SetReturnComputedValueBehavior(Delegate valueFactory)
    {
        Debug.Assert(Method.ReturnType != typeof(void));
        Debug.Assert(returnOrThrow == null);

        var expectedReturnType = Expectation.HasResultExpression(out var awaitable) ? awaitable.ResultType
                                                                                         : Method.ReturnType;

        if (valueFactory == null)
        {
            // A `null` reference (instead of a valid delegate) is interpreted as the actual return value.
            // This is necessary because the compiler might have picked the unexpected overload for calls
            // like `Returns(null)`, or the user might have picked an overload like `Returns<T>(null)`,
            // and instead of in `Returns(TResult)`, we ended up in `Returns(Delegate)` or `Returns(Func)`,
            // which likely isn't what the user intended.
            // So here we do what we would've done in `Returns(TResult)`:
            returnOrThrow = new ReturnValue(expectedReturnType.GetDefaultValue());
        }
        else if (expectedReturnType == typeof(Delegate))
        {
            // If `TResult` is `Delegate`, that is someone is setting up the return value of a method
            // that returns a `Delegate`, then we have arrived here because C# picked the wrong overload:
            // We don't want to invoke the passed delegate to get a return value; the passed delegate
            // already is the return value.
            returnOrThrow = new ReturnValue(valueFactory);
        }
        else if (IsInvocationFunc(valueFactory))
        {
            returnOrThrow = new ReturnComputedValue(invocation => valueFactory.InvokePreserveStack(new object[] { invocation }));
        }
        else
        {
            ValidateCallback(valueFactory);

            if (valueFactory.CompareParameterTypesTo(Type.EmptyTypes))
            {
                // we need this for the user to be able to use parameterless methods
                returnOrThrow = new ReturnComputedValue(invocation => valueFactory.InvokePreserveStack());
            }
            else
            {
                returnOrThrow = new ReturnComputedValue(invocation => valueFactory.InvokePreserveStack(invocation.Arguments));
            }
        }

        bool IsInvocationFunc(Delegate callback)
        {
            var type = callback.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Func<,>))
            {
                var typeArguments = type.GetGenericArguments();
                return typeArguments[0] == typeof(IInvocation)
                    && (typeArguments[1] == typeof(object) || expectedReturnType.IsAssignableFrom(typeArguments[1]));
            }

            return false;
        }

        void ValidateCallback(Delegate callback)
        {
            var callbackMethod = callback.GetMethodInfo();

            ValidateNumberOfCallbackParameters(callback, callbackMethod);

            ValidateCallbackReturnType(callbackMethod, expectedReturnType);

            if (callbackMethod.GetParameterTypes().Any(Extensions.IsOrContainsTypeMatcher))
            {
                throw new ArgumentException(Resources.TypeMatchersMayNotBeUsedWithCallbacks);
            }
        }
    }

    public void SetThrowExceptionBehavior(Exception exception)
    {
        returnOrThrow = new ThrowException(exception);
    }

    public void SetThrowComputedExceptionBehavior(Delegate exceptionFactory)
    {
        Debug.Assert(returnOrThrow == null);

        if (exceptionFactory == null)
        {
            // A `null` reference (instead of a valid delegate) is interpreted as the actual return value.
            // This is necessary because the compiler might have picked the unexpected overload for calls
            // like `Throws(null)`, or the user might have picked an overload like `Throws<TException>(null)`,
            // and instead of in `Throws(TException)`, we ended up in `Throws(Delegate)` or `Throws(Func)`,
            // which likely isn't what the user intended.
            // So here we do what we would've done in `Throws(TException)`:
            returnOrThrow = new ThrowException(default);
        }
        else
        {
            var callbackMethod = exceptionFactory.GetMethodInfo();

            ValidateNumberOfCallbackParameters(exceptionFactory, callbackMethod);

            ValidateCallbackReturnType(callbackMethod, typeof(Exception));

            if (exceptionFactory.CompareParameterTypesTo(Type.EmptyTypes))
            {
                // we need this for the user to be able to use parameterless methods
                returnOrThrow = new ThrowComputedException(invocation => exceptionFactory.InvokePreserveStack() as Exception);
            }
            else
            {
                returnOrThrow = new ThrowComputedException(invocation => exceptionFactory.InvokePreserveStack(invocation.Arguments) as Exception);
            }
        }
    }

    protected override void ResetCore()
    {
        verifyInvocationCount?.Reset();
    }

    public void SetExpectedInvocationCount(Times times)
    {
        verifyInvocationCount = new VerifyInvocationCount(this, times);
    }

    protected override void VerifySelf()
    {
        if (verifyInvocationCount != null)
        {
            verifyInvocationCount.Verify();
        }
        else
        {
            base.VerifySelf();
        }
    }

    public override string ToString()
    {
        var message = new StringBuilder();

        if (FailMessage != null)
        {
            message.Append(FailMessage).Append(": ");
        }

        message.Append(base.ToString());

        if (declarationSite != null)
        {
            message.Append(" (").Append(declarationSite).Append(')');
        }

        return message.ToString().Trim();

        

        

        
    }

    private void ValidateNumberOfCallbackParameters(Delegate callback, MethodInfo callbackMethod)
    {
        var numberOfActualParameters = callbackMethod.GetParameters().Length;
        if (callbackMethod.IsStatic)
        {
            if (callbackMethod.IsExtensionMethod() || callback.Target != null)
            {
                numberOfActualParameters--;
            }
        }

        if (numberOfActualParameters > 0)
        {
            var numberOfExpectedParameters = Method.GetParameters().Length;
            if (numberOfActualParameters != numberOfExpectedParameters)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.InvalidCallbackParameterCountMismatch,
                        numberOfExpectedParameters,
                        numberOfActualParameters));

                

                

                
            }
        }
    }

    private void ValidateCallbackReturnType(MethodInfo callbackMethod, Type expectedReturnType)
    {
        var actualReturnType = callbackMethod.ReturnType;

        if (actualReturnType == typeof(void))
        {
            throw new ArgumentException(Resources.InvalidReturnsCallbackNotADelegateWithReturnType);
        }

        if (!expectedReturnType.IsAssignableFrom(actualReturnType))
        {
            // TODO: If the return type is a matcher, does the callback's return type need to be matched against it?
            if (typeof(ITypeMatcher).IsAssignableFrom(expectedReturnType) == false)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.InvalidCallbackReturnTypeMismatch,
                        expectedReturnType.GetFormattedName(),
                        actualReturnType.GetFormattedName()));
            }
        }
    }
}
