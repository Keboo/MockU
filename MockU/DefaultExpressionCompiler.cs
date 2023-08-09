using System.Linq.Expressions;

namespace MockU;

internal sealed class DefaultExpressionCompiler : ExpressionCompiler
{
    new public static readonly DefaultExpressionCompiler Instance = new DefaultExpressionCompiler();

    

    

    
    private DefaultExpressionCompiler()
    {
    }

    public override Delegate Compile(LambdaExpression expression)
    {
        return expression.Compile();
    }

    public override TDelegate Compile<TDelegate>(Expression<TDelegate> expression)
    {
        return expression.Compile();
    }
}
