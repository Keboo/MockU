using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU.Matchers;

internal class LazyEvalMatcher : IMatcher

{
    private Expression expression;

    public LazyEvalMatcher(Expression expression)
    {
        this.expression = expression;
    }

    public bool Matches(object argument, Type parameterType)
    {
        var eval = Evaluator.PartialEval(expression);
        return eval is ConstantExpression ce && new ConstantMatcher(ce.Value).Matches(argument, parameterType);
    }

    public void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));
    }
}
