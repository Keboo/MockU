namespace MockU.Language.Flow;

internal class NonVoidSetupPhrase<T, TResult> : SetupPhrase, ISetup<T, TResult>, ISetupGetter<T, TResult>, IReturnsResult<T> where T : class
{
    public NonVoidSetupPhrase(MethodCall setup) : base(setup)
    {
    }

    public new IReturnsThrows<T, TResult> Callback(InvocationAction action)
    {
        Setup.SetCallbackBehavior(action.Action);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback(Delegate callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    IReturnsThrowsGetter<T, TResult> ICallbackGetter<T, TResult>.Callback(Action callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback(Action callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1>(Action<T1> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2>(Action<T1, T2> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsThrows<T, TResult> Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public new IReturnsResult<T> CallBase()
    {
        Setup.SetCallBaseBehavior();
        return this;
    }

    public IVerifies Raises(Action<T> eventExpression, EventArgs args)
    {
        Setup.SetRaiseEventBehavior(eventExpression, new Func<EventArgs>(() => args));
        return this;
    }

    public IVerifies Raises(Action<T> eventExpression, Func<EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises(Action<T> eventExpression, params object[] args)
    {
        Setup.SetRaiseEventBehavior(eventExpression, args);
        return this;
    }

    public IVerifies Raises<T1>(Action<T> eventExpression, Func<T1, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2>(Action<T> eventExpression, Func<T1, T2, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3>(Action<T> eventExpression, Func<T1, T2, T3, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4>(Action<T> eventExpression, Func<T1, T2, T3, T4, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IVerifies Raises<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T> eventExpression, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, EventArgs> func)
    {
        Setup.SetRaiseEventBehavior(eventExpression, func);
        return this;
    }

    public IReturnsResult<T> Returns(TResult value)
    {
        Setup.SetReturnValueBehavior(value);
        return this;
    }

    public IReturnsResult<T> Returns(InvocationFunc valueFunction)
    {
        Setup.SetReturnComputedValueBehavior(valueFunction.Func);
        return this;
    }

    public IReturnsResult<T> Returns(Delegate valueFunction)
    {
        Setup.SetReturnComputedValueBehavior(valueFunction);
        return this;
    }

    public IReturnsResult<T> Returns(Func<TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1>(Func<T1, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2>(Func<T1, T2, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3>(Func<T1, T2, T3, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }

    public IReturnsResult<T> Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> valueExpression)
    {
        Setup.SetReturnComputedValueBehavior(valueExpression);
        return this;
    }
}
