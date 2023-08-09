

using System.Linq.Expressions;

namespace MockU;

internal static partial class ExpressionExtensions
{
    /// <summary>
    ///   Applies the specified <see cref="ExpressionVisitor"/> to this expression tree.
    /// </summary>
    /// <param name="expression">The <see cref="Expression"/> to which <paramref name="visitor"/> should be applied.</param>
    /// <param name="visitor">The <see cref="ExpressionVisitor"/> that should be applied to <paramref name="expression"/>.</param>
    public static Expression Apply(this Expression expression, ExpressionVisitor visitor)
    {
        return visitor.Visit(expression);
    }
}
