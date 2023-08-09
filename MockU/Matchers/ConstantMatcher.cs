using System.Collections;
using System.Diagnostics;

namespace MockU.Matchers;

internal class ConstantMatcher : IMatcher

{
    private object constantValue;

    public ConstantMatcher(object constantValue)
    {
        this.constantValue = constantValue;
    }

    public bool Matches(object argument, Type parameterType)
    {
        if (Equals(argument, constantValue))
        {
            return true;
        }

        if (constantValue is IEnumerable && argument is IEnumerable enumerable &&
            !(constantValue is IMocked) && !(argument is IMocked))
        // the above checks on the second line are necessary to ensure we have usable
        // implementations of IEnumerable, which might very well not be the case for
        // mocked objects.
        {
            return MatchesEnumerable(enumerable);
        }

        return false;
    }

    public void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));

        

        

        
    }

    private bool MatchesEnumerable(IEnumerable enumerable)
    {
        var constValues = (IEnumerable)constantValue;
        return constValues.Cast<object>().SequenceEqual(enumerable.Cast<object>());
    }
}
