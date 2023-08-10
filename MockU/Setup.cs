using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace MockU;

internal abstract class Setup : ISetup
{
    private readonly Mock mock;
    private Flags flags;

    protected Setup(Expression? originalExpression, Mock mock, Expectation expectation)
    {
        Debug.Assert(mock != null);
        Debug.Assert(expectation != null);

        OriginalExpression = originalExpression;
        Expectation = expectation;
        this.mock = mock;
    }

    public virtual Condition? Condition => null;

    public Expectation Expectation { get; }

    public LambdaExpression Expression => Expectation.Expression;

    Mock? ISetup.InnerMock => InnerMocks.SingleOrDefault();

    public virtual IEnumerable<Mock> InnerMocks => Enumerable.Empty<Mock>();

    public bool IsConditional => Condition != null;

    public bool IsOverridden => (flags & Flags.Overridden) != 0;

    public bool IsVerifiable => (flags & Flags.Verifiable) != 0;

    public Mock Mock => mock;

    public Expression? OriginalExpression { get; }

    public bool IsMatched => (flags & Flags.Matched) != 0;

    public void Execute(Invocation invocation)
    {
        // update this setup:
        flags |= Flags.Matched;

        // update invocation:
        invocation.MarkAsMatchedBy(this);
        SetOutParameters(invocation);

        // update condition (important for `MockSequence`) and matchers (important for `Capture`):
        Condition?.SetupEvaluatedSuccessfully();
        Expectation.SetupEvaluatedSuccessfully(invocation);

        if (Expectation.HasResultExpression(out var awaitableFactory))
        {
            try
            {
                ExecuteCore(invocation);
            }
            catch (Exception exception)
            {
                invocation.Exception = exception;
            }
            finally
            {
                invocation.ConvertResultToAwaitable(awaitableFactory);
            }
        }
        else
        {
            ExecuteCore(invocation);
        }
    }

    protected abstract void ExecuteCore(Invocation invocation);

    public void MarkAsOverridden()
    {
        Debug.Assert(!IsOverridden);

        flags |= Flags.Overridden;
    }

    public void MarkAsVerifiable()
    {
        flags |= Flags.Verifiable;
    }

    public bool Matches(Invocation? invocation)
    {
        return Expectation.IsMatch(invocation) && (Condition == null || Condition.IsTrue);
    }

    public bool Matches(MethodExpectation expectation)
    {
        return Expectation.Equals(expectation);
    }

    public virtual void SetOutParameters(Invocation invocation)
    {
    }

    public override string ToString()
    {
        var expression = Expectation.Expression;
        var mockedType = expression.Parameters[0].Type;

        var builder = new StringBuilder();
        builder.AppendNameOf(mockedType)
               .Append(' ')
               .Append(expression.PartialMatcherAwareEval().ToStringFixed());

        return builder.ToString();
    }

    /// <summary>
    ///   Verifies this setup and those of its inner mock (if present and known).
    /// </summary>
    /// <param name="recursive">
    ///   Specifies whether recursive verification should be performed.
    /// </param>
    /// <param name="predicate">
    ///   Specifies which setups should be verified.
    /// </param>
    /// <param name="verifiedMocks">
    ///   The set of mocks that have already been verified.
    /// </param>
    /// <exception cref="MockException">
    ///   This setup or any of its inner mock (if present and known) failed verification.
    /// </exception>
    internal void Verify(bool recursive, Func<ISetup, bool> predicate, HashSet<Mock> verifiedMocks)
    {
        // verify this setup:
        VerifySelf();

        // optionally verify setups of inner mock (if present and known):
        if (recursive)
        {
            try
            {
                foreach (var innerMock in InnerMocks)
                {
                    innerMock.Verify(predicate, verifiedMocks);
                }
            }
            catch (MockException error) when (error.IsVerificationError)
            {
                throw MockException.FromInnerMockOf(this, error);
            }
        }
    }

    protected virtual void VerifySelf()
    {
        if (!IsMatched)
        {
            throw MockException.UnmatchedSetup(this);
        }
    }

    public void Reset()
    {
        flags &= ~Flags.Matched;

        ResetCore();
    }

    protected virtual void ResetCore()
    {
    }

    public void Verify(bool recursive = true)
    {
        Verify(recursive, setup => setup.IsVerifiable);
    }

    public void VerifyAll()
    {
        Verify(recursive: true, setup => true);
    }

    private void Verify(bool recursive, Func<ISetup, bool> predicate)
    {
        var verifiedMocks = new HashSet<Mock>();

        foreach (Invocation invocation in mock.MutableInvocations)
        {
            invocation.MarkAsVerifiedIfMatchedBy(setup => setup == this);
        }

        Verify(recursive, predicate, verifiedMocks);
    }

    protected static Mock? TryGetInnerMockFrom(object? returnValue)
    {
        return (Awaitable.TryGetResultRecursive(returnValue) as IMocked)?.Mock;
    }

    [Flags]
    private enum Flags : byte
    {
        Matched = 1,
        Overridden = 2,
        Verifiable = 4,
    }
}
