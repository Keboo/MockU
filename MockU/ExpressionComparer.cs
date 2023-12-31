using System.Collections.ObjectModel;
using System.Linq.Expressions;

using MockU.Expressions.Visitors;

namespace MockU;

internal sealed class ExpressionComparer : IEqualityComparer<Expression?>
{
    public static readonly ExpressionComparer Default = new();

    [ThreadStatic]
    private static int quoteDepth = 0;

    private ExpressionComparer()
    {
    }

    public bool Equals(Expression? x, Expression? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        // Before actually comparing two nodes, make sure that captured variables have been
        // evaluated to their current values (as we don't want to compare their identities).
        // But do so only for the main expression; leave quoted (nested) expressions unchanged.

        if (x is MemberExpression && quoteDepth == 0)
        {
            x = x.Apply(EvaluateCaptures.Rewriter);
        }

        if (y is MemberExpression && quoteDepth == 0)
        {
            y = y.Apply(EvaluateCaptures.Rewriter);
        }

        if (x.NodeType == y.NodeType)
        {
            switch (x.NodeType)
            {
                case ExpressionType.Quote:
                    quoteDepth++;
                    try
                    {
                        return EqualsUnary((UnaryExpression)x, (UnaryExpression)y);
                    }
                    finally
                    {
                        quoteDepth--;
                    }

                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.TypeAs:
                case ExpressionType.UnaryPlus:
                    return EqualsUnary((UnaryExpression)x, (UnaryExpression)y);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Assign:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Power:
                    return EqualsBinary((BinaryExpression)x, (BinaryExpression)y);
                case ExpressionType.TypeIs:
                    return EqualsTypeBinary((TypeBinaryExpression)x, (TypeBinaryExpression)y);
                case ExpressionType.Conditional:
                    return EqualsConditional((ConditionalExpression)x, (ConditionalExpression)y);
                case ExpressionType.Constant:
                    return EqualsConstant((ConstantExpression)x, (ConstantExpression)y);
                case ExpressionType.Parameter:
                    return EqualsParameter((ParameterExpression)x, (ParameterExpression)y);
                case ExpressionType.MemberAccess:
                    return EqualsMember((MemberExpression)x, (MemberExpression)y);
                case ExpressionType.Call:
                    return EqualsMethodCall((MethodCallExpression)x, (MethodCallExpression)y);
                case ExpressionType.Lambda:
                    return EqualsLambda((LambdaExpression)x, (LambdaExpression)y);
                case ExpressionType.New:
                    return EqualsNew((NewExpression)x, (NewExpression)y);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return EqualsNewArray((NewArrayExpression)x, (NewArrayExpression)y);
                case ExpressionType.Index:
                    return EqualsIndex((IndexExpression)x, (IndexExpression)y);
                case ExpressionType.Invoke:
                    return EqualsInvocation((InvocationExpression)x, (InvocationExpression)y);
                case ExpressionType.MemberInit:
                    return EqualsMemberInit((MemberInitExpression)x, (MemberInitExpression)y);
                case ExpressionType.ListInit:
                    return EqualsListInit((ListInitExpression)x, (ListInitExpression)y);
            }
        }

        if (x.NodeType == ExpressionType.Extension || y.NodeType == ExpressionType.Extension)
        {
            return EqualsExtension(x, y);
        }

        return false;
    }

    public int GetHashCode(Expression? obj)
    {
        return obj is null ? 0 : obj.GetHashCode();
    }

    private static bool Equals<T>(ReadOnlyCollection<T> x, ReadOnlyCollection<T> y, Func<T, T, bool> comparer)
    {
        if (x.Count != y.Count)
        {
            return false;
        }

        for (var index = 0; index < x.Count; index++)
        {
            if (!comparer(x[index], y[index]))
            {
                return false;
            }
        }

        return true;
    }

    private bool EqualsBinary(BinaryExpression x, BinaryExpression y)
    {
        return x.Method == y.Method && Equals(x.Left, y.Left) && Equals(x.Right, y.Right) &&
            Equals(x.Conversion, y.Conversion);
    }

    private bool EqualsConditional(ConditionalExpression x, ConditionalExpression y)
    {
        return Equals(x.Test, y.Test) && Equals(x.IfTrue, y.IfTrue) && Equals(x.IfFalse, y.IfFalse);
    }

