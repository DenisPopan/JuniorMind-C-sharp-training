using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public abstract class ListDecorator<T> : IList<T>
    {
        protected IList<T> list;

        protected ListDecorator(IList<T> list)
        {
            this.list = list;
        }

        public int Count { get; protected set; }

        public virtual bool IsReadOnly { get; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return list[index];
            }

            set
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException();
                }

                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                list[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            list.Add(item);
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public void Insert(int index, T item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            list.Insert(index, item);
        }

        public virtual void Clear()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            list.Clear();
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            return list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            list.RemoveAt(index);
        }
    }
}
