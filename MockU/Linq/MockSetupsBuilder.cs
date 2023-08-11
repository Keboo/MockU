using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MockU.Linq;

internal class MockSetupsBuilder : ExpressionVisitor

/* Unmerged change from project 'Moq(netstandard2.0)'
Before:
        private static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
        private static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };
After:
        static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
        static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };
*/

/* Unmerged change from project 'Moq(netstandard2.1)'
Before:
        private static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
        private static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };
After:
        static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
        static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };
*/

/* Unmerged change from project 'Moq(net6.0)'
Before:
        private static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
        private static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };
After:
        static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
        static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };
*/
{
    private static readonly string[] queryableMethods = new[] { "First", "Where", "FirstOrDefault" };
    private static readonly string[] unsupportedMethods = new[] { "All", "Any", "Last", "LastOrDefault", "Single", "SingleOrDefault" };

    

    

    
    private int stackIndex;
    private int quoteDepth;

    public MockSetupsBuilder()
    {
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
        if (stackIndex > 0)
        {
            if (node.NodeType != ExpressionType.Equal && node.NodeType != ExpressionType.AndAlso)
                throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Resources.LinqBinaryOperatorNotSupported, node.ToStringFixed()));

            if (node.NodeType == ExpressionType.Equal)
            {
                // TODO: throw if a matcher is used on either side of the expression.
                //ThrowIfMatcherIsUsed(

                // Account for the inverted assignment/querying like "false == foo.IsValid" scenario
                if (node.Left.NodeType == ExpressionType.Constant)
                    // Invert left & right nodes in this case.
                    return ConvertToSetup(node.Right, node.Left) ?? base.VisitBinary(node);
                else
                    // Perform straight conversion where the right-hand side will be the setup return value.
                    return ConvertToSetup(node.Left, node.Right) ?? base.VisitBinary(node);
            }
        }

        return base.VisitBinary(node);
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        if (stackIndex > 0 && node.Type == typeof(bool))
        {
            return ConvertToSetupReturns(node, Expression.Constant(true));
        }

        return base.VisitMember(node);
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        if (node.Method.DeclaringType == typeof(Queryable) && queryableMethods.Contains(node.Method.Name))
        {
            stackIndex++;
            var result = base.VisitMethodCall(node);
            stackIndex--;
            return result;
        }

        if (unsupportedMethods.Contains(node.Method.Name))
        {
            throw new NotSupportedException(string.Format(
                CultureInfo.CurrentCulture,
                Resources.LinqMethodNotSupported,
                node.Method.Name));
        }

        if (stackIndex > 0 && node.Type == typeof(bool))
        {
            return ConvertToSetupReturns(node, Expression.Constant(true));
        }

        return base.VisitMethodCall(node);
    }

    protected override Expression VisitUnary(UnaryExpression node)
    {
        if (stackIndex > 0 && node.NodeType == ExpressionType.Not)
        {
            return ConvertToSetup(node.Operand, Expression.Constant(false)) ?? base.VisitUnary(node);
        }

        if (node.NodeType == ExpressionType.Quote)
        {
            quoteDepth++;
            var result = quoteDepth > 1 ? node : base.VisitUnary(node);
            quoteDepth--;
            return result;
        }

        return base.VisitUnary(node);
    }

    private static Expression? ConvertToSetup(Expression left, Expression right)
    {
        switch (left.NodeType)
        {
            case ExpressionType.Call:
            case ExpressionType.Invoke:
            case ExpressionType.MemberAccess:
                return ConvertToSetupReturns(left, right);

            case ExpressionType.Convert:
                var left1 = (UnaryExpression)left;
                return ConvertToSetup(left1.Operand, Expression.Convert(right, left1.Operand.Type));
        }

        return null;
    }

    /// <summary>
    ///   Converts a taken-apart binary expression such as `m.A.B` (==) `x` to
    ///   `Mocks.SetupReturns(Mock.Get(m), m' => m'.A.B, (object)x)`.
    /// </summary>
    /// <param name="left">Body of the expression to set up.</param>
    /// <param name="right">Return value to be configured for <paramref name="left"/>.</param>
    private static Expression ConvertToSetupReturns(Expression left, Expression right)
    {
        var v = new ReplaceMockObjectWithParameter();
        var rewrittenLeft = v.Visit(left);

        return Expression.Call(
            Mock.SetupReturnsMethod,
            // mock:
            Expression.Call(
                Mock.GetMethod.MakeGenericMethod(v.MockObject.Type),
                v.MockObject),
            // expression:
            Expression.Lambda(
                rewrittenLeft,
                v.MockObjectParameter),
            // value:
            Expression.Convert(right, typeof(object)));  // explicit boxing operation required for value types
    }

    /// <summary>
    ///   Locates the root mock object in a setup expression (which is usually, but not always, a <see cref="ParameterExpression"/>),
    ///   stores a reference to it, and finally replaces it with a new <see cref="ParameterExpression"/>.
    /// </summary>
    private sealed class ReplaceMockObjectWithParameter : ExpressionVisitor
    {
        private ParameterExpression? mockObjectParameter;

        public Expression? MockObject { get; private set; }

        public ParameterExpression? MockObjectParameter => mockObjectParameter;

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression pe && pe.Type.IsDefined(typeof(CompilerGeneratedAttribute)) && pe.Type.Name.Contains("f__AnonymousType"))
            {
                // In LINQ query expressions with more than one `from` clause such as:
                //
                //   from a in Mocks.Of<A>()
                //   from b in Mocks.Of<B>()
                //   where ...
                //
                // The `.Where()` operator will have a parameter `p` that is neither of type `A` nor `B`,
                // but of a compiler-generated anonymous type (essentially that of `new { a, b }`).
                //
                // In such cases, the mock object is not referenced by `p` itself, but rather by any
                // member of `p`, that is, `p.a` or `p.b`:
                MockObject = node;
                mockObjectParameter = Expression.Parameter(node.Type, pe.Name);
                return mockObjectParameter;
            }
            else
            {
                return base.VisitMember(node);
            }
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // Regular case: The parameter `p` directly references the mock object:
            MockObject = node;
            mockObjectParameter = Expression.Parameter(node.Type, node.Name);
            return mockObjectParameter;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            // An expression may contain nested expressions, such as:
            //
            //     m => m.GetObjectById(It.IsAny<int>(id => id > 0))
            //          ^                                   ^^
            //   We're only interested            ...but not in this one!
            //   in this parameter...
            //
            // Therefore, for the nested (quoted) expression, we short-circuit:
            return node.NodeType == ExpressionType.Quote ? node : base.VisitUnary(node);
        }
    }
}