    private static bool EqualsConstant(ConstantExpression x, ConstantExpression y)
    {
        return Equals(x.Value, y.Value);
    }

    private bool EqualsElementInit(ElementInit x, ElementInit y)
    {
        return x.AddMethod == y.AddMethod && Equals(x.Arguments, y.Arguments, Equals);
    }

    private bool EqualsIndex(IndexExpression x, IndexExpression y)
    {
        return Equals(x.Object, y.Object)
            && Equals(x.Indexer, y.Indexer)
            && Equals(x.Arguments, y.Arguments, Equals);
    }

    private bool EqualsInvocation(InvocationExpression x, InvocationExpression y)
    {
        return Equals(x.Expression, y.Expression) && Equals(x.Arguments, y.Arguments, Equals);
    }

    private bool EqualsLambda(LambdaExpression x, LambdaExpression y)
    {
        return x.GetType() == y.GetType() && Equals(x.Body, y.Body) &&
            Equals(x.Parameters, y.Parameters, EqualsParameter);
    }

    private bool EqualsListInit(ListInitExpression x, ListInitExpression y)
    {
        return EqualsNew(x.NewExpression, y.NewExpression) &&
            Equals(x.Initializers, y.Initializers, EqualsElementInit);
    }

    private bool EqualsMemberAssignment(MemberAssignment x, MemberAssignment y)
    {
        return Equals(x.Expression, y.Expression);
    }

    private bool EqualsMemberBinding(MemberBinding x, MemberBinding y)
    {
        if (x.BindingType == y.BindingType && x.Member == y.Member)
        {
            return x.BindingType switch
            {
                MemberBindingType.Assignment => EqualsMemberAssignment((MemberAssignment)x, (MemberAssignment)y),
                MemberBindingType.MemberBinding => EqualsMemberMemberBinding((MemberMemberBinding)x, (MemberMemberBinding)y),
                MemberBindingType.ListBinding => EqualsMemberListBinding((MemberListBinding)x, (MemberListBinding)y),
                _ => throw new ArgumentOutOfRangeException(nameof(x)),
            };
        }

        return false;
    }

    private bool EqualsMember(MemberExpression x, MemberExpression y)
    {
        return x.Member == y.Member && Equals(x.Expression, y.Expression);
    }

    private bool EqualsMemberInit(MemberInitExpression x, MemberInitExpression y)
    {
        return EqualsNew(x.NewExpression, y.NewExpression) &&
            Equals(x.Bindings, y.Bindings, EqualsMemberBinding);
    }

    private bool EqualsMemberListBinding(MemberListBinding x, MemberListBinding y)
    {
        return Equals(x.Initializers, y.Initializers, EqualsElementInit);
    }

    private bool EqualsMemberMemberBinding(MemberMemberBinding x, MemberMemberBinding y)
    {
        return Equals(x.Bindings, y.Bindings, EqualsMemberBinding);
    }

    private bool EqualsMethodCall(MethodCallExpression x, MethodCallExpression y)
    {
        return x.Method == y.Method && Equals(x.Object, y.Object) &&
            Equals(x.Arguments, y.Arguments, Equals);
    }

    private bool EqualsNewArray(NewArrayExpression x, NewArrayExpression y)
    {
        return x.Type == y.Type && Equals(x.Expressions, y.Expressions, Equals);
    }

    private bool EqualsNew(NewExpression x, NewExpression y)
    {
        return x.Constructor == y.Constructor && Equals(x.Arguments, y.Arguments, Equals);
    }

    private bool EqualsParameter(ParameterExpression x, ParameterExpression y)
    {
        return x.Type == y.Type;
    }

    private bool EqualsTypeBinary(TypeBinaryExpression x, TypeBinaryExpression y)
    {
        return x.TypeOperand == y.TypeOperand && Equals(x.Expression, y.Expression);
    }

    private bool EqualsUnary(UnaryExpression x, UnaryExpression y)
    {
        return x.Method == y.Method && Equals(x.Operand, y.Operand);
    }

    private static bool EqualsExtension(Expression x, Expression y)
    {
        // For now, we only care about our own `MatchExpression` extension;
        // if we wanted to be more thorough, we'd try to reduce `x` and `y`,
        // then compare the reduced nodes.

        return x.IsMatch(out var xm) && y.IsMatch(out var ym) && Equals(xm, ym);
    }
}
