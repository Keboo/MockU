using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace MockU;

internal abstract class SetupWithOutParameterSupport : MethodSetup

{
    private readonly List<KeyValuePair<int, object>> outValues;

    protected SetupWithOutParameterSupport(Expression originalExpression, Mock mock, MethodExpectation expectation)
        : base(originalExpression, mock, expectation)
    {
        Debug.Assert(expectation != null);

        outValues = GetOutValues(expectation.Arguments, expectation.Method.GetParameters());
    }

    public sealed override void SetOutParameters(Invocation invocation)
    {
        if (outValues != null)
        {
            foreach (var item in outValues)
            {
                invocation.Arguments[item.Key] = item.Value;

                /* Unmerged change from project 'Moq(netstandard2.0)'
                Before:
                        private static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
                After:
                        static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
                */

                /* Unmerged change from project 'Moq(netstandard2.1)'
                Before:
                        private static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
                After:
                        static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
                */

                /* Unmerged change from project 'Moq(net6.0)'
                Before:
                        private static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
                After:
                        static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
                */
            }
        }
    }

    private static List<KeyValuePair<int, object>> GetOutValues(IReadOnlyList<Expression> arguments, ParameterInfo[] parameters)
    {
        List<KeyValuePair<int, object>> outValues = null;
        for (int i = 0, n = parameters.Length; i < n; ++i)
        {
            var parameter = parameters[i];
            if (parameter.ParameterType.IsByRef)
            {
                if ((parameter.Attributes & (ParameterAttributes.In | ParameterAttributes.Out)) == ParameterAttributes.Out)

                /* Unmerged change from project 'Moq(netstandard2.0)'
                Before:
                                        var constant = arguments[i].PartialEval() as ConstantExpression;
                                        if (constant == null)
                After:
                                        if (constant == null)
                */

                /* Unmerged change from project 'Moq(netstandard2.1)'
                Before:
                                        var constant = arguments[i].PartialEval() as ConstantExpression;
                                        if (constant == null)
                After:
                                        if (constant == null)
                */

                /* Unmerged change from project 'Moq(net6.0)'
                Before:
                                        var constant = arguments[i].PartialEval() as ConstantExpression;
                                        if (constant == null)
                After:
                                        if (constant == null)
                */
                {
                    if (arguments[i].PartialEval() is not ConstantExpression constant)
                    {
                        throw new NotSupportedException(Resources.OutExpressionMustBeConstantValue);
                    }

                    if (outValues == null)
                    {
                        outValues = new List<KeyValuePair<int, object>>();
                    }

                    outValues.Add(new KeyValuePair<int, object>(i, constant.Value));
                }
            }
        }
        return outValues;
    }
}
