namespace IntegersArray
{
    public class CircularLinkedListNode<T>
    {
        public CircularLinkedListNode(T value = default(T))
        {
            Value = value;
        }

        public T Value
        {
            get; internal set;
        }

        public CircularLinkedListNode<T> Previous
        {
            get; internal set;
        }

        public CircularLinkedListNode<T> Next
        {
            get; internal set;
        }

        public CircularLinkedList<T> List
        {
            get; internal set;
        }
    }
}
