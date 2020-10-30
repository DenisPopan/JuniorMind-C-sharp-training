using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class CircularLinkedList<T> : ICollection<T>
    {
        readonly CircularLinkedListNode<T> sentinelNode;

        public CircularLinkedList()
        {
            sentinelNode = new CircularLinkedListNode<T>();
            sentinelNode.Next = sentinelNode;
            sentinelNode.Previous = sentinelNode;
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public CircularLinkedListNode<T> First
        {
            get
            {
                if (sentinelNode.Next == sentinelNode)
                {
                    throw new ArgumentException("The linked list is empty");
                }

                return sentinelNode.Next;
            }
        }

        public CircularLinkedListNode<T> Last
        {
            get
            {
                if (sentinelNode.Next == sentinelNode)
                {
                    throw new ArgumentException("The linked list is empty");
                }

                return sentinelNode.Previous;
            }
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var nodeToBeAdded = new CircularLinkedListNode<T>();

            nodeToBeAdded.Value = item;
            nodeToBeAdded.Next = sentinelNode;
            nodeToBeAdded.Previous = sentinelNode.Previous;
            sentinelNode.Previous.Next = nodeToBeAdded;
            sentinelNode.Previous = nodeToBeAdded;
            Count++;
        }

        public void Clear()
        {
            sentinelNode.Next = sentinelNode;
            sentinelNode.Previous = sentinelNode;
            Count = 0;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            foreach (var nodeValue in this)
            {
                if (nodeValue.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("number of elements in the instance is greater than the available space from arrayIndex to the end of the destination array");
            }

            int index = 0;
            foreach (var nodeValue in this)
            {
                array[index] = nodeValue;
                index++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var auxNode = sentinelNode.Next;

            for (int i = 0; i < Count; i++)
            {
                yield return auxNode.Value;
                auxNode = auxNode.Next;
            }
        }

        public bool Remove(T item)
        {
            var auxNode = sentinelNode.Next;
            while (auxNode != sentinelNode)
            {
                if (auxNode.Value.Equals(item))
                {
                    auxNode.Previous.Next = auxNode.Next;
                    auxNode.Next.Previous = auxNode.Previous;
                    Count--;
                    return true;
                }

                auxNode = auxNode.Next;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
