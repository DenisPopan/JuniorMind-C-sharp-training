namespace IntegersArray
{
    internal class Element<TKey, TValue>
    {
        internal Element()
        {
            Key = default;
            Value = default;
            Next = -1;
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
