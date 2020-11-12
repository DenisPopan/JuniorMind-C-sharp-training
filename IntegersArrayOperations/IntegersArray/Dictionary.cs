using System;
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

        public System.Collections.Generic.ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[Count];
                int index = 0;
                foreach (var element in this)
                {
                    keys[index] = element.Key;
                    index++;
                }

                return keys;
            }
        }

        public System.Collections.Generic.ICollection<TValue> Values
        {
            get
            {
                TValue[] values = new TValue[Count];
                int index = 0;
                foreach (var element in this)
                {
                    values[index] = element.Value;
                    index++;
                }

                return values;
            }
        }

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

            if (freeIndex.Count == 0)
            {
                AddElement(Count, key, value);
            }
            else
            {
                AddElement(freeIndex.First.Value, key, value);
                freeIndex.RemoveFirst();
            }
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
                RemoveElement(elementToRemovePosition);
            }
            else
            {
                var currentElementPosition = buckets[bucketIndex];
                while (elements[currentElementPosition].Next != -1)
                {
                    if (elements[currentElementPosition].Next == elementToRemovePosition)
                    {
                        elements[currentElementPosition].Next = elements[elementToRemovePosition].Next;
                        RemoveElement(elementToRemovePosition);
                        break;
                    }

                    currentElementPosition = elements[currentElementPosition].Next;
                }
            }

            Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = this[key];
            return true;
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

        void RemoveElement(int elementPosition)
        {
            elements[elementPosition] = null;
            freeIndex.AddFirst(new LinkedListNode<int>(elementPosition));
        }

        void AddElement(int position, TKey key, TValue value)
        {
            int bucketIndex = GetBucketIndex(key);
            elements[position] = new Element<TKey, TValue>();
            elements[position].Key = key;
            elements[position].Value = value;
            elements[position].Next = buckets[bucketIndex];
            buckets[bucketIndex] = Count;
            Count++;
        }
    }
}
