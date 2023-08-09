using System.Diagnostics;

namespace MockU.Matchers;

internal sealed class AnyMatcher : IMatcher
{
    public static AnyMatcher Instance { get; } = new AnyMatcher();

    

    

    
    private AnyMatcher()
    {
    }

    public bool Matches(object argument, Type parameterType) => true;

    public void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));
    }
}
