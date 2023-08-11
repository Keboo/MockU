using System.Diagnostics;

namespace MockU.Language.Flow;

internal abstract class SetupPhrase : ICallbackResult, IVerifies, IThrowsResult

{
    protected SetupPhrase(MethodCall setup)
    {
        Debug.Assert(setup != null);

        Setup = setup;
    }

    public MethodCall Setup { get; }

    public IVerifies AtMost(int callCount)
    {
        Setup.SetExpectedInvocationCount(Times.AtMost(callCount));
        return this;
    }

    public IVerifies AtMostOnce()
    {
        Setup.SetExpectedInvocationCount(Times.AtMostOnce());
        return this;
    }

    public ICallbackResult Callback(InvocationAction action)
    {
        Setup.SetCallbackBehavior(action.Action);
        return this;
    }

    public ICallbackResult Callback(Delegate callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback(Action callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T>(Action<T> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2>(Action<T1, T2> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallbackResult Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> callback)
    {
        Setup.SetCallbackBehavior(callback);
        return this;
    }

    public ICallBaseResult CallBase()
    {
        Setup.SetCallBaseBehavior();
        return this;
    }

    public IThrowsResult Throws(Exception exception)
    {
        Setup.SetThrowExceptionBehavior(exception);
        return this;
    }

    public IThrowsResult Throws<TException>() where TException : Exception, new()
    {
        Setup.SetThrowExceptionBehavior(new TException());
        return this;
    }

    public IThrowsResult Throws(Delegate exceptionFunction)
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, TException>(Func<T1, T2, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, TException>(Func<T1, T2, T3, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, TException>(Func<T1, T2, T3, T4, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, TException>(Func<T1, T2, T3, T4, T5, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, TException>(Func<T1, T2, T3, T4, T5, T6, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, TException>(Func<T1, T2, T3, T4, T5, T6, T7, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public IThrowsResult Throws<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TException> exceptionFunction) where TException : Exception
    {
        Setup.SetThrowComputedExceptionBehavior(exceptionFunction);
        return this;
    }

    public void Verifiable()
    {
        Setup.MarkAsVerifiable();
    }

    public void Verifiable(string failMessage)
    {
        Setup.MarkAsVerifiable();
        Setup.SetFailMessage(failMessage);
    }

    public void Verifiable(Func<Times> times) => Verifiable(times(), null);

    public void Verifiable(Times times) => Verifiable(times, null);

    public void Verifiable(Func<Times> times, string? failMessage) => Verifiable(times(), failMessage);

    public void Verifiable(Times times, string? failMessage)
    {
        Setup.MarkAsVerifiable();
        Setup.SetExpectedInvocationCount(times);
        if (failMessage != null)
        {
            Setup.SetFailMessage(failMessage);
        }
    }

    public override string ToString()
    {
        return Setup.Expression.ToStringFixed();
    }
}
