using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        int freeIndex;

        public Dictionary(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Size needs to be at least 1!");
            }

            buckets = new int[size];
            PopulateBuckets();
            elements = new Element<TKey, TValue>[size];
            freeIndex = -1;
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
                var keys = new LinkedList<TKey>();
                foreach (var element in this)
                {
                    keys.AddLast(element.Key);
                }

                return keys;
            }
        }

        public System.Collections.Generic.ICollection<TValue> Values
        {
            get
            {
                var values = new LinkedList<TValue>();
                foreach (var element in this)
                {
                   values.AddLast(element.Value);
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

            if (freeIndex == -1)
            {
                AddElement(Count, key, value);
            }
            else
            {
                int elementToAddPosition = freeIndex;
                freeIndex = elements[freeIndex].Next;
                AddElement(elementToAddPosition, key, value);
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
                elements[i] = null;
            }

            Count = 0;
            PopulateBuckets();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this[item.Key].Equals(item.Value);
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
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] == -1)
                {
                    continue;
                }

                for (int elementPosition = buckets[i]; elementPosition != -1; elementPosition = elements[elementPosition].Next)
                {
                    yield return new KeyValuePair<TKey, TValue>(elements[elementPosition].Key, elements[elementPosition].Value);
                }
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

            int previousIndex;
            var elementToRemovePosition = FindElementPosition(key, out previousIndex);

            if (elementToRemovePosition == -1)
            {
                return false;
            }

            if (previousIndex == -1)
            {
                buckets[bucketIndex] = elements[elementToRemovePosition].Next;
            }
            else
            {
                elements[previousIndex].Next = elements[elementToRemovePosition].Next;
            }

            RemoveElement(elementToRemovePosition);
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
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
            return FindElementPosition(key, out int previousPosition);
        }

        int FindElementPosition(TKey key, out int previousPosition)
        {
            previousPosition = -1;
            int bucketIndex = GetBucketIndex(key);

            if (bucketIndex < 0 || bucketIndex >= elements.Length || buckets[bucketIndex] == -1)
            {
                return -1;
            }

            for (int elementPosition = buckets[bucketIndex]; elementPosition != -1; elementPosition = elements[elementPosition].Next)
            {
                if (elements[elementPosition].Key.Equals(key))
                {
                    return elementPosition;
                }

                previousPosition = elementPosition;
            }

            return -1;
        }

        void AddElement(int position, TKey key, TValue value)
        {
            int bucketIndex = GetBucketIndex(key);
            elements[position] = new Element<TKey, TValue>(key, value, buckets[bucketIndex]);
            buckets[bucketIndex] = position;
            Count++;
        }

        void RemoveElement(int elementPosition)
        {
            elements[elementPosition].Next = freeIndex;
            freeIndex = elementPosition;
            Count--;
        }
    }
}
