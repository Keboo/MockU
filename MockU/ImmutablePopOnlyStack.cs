using System.Diagnostics;

namespace MockU;

internal readonly struct ImmutablePopOnlyStack<T>
{
    private readonly T[] items;
    private readonly int index;

    public ImmutablePopOnlyStack(IEnumerable<T> items)
    {
        Debug.Assert(items != null);

        this.items = items.ToArray();
        index = 0;
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
