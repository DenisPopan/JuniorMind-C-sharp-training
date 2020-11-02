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
            AddLast(nodeToBeAdded);
        }

        public void AddLast(CircularLinkedListNode<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }

            AddAfter(sentinelNode.Previous, newNode);
        }

        public void AddAfter(CircularLinkedListNode<T> existingNode, CircularLinkedListNode<T> newNode)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }

            newNode.Previous = existingNode;
            newNode.Next = existingNode.Next;
            existingNode.Next.Previous = newNode;
            existingNode.Next = newNode;
            Count++;
        }

        public void AddAfter(CircularLinkedListNode<T> existingNode, T value)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var newNode = new CircularLinkedListNode<T>(value);

            AddAfter(existingNode, newNode);
        }

        public void AddBefore(CircularLinkedListNode<T> existingNode, CircularLinkedListNode<T> newNode)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }

            newNode.Next = existingNode;
            newNode.Previous = existingNode.Previous;
            existingNode.Previous.Next = newNode;
            existingNode.Previous = newNode;
            Count++;
        }

        public void AddBefore(CircularLinkedListNode<T> existingNode, T value)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var newNode = new CircularLinkedListNode<T>(value);
            AddBefore(existingNode, newNode);
        }

        public void AddFirst(CircularLinkedListNode<T> newNode)
        {
            if (newNode == null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }

            newNode.Next = sentinelNode.Next;
            newNode.Previous = sentinelNode;
            sentinelNode.Next.Previous = newNode;
            sentinelNode.Next = newNode;
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
