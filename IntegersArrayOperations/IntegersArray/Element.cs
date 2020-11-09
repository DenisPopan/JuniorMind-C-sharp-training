namespace IntegersArray
{
    class Element<T>
    {
        public Element(T key, T value)
        {
            Key = key;
            Value = value;
        }

        public T Key
        {
            get; internal set;
        }

        public T Value
        {
            get; internal set;
        }

        public int Next
        {
            get; internal set;
        }
    }
}
