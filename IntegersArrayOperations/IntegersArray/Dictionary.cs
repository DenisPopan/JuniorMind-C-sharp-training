using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        readonly int[] buckets;
        readonly Element<TKey, TValue>[] elements;
        readonly LinkedList<Element<TKey, TValue>> freeIndex;

        public Dictionary(int size)
        {
            buckets = new int[size];
            PopulateBuckets();
            elements = new Element<TKey, TValue>[size];
            freeIndex = new LinkedList<Element<TKey, TValue>>();
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

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(TKey key, TValue value)
        {
            int bucketIndex = GetPosition(key);
            elements[Count] = new Element<TKey, TValue>();
            elements[Count].Key = key;
            elements[Count].Value = value;
            elements[Count].Next = buckets[bucketIndex];
            buckets[bucketIndex] = Count;
            Count++;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        int GetPosition(TKey key)
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
    }
}
