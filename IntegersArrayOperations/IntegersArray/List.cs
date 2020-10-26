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
            get
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException();
                }

                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return array[index];
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

                array[index] = value;
            }
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
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

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

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            }

            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("number of elements in the instance is greater than the available space from arrayIndex to the end of the destination array");
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
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            ArrayResize();
            ShiftRight(index);
            array[index] = item;
            Count++;
        }

        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            Array.Resize(ref array, 4);
            Count = 0;
        }

        public bool Remove(T item)
        {
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

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
            if (IsReadOnly)
            {
                throw new NotSupportedException();
            }

            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

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
