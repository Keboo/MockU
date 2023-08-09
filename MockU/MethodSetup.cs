

using System.Linq.Expressions;
using System.Reflection;

namespace MockU;

/// <summary>
///   Abstract base class for setups that target a single, specific method.
/// </summary>
internal abstract class MethodSetup : Setup
{
    protected MethodSetup(Expression originalExpression, Mock mock, MethodExpectation expectation)
        : base(originalExpression, mock, expectation)
    {
    }

    public MethodInfo Method => ((MethodExpectation)Expectation).Method;
}
