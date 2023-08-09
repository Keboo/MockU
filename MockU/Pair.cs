namespace MockU;

internal readonly struct Pair<T1, T2> : IEquatable<Pair<T1, T2>>
{
    public readonly T1 Item1;
    public readonly T2 Item2;

    public Pair(T1 item1, T2 item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

    public void Deconstruct(out T1 item1, out T2 item2)
    {
        item1 = Item1;
        item2 = Item2;
    }

    public bool Equals(Pair<T1, T2> other)
    {
        return Equals(Item1, other.Item1)
            && Equals(Item2, other.Item2);
    }

    public override bool Equals(object obj)
    {
        return obj is Pair<T1, T2> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return unchecked(1001 * Item1?.GetHashCode() ?? 101 + Item2?.GetHashCode() ?? 11);
    }
}
