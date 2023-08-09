using System.Diagnostics;

namespace MockU.Matchers;

internal class ParamArrayMatcher : IMatcher

/* Unmerged change from project 'Moq(netstandard2.0)'
Before:
        private IMatcher[] matchers;
After:
        IMatcher[] matchers;
*/

/* Unmerged change from project 'Moq(netstandard2.1)'
Before:
        private IMatcher[] matchers;
After:
        IMatcher[] matchers;
*/

/* Unmerged change from project 'Moq(net6.0)'
Before:
        private IMatcher[] matchers;
After:
        IMatcher[] matchers;
*/
{
    private IMatcher[] matchers;

    public ParamArrayMatcher(IMatcher[] matchers)
    {
        Debug.Assert(matchers != null);

        this.matchers = matchers;
    }

    public bool Matches(object argument, Type parameterType)

    /* Unmerged change from project 'Moq(netstandard2.0)'
    Before:
                Array values = argument as Array;
                if (values == null || this.matchers.Length != values.Length)
    After:
                if (values == null || this.matchers.Length != values.Length)
    */

    /* Unmerged change from project 'Moq(netstandard2.1)'
    Before:
                Array values = argument as Array;
                if (values == null || this.matchers.Length != values.Length)
    After:
                if (values == null || this.matchers.Length != values.Length)
    */

    /* Unmerged change from project 'Moq(net6.0)'
    Before:
                Array values = argument as Array;
                if (values == null || this.matchers.Length != values.Length)
    After:
                if (values == null || this.matchers.Length != values.Length)
    */
    {
        if (argument is not Array values || matchers.Length != values.Length)
        {
            return false;
        }

        var elementType = parameterType.GetElementType();

        for (int index = 0; index < values.Length; index++)
        {
            if (!matchers[index].Matches(values.GetValue(index), elementType))
            {
                return false;
            }
        }

        return true;
    }

    public void SetupEvaluatedSuccessfully(object argument, Type parameterType)
    {
        Debug.Assert(Matches(argument, parameterType));
        Debug.Assert(argument is Array array && array.Length == matchers.Length);

        var values = (Array)argument;
        var elementType = parameterType.GetElementType();
        for (int i = 0, n = matchers.Length; i < n; ++i)
        {
            matchers[i].SetupEvaluatedSuccessfully(values.GetValue(i), elementType);
        }
    }
}
