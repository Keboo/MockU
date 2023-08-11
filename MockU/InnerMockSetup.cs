using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU;

internal sealed class InnerMockSetup : SetupWithOutParameterSupport
{
    private readonly object? returnValue;

    public InnerMockSetup(Expression originalExpression, Mock mock, MethodExpectation expectation, object? returnValue)
        : base(originalExpression, mock, expectation)
    {
        Debug.Assert(Awaitable.TryGetResultRecursive(returnValue) is IMocked);

        this.returnValue = returnValue;

        MarkAsVerifiable();
    }

    public override IEnumerable<Mock> InnerMocks
    {
        get
        {
            var innerMock = TryGetInnerMockFrom(returnValue);
            Debug.Assert(innerMock != null);
            yield return innerMock;
        }
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        invocation.ReturnValue = returnValue;
    }

    protected override void ResetCore()
    {
        foreach (var innerMock in InnerMocks)
        {
            innerMock.MutableSetups.Reset();
        }
    }

    protected override void VerifySelf()
    {
    }
}
