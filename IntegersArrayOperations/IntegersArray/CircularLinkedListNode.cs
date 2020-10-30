namespace IntegersArray
{
    public class CircularLinkedListNode<T>
    {
        public T Value
        {
            get; set;
        }

        public CircularLinkedListNode<T> Previous
        {
            get; internal set;
        }

        public CircularLinkedListNode<T> Next
        {
            get; internal set;
        }
    }
}
