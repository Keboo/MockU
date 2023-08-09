using System.ComponentModel;

using MockU.Behaviors;

namespace MockU.Language.Flow;

// keeping the fluent API separate from `SequenceMethodCall` saves us from having to
// define a generic variant `SequenceMethodCallReturn<TResult>`, which would be much more
// work that having a generic fluent API counterpart `SequenceMethodCall<TResult>`.
[EditorBrowsable(EditorBrowsableState.Never)]
internal sealed class SetupSequencePhrase : ISetupSequentialAction

{
    private SequenceSetup setup;

    public SetupSequencePhrase(SequenceSetup setup)
    {
        this.setup = setup;
    }

    public ISetupSequentialAction Pass()
    {
        setup.AddBehavior(NoOp.Instance);
        return this;
    }

    public ISetupSequentialAction Throws<TException>()
        where TException : Exception, new()
        => Throws(new TException());

    public ISetupSequentialAction Throws(Exception exception)
    {
        setup.AddBehavior(new ThrowException(exception));
        return this;
    }

    public ISetupSequentialAction Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        Guard.NotNull(exceptionFunction, nameof(exceptionFunction));

        setup.AddBehavior(new ThrowComputedException(_ => exceptionFunction()));
        return this;
    }

    public override string ToString()
    {
        return setup.Expression.ToStringFixed();

        

        

        
    }
}

internal sealed class SetupSequencePhrase<TResult> : ISetupSequentialResult<TResult>

{
    private SequenceSetup setup;

    public SetupSequencePhrase(SequenceSetup setup)
    {
        this.setup = setup;
    }

    public ISetupSequentialResult<TResult> CallBase()
    {
        setup.AddBehavior(ReturnBase.Instance);
        return this;
    }

    public ISetupSequentialResult<TResult> Returns(TResult value)
    {
        setup.AddBehavior(new ReturnValue(value));
        return this;
    }

    public ISetupSequentialResult<TResult> Returns(Func<TResult> valueFunction)
    {
        Guard.NotNull(valueFunction, nameof(valueFunction));

        // If `valueFunction` is `TResult`, that is someone is setting up the return value of a method
        // that returns a `TResult`, then we have arrived here because C# picked the wrong overload:
        // We don't want to invoke the passed delegate to get a return value; the passed delegate
        // already is the return value.
        if (valueFunction is TResult)
        {
            return Returns((TResult)(object)valueFunction);
        }

        setup.AddBehavior(new ReturnComputedValue(_ => valueFunction()));
        return this;
    }

    public ISetupSequentialResult<TResult> Throws(Exception exception)
    {
        setup.AddBehavior(new ThrowException(exception));
        return this;
    }

    public ISetupSequentialResult<TResult> Throws<TException>()
        where TException : Exception, new()
        => Throws(new TException());

    public ISetupSequentialResult<TResult> Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
    {
        Guard.NotNull(exceptionFunction, nameof(exceptionFunction));

        setup.AddBehavior(new ThrowComputedException(_ => exceptionFunction()));
        return this;
    }

    public override string ToString()
    {
        return setup.Expression.ToStringFixed();
    }
}
