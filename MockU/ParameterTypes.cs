using System.Collections;
using System.Reflection;

namespace MockU;

/// <summary>
///   Allocation-free adapter type for treating a `ParameterInfo[]` array like a `Type[]` array.
/// </summary>
internal readonly struct ParameterTypes : IReadOnlyList<Type>

/* Unmerged change from project 'Moq(netstandard2.0)'
Before:
        private readonly ParameterInfo[] parameters;
After:
        readonly ParameterInfo[] parameters;
*/

/* Unmerged change from project 'Moq(netstandard2.1)'
Before:
        private readonly ParameterInfo[] parameters;
After:
        readonly ParameterInfo[] parameters;
*/

/* Unmerged change from project 'Moq(net6.0)'
Before:
        private readonly ParameterInfo[] parameters;
After:
        readonly ParameterInfo[] parameters;
*/
{
    private readonly ParameterInfo[] parameters;

    public ParameterTypes(ParameterInfo[] parameters)
    {
        this.parameters = parameters;
    }

    public Type this[int index] => parameters[index].ParameterType;

    public int Count => parameters.Length;

    public IEnumerator<Type> GetEnumerator()
    {
        for (int i = 0, n = Count; i < n; ++i)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
