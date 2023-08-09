using System.Diagnostics;

namespace MockU.Matchers;

internal class RefMatcher : IMatcher

{
    private readonly object reference;
    private readonly bool referenceIsValueType;

    public RefMatcher(object reference)
    {
        this.reference = reference;
        referenceIsValueType = reference?.GetType().IsValueType ?? false;
    }

    public bool Matches(object argument, Type parameterType)
    {
        return referenceIsValueType ? Equals(reference, argument)
                                         : ReferenceEquals(reference, argument);
    }

    public void SetupEvaluatedSuccessfully(object value, Type parameterType)
    {
        Debug.Assert(Matches(value, parameterType));
    }
}
