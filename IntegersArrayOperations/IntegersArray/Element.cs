namespace IntegersArray
{
    internal class Element<TKey, TValue>
    {
        internal Element(TKey key = default, TValue value = default, int next = -1)
        {
            Key = key;
            Value = value;
            Next = next;
        }

        internal TKey Key
        {
            get; set;
        }

        internal TValue Value
        {
            get; set;
        }

        internal int Next
        {
            get; set;
        }
    }
}
