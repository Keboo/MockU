using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU.Matchers;

internal class ExpressionMatcher : IMatcher

{
    private Expression expression;

    public ExpressionMatcher(Expression expression)
    {
        this.expression = expression;
    }

    public bool Matches(object argument, Type parameterType)
    {
        return argument is Expression valueExpression
            && ExpressionComparer.Default.Equals(expression, valueExpression);
    }

    public void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));
    }
}
