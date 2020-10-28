using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class ReadOnlyList<T> : IList<T>
    {
        protected IList<T> list;

        public ReadOnlyList(IList<T> list)
        {
            this.list = list;
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

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
                throw new NotSupportedException();
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
            throw new NotSupportedException();
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
            throw new NotSupportedException();
        }

        public virtual void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
    }
}
