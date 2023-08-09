using System.Diagnostics;

namespace MockU;

internal readonly struct ImmutablePopOnlyStack<T>

/* Unmerged change from project 'Moq(netstandard2.0)'
Before:
        private readonly T[] items;
        private readonly int index;
After:
        readonly T[] items;
        readonly int index;
*/

/* Unmerged change from project 'Moq(netstandard2.1)'
Before:
        private readonly T[] items;
        private readonly int index;
After:
        readonly T[] items;
        readonly int index;
*/

/* Unmerged change from project 'Moq(net6.0)'
Before:
        private readonly T[] items;
        private readonly int index;
After:
        readonly T[] items;
        readonly int index;
*/
{
    private readonly T[] items;
    private readonly int index;

    public ImmutablePopOnlyStack(IEnumerable<T> items)
    {
        Debug.Assert(items != null);

        this.items = items.ToArray();
        index = 0;

        /* Unmerged change from project 'Moq(netstandard2.0)'
        Before:
                private ImmutablePopOnlyStack(T[] items, int index)
        After:
                ImmutablePopOnlyStack(T[] items, int index)
        */

        /* Unmerged change from project 'Moq(netstandard2.1)'
        Before:
                private ImmutablePopOnlyStack(T[] items, int index)
        After:
                ImmutablePopOnlyStack(T[] items, int index)
        */

        /* Unmerged change from project 'Moq(net6.0)'
        Before:
                private ImmutablePopOnlyStack(T[] items, int index)
        After:
                ImmutablePopOnlyStack(T[] items, int index)
        */
    }

    private ImmutablePopOnlyStack(T[] items, int index)
    {
        Debug.Assert(items != null);
        Debug.Assert(0 <= index && index <= items.Length);

        this.items = items;
        this.index = index;
    }

    public bool Empty => index == items.Length;

    public T Pop(out ImmutablePopOnlyStack<T> stackBelowTop)
    {
        Debug.Assert(index < items.Length);

        var top = items[index];
        stackBelowTop = new ImmutablePopOnlyStack<T>(items, index + 1);
        return top;
    }
}
