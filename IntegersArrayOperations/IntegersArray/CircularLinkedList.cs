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
                if (IsListEmpty())
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
                if (IsListEmpty())
                {
                    throw new ArgumentException("The linked list is empty");
                }

                return sentinelNode.Previous;
            }
        }

        public void Add(T item)
        {
            AddLast(new CircularLinkedListNode<T>(item));
        }

        public void AddLast(CircularLinkedListNode<T> newNode)
        {
            AddAfter(sentinelNode.Previous, newNode);
        }

        public void AddAfter(CircularLinkedListNode<T> existingNode, CircularLinkedListNode<T> newNode)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            if (!IsAListMember(existingNode))
            {
                throw new InvalidOperationException(nameof(existingNode));
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
            AddAfter(existingNode, new CircularLinkedListNode<T>(value));
        }

        public void AddBefore(CircularLinkedListNode<T> existingNode, CircularLinkedListNode<T> newNode)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            AddAfter(existingNode.Previous, newNode);
        }

        public void AddBefore(CircularLinkedListNode<T> existingNode, T value)
        {
            if (existingNode == null)
            {
                throw new ArgumentNullException(nameof(existingNode));
            }

            AddBefore(existingNode, new CircularLinkedListNode<T>(value));
        }

        public void AddFirst(CircularLinkedListNode<T> newNode)
        {
            AddAfter(sentinelNode, newNode);
        }

        public void AddFirst(T value)
        {
            AddFirst(new CircularLinkedListNode<T>(value));
        }

        public CircularLinkedListNode<T> Find(T value)
        {
            for (var node = sentinelNode.Next; node != sentinelNode; node = node.Next)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        public CircularLinkedListNode<T> FindLast(T value)
        {
            for (var node = sentinelNode.Previous; node != sentinelNode; node = node.Previous)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
            }

            return null;
        }

        public void Clear()
        {
            sentinelNode.Next = sentinelNode;
            sentinelNode.Previous = sentinelNode;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
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
            for (var node = sentinelNode.Next; node != sentinelNode; node = node.Next)
            {
                yield return node.Value;
            }
        }

        public bool Remove(T item)
        {
            var nodeToDelete = Find(item);
            if (nodeToDelete == null)
            {
                return false;
            }

            return Remove(nodeToDelete);
        }

        public bool Remove(CircularLinkedListNode<T> nodeToDelete)
        {
            if (nodeToDelete == null)
            {
                throw new ArgumentNullException(nameof(nodeToDelete));
            }

            if (IsListEmpty())
            {
                throw new InvalidOperationException();
            }

            if (!IsAListMember(nodeToDelete))
            {
                throw new InvalidOperationException(nameof(nodeToDelete));
            }

            nodeToDelete.Previous.Next = nodeToDelete.Next;
            nodeToDelete.Next.Previous = nodeToDelete.Previous;
            Count--;
            return true;
        }

        public void RemoveFirst()
        {
            Remove(First);
        }

        public void RemoveLast()
        {
            Remove(Last);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        bool IsListEmpty()
        {
            return sentinelNode.Next == sentinelNode || sentinelNode.Previous == sentinelNode;
        }

        bool IsAListMember(CircularLinkedListNode<T> node)
        {
            return node.Previous != null && node.Next != null;
        }
    }
}
