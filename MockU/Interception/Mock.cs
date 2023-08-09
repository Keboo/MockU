

using MockU;
using MockU.Interception;

namespace MockU;

public partial class Mock : IInterceptor
{
    void IInterceptor.Intercept(Invocation invocation)
    {
        if (HandleWellKnownMethods.Handle(invocation, this))
        {
            return;
        }

        RecordInvocation.Handle(invocation, this);

        if (FindAndExecuteMatchingSetup.Handle(invocation, this))
        {
            return;
        }

        if (HandleEventSubscription.Handle(invocation, this))
        {
            return;
        }

        FailForStrictMock.Handle(invocation, this);

        Return.Handle(invocation, this);
    }
}
