

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;

namespace MockU;

/// <summary>
///   Programmable setup used by <see cref="Mock.SetupSequence(Mock, LambdaExpression)"/>.
/// </summary>
internal sealed class SequenceSetup : SetupWithOutParameterSupport

{
    // contains the behaviors set up with the `CallBase`, `Pass`, `Returns`, and `Throws` verbs
    private ConcurrentQueue<Behavior> behaviors;

    public SequenceSetup(Expression originalExpression, Mock mock, MethodExpectation expectation)
        : base(originalExpression, mock, expectation)
    {
        behaviors = new ConcurrentQueue<Behavior>();
    }

    public void AddBehavior(Behavior behavior)
    {
        Debug.Assert(behavior != null);

        behaviors.Enqueue(behavior);
    }

    protected override void ExecuteCore(Invocation invocation)
    {
        if (behaviors.TryDequeue(out var behavior))
        {
            behavior.Execute(invocation);
        }
        else
        {
            // we get here if there are more invocations than configured behaviors.
            // if the setup method does not have a return value, we don't need to do anything;
            // if it does have a return value, we produce the default value.

            var returnType = invocation.Method.ReturnType;
            if (returnType != typeof(void))
            {
                invocation.ReturnValue = returnType.GetDefaultValue();
            }
        }
    }
}
