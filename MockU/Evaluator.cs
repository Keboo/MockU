using System.Linq.Expressions;

namespace MockU;

/// <summary>
/// Provides partial evaluation of subtrees, whenever they can be evaluated locally.
/// </summary>
/// <author>Matt Warren: http://blogs.msdn.com/mattwar</author>
/// <contributor>Documented by InSTEDD: http://www.instedd.org</contributor>
internal static class Evaluator
{
    /// <summary>
    /// Performs evaluation and replacement of independent sub-trees
    /// </summary>
    /// <param name="expression">The root of the expression tree.</param>
    /// <param name="fnCanBeEvaluated">A function that decides whether a given expression
    /// node can be part of the local function.</param>
    /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
    public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
    {
        return new SubtreeEvaluator(new Nominator(fnCanBeEvaluated).Nominate(expression)).Eval(expression);
    }

    /// <summary>
    /// Performs evaluation and replacement of independent sub-trees
    /// </summary>
    /// <param name="expression">The root of the expression tree.</param>
    /// <returns>A new tree with sub-trees evaluated and replaced.</returns>
    public static Expression PartialEval(Expression expression)
    {
        return PartialEval(expression, e => e.NodeType != ExpressionType.Parameter && !(e is MatchExpression));

        

        

        
    }

    /// <summary>
    /// Evaluates and replaces sub-trees when first candidate is reached (top-down)
    /// </summary>
    private class SubtreeEvaluator : ExpressionVisitor

    

    

    
    {
        private HashSet<Expression> candidates;

        internal SubtreeEvaluator(HashSet<Expression> candidates)
        {
            this.candidates = candidates;
        }

        internal Expression Eval(Expression exp)
        {
            return Visit(exp);
        }

        public override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return null;
            }
            if (candidates.Contains(exp))
            {
                return Evaluate(exp);
            }
            return base.Visit(exp);

            

            

            
        }

        private static Expression Evaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                return e;
            }
            LambdaExpression lambda = Expression.Lambda(e);
            Delegate fn = lambda.CompileUsingExpressionCompiler();
            return Expression.Constant(fn.DynamicInvoke(null), e.Type);

            

            

            
        }
    }

    /// <summary>
    /// Performs bottom-up analysis to determine which nodes can possibly
    /// be part of an evaluated sub-tree.
    /// </summary>
    private class Nominator : ExpressionVisitor

    

    

    
    {
        private Func<Expression, bool> fnCanBeEvaluated;
        private HashSet<Expression> candidates;
        private bool cannotBeEvaluated;

        internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
        {
            this.fnCanBeEvaluated = fnCanBeEvaluated;
        }

        internal HashSet<Expression> Nominate(Expression expression)
        {
            candidates = new HashSet<Expression>();
            Visit(expression);
            return candidates;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression != null && expression.NodeType != ExpressionType.Quote)
            {
                bool saveCannotBeEvaluated = cannotBeEvaluated;
                cannotBeEvaluated = false;
                base.Visit(expression);
                if (!cannotBeEvaluated)
                {
                    bool canBeEvaluated;
                    try
                    {
                        canBeEvaluated = fnCanBeEvaluated(expression);
                    }
                    catch
                    {
                        canBeEvaluated = false;
                    }

                    if (canBeEvaluated)
                    {
                        candidates.Add(expression);
                    }
                    else
                    {
                        cannotBeEvaluated = true;
                    }
                }

                cannotBeEvaluated |= saveCannotBeEvaluated;
            }

            return expression;
        }
    }
}
