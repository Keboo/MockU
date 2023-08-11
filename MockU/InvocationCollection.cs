using System.Collections;
using System.Diagnostics;

namespace MockU;

internal sealed class InvocationCollection : IInvocationList
{
    private Invocation[]? invocations;

    private int capacity = 0;
    private int count = 0;

    private readonly object invocationsLock = new();
    private readonly Mock owner;

    public InvocationCollection(Mock owner)
    {
        Debug.Assert(owner != null);

        this.owner = owner;
    }

    public int Count
    {
        get
        {
            lock (invocationsLock)
            {
                return count;
            }
        }
    }

    public IInvocation? this[int index]
    {
        get
        {
            lock (invocationsLock)
            {
                if (count <= index || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return invocations?[index];
            }
        }
    }

    public void Add(Invocation invocation)
    {
        lock (invocationsLock)
        {
            if (count == capacity)
            {
                var targetCapacity = capacity == 0 ? 4 : capacity * 2;
                Array.Resize(ref invocations, targetCapacity);
                capacity = targetCapacity;
            }

            invocations![count] = invocation;
            count++;
        }
    }

    public void Clear()
    {
        lock (invocationsLock)
        {
            // Replace the collection so readers with a reference to the old collection aren't interrupted
            invocations = null;
            count = 0;
            capacity = 0;

            owner.MutableSetups.Reset();
            // ^ TODO: Currently this could cause a deadlock as another lock will be taken inside this one!
        }
    }

    public Invocation[] ToArray()
    {
        lock (invocationsLock)
        {
            if (count == 0)
            {
                return Array.Empty<Invocation>();
            }

            var result = new Invocation[count];

            Array.Copy(invocations!, result, count);

            return result;
        }
    }

    public Invocation[] ToArray(Func<Invocation, bool> predicate)
    {
        lock (invocationsLock)
        {
            if (count == 0)
            {
                return Array.Empty<Invocation>();
            }

            var result = new List<Invocation>(count);

            for (var i = 0; i < count; i++)
            {
                var invocation = invocations![i];
                if (predicate(invocation))
                {
                    result.Add(invocation);
                }
            }

            return result.ToArray();
        }
    }

    public IEnumerator<IInvocation> GetEnumerator()
    {
        // Take local copies of collection and count so they are isolated from changes by other threads.
        Invocation[]? collection;
        int count;

        lock (invocationsLock)
        {
            collection = invocations;
            count = this.count;
        }

        for (var i = 0; i < count; i++)
        {
            yield return collection![i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
