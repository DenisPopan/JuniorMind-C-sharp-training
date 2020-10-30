using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class CircularLinkedList<T> : ICollection<CircularLinkedListNode<T>>
    {
        readonly CircularLinkedListNode<T> sentinelNode;

        public CircularLinkedList()
        {
            sentinelNode = new CircularLinkedListNode<T>(default(T));
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

        public void Add(CircularLinkedListNode<T> item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Next = sentinelNode;
            item.Previous = sentinelNode.Previous;
            sentinelNode.Previous.Next = item;
            sentinelNode.Previous = item;
            Count++;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(CircularLinkedListNode<T> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(CircularLinkedListNode<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<CircularLinkedListNode<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(CircularLinkedListNode<T> item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
