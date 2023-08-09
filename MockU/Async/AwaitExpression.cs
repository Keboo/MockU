using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU.Async;

internal sealed class AwaitExpression : Expression

{
    private readonly IAwaitableFactory awaitableFactory;

    public AwaitExpression(Expression operand, IAwaitableFactory awaitableFactory)
    {
        Debug.Assert(awaitableFactory != null);
        Debug.Assert(operand != null);

        this.awaitableFactory = awaitableFactory;
        Operand = operand;
    }

    public override bool CanReduce => false;

    public override ExpressionType NodeType => ExpressionType.Extension;

    public Expression Operand { get; }

    public override Type Type => awaitableFactory.ResultType;

    public override string ToString()
    {
        return awaitableFactory.ResultType == typeof(void) ? $"await {Operand}"
                                                                : $"(await {Operand})";
    }

    protected override Expression VisitChildren(ExpressionVisitor visitor) => this;
}
