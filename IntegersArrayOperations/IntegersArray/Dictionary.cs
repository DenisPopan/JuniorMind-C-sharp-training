﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        readonly LinkedList<int> freeIndex;

        public Dictionary(int size)
        {
            buckets = new int[size];
            PopulateBuckets();
            elements = new Element<TKey, TValue>[size];
            freeIndex = new LinkedList<int>();
            Count = 0;
        }

        public bool IsReadOnly
        {
            get;
        }

        public int Count
        {
            get; internal set;
        }

        public System.Collections.Generic.ICollection<TKey> Keys => throw new NotImplementedException();

        public System.Collections.Generic.ICollection<TValue> Values => throw new NotImplementedException();

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var elementPosition = FindElementPosition(key);

                if (elementPosition != -1)
                {
                    return elements[elementPosition].Value;
                }

                throw new KeyNotFoundException();
            }

            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                var elementPosition = FindElementPosition(key);

                if (elementPosition == -1)
                {
                    throw new KeyNotFoundException();
                }

                elements[elementPosition].Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (ContainsKey(key))
            {
                throw new ArgumentException("An element with the same key already exists!");
            }

            int bucketIndex = GetBucketIndex(key);
            elements[Count] = new Element<TKey, TValue>();
            elements[Count].Key = key;
            elements[Count].Value = value;
            elements[Count].Next = buckets[bucketIndex];
            buckets[bucketIndex] = Count;
            Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                elements[i] = new Element<TKey, TValue>();
            }

            Count = 0;
            PopulateBuckets();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var elementPosition = FindElementPosition(item.Key);

            if (elementPosition == -1)
            {
                throw new KeyNotFoundException();
            }

            return elements[elementPosition].Value.Equals(item.Value);
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return FindElementPosition(key) != -1;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
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
                throw new ArgumentException("Number of elements in the instance is greater than the available space from arrayIndex to the end of the destination array");
            }

            int index = 0;
            foreach (var element in this)
            {
                array[index] = element;
                index++;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == null)
                {
                    break;
                }

                if (elements[i].Key.Equals(default) && elements[i].Value.Equals(default))
                {
                    continue;
                }

                yield return new KeyValuePair<TKey, TValue>(elements[i].Key, elements[i].Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bucketIndex = GetBucketIndex(key);

            var elementToRemovePosition = FindElementPosition(key);

            if (elementToRemovePosition == -1)
            {
                return false;
            }

            if (buckets[bucketIndex] == elementToRemovePosition)
            {
                buckets[bucketIndex] = elements[elementToRemovePosition].Next;
                elements[elementToRemovePosition] = new Element<TKey, TValue>();
                freeIndex.AddFirst(new LinkedListNode<int>(elementToRemovePosition));
            }
            else
            {
                var currentElementPosition = buckets[bucketIndex];
                if (elements[currentElementPosition].Next == elementToRemovePosition)
                {
                    elements[currentElementPosition].Next = elements[elementToRemovePosition].Next;
                    elements[elementToRemovePosition] = new Element<TKey, TValue>();
                    freeIndex.AddFirst(new LinkedListNode<int>(elementToRemovePosition));
                }
            }

            Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        int GetBucketIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode() % elements.Length);
        }

        void PopulateBuckets()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }
        }

        int FindElementPosition(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);

            if (bucketIndex < 0 || bucketIndex >= elements.Length || buckets[bucketIndex] == -1)
            {
                return -1;
            }

            var currentElement = elements[buckets[bucketIndex]];

            if (currentElement.Key.Equals(key))
            {
                return buckets[bucketIndex];
            }

            while (currentElement.Next != -1)
            {
                if (elements[currentElement.Next].Key.Equals(key))
                {
                    return currentElement.Next;
                }

                currentElement = elements[currentElement.Next];
            }

            return -1;
        }
    }
}
