using System;
using System.Collections;
using System.Collections.Generic;

namespace IntegersArray
{
    public class List<T> : IList<T>
    {
        protected T[] array;

        public List()
        {
            array = new T[4];
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public virtual T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            ArrayResize();
            array[Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length - arrayIndex < Count)
            {
                return;
            }

            int currentArrayIndex = 0;

            for (int i = arrayIndex; i < array.Length; i++)
            {
                array[i] = this.array[currentArrayIndex];
                currentArrayIndex++;
            }
        }

        public void Insert(int index, T item)
        {
            ArrayResize();
            ShiftRight(index);
            array[index] = item;
            Count++;
        }

        public void Clear()
        {
            Array.Resize(ref array, 4);
            Count = 0;
        }

        public bool Remove(T item)
        {
            int elementPosition = IndexOf(item);
            if (elementPosition == -1)
            {
                return false;
            }

            RemoveAt(elementPosition);
            return true;
        }

        public void RemoveAt(int index)
        {
            ShiftLeft(index);
            Count--;
        }

        void ShiftRight(int insertIndex)
        {
            for (int i = Count; i > insertIndex; i--)
            {
                array[i] = array[i - 1];
            }
        }

        void ShiftLeft(int elementToDeletePosition)
        {
            for (int i = elementToDeletePosition; i < Count - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }

        void ArrayResize()
        {
            if (Count != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }
    }
}
