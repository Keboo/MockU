using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU.Behaviors;

internal sealed class RaiseEvent : Behavior
{
    private Mock mock;
    private LambdaExpression expression;
    private Delegate? eventArgsFunc;
    private object[]? eventArgsParams;

    public RaiseEvent(Mock mock, LambdaExpression expression, Delegate? eventArgsFunc, object[]? eventArgsParams)
    {
        Debug.Assert(mock != null);
        Debug.Assert(expression != null);
        Debug.Assert(eventArgsFunc != null ^ eventArgsParams != null);

        this.mock = mock;
        this.expression = expression;
        this.eventArgsFunc = eventArgsFunc;
        this.eventArgsParams = eventArgsParams;
    }

    public override void Execute(Invocation invocation)
    {
        object[] args;

        if (eventArgsParams != null)
        {
            args = eventArgsParams;
        }
        else
        {
            var argsFuncType = eventArgsFunc.GetType();
            if (argsFuncType.IsGenericType && argsFuncType.GetGenericArguments().Length == 1)
            {
                args = new object[] { mock.Object, eventArgsFunc.InvokePreserveStack() };
            }
            else
            {
                args = new object[] { mock.Object, eventArgsFunc.InvokePreserveStack(invocation.Arguments) };
            }
        }

        Mock.RaiseEvent(mock, expression, expression.Split(), args);
    }
}
