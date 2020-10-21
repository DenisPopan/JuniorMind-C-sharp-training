using System;
using System.Collections;

namespace IntegersArray
{
    public class List<T> : IEnumerable
    {
        T[] array;

        public List()
        {
            array = new T[4];
            Count = 0;
        }

        public int Count { get; private set; }

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        public void Add(T element)
        {
            ArrayResize();
            array[Count] = element;
            Count++;
        }

        public bool Contains(T element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (array[i].Equals(element))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T element)
        {
            ArrayResize();
            ShiftRight(index);
            array[index] = element;
            Count++;
        }

        public void Clear()
        {
            Array.Resize(ref array, 4);
            Count = 0;
        }

        public void Remove(T element)
        {
            int elementPosition = IndexOf(element);
            if (elementPosition == -1)
            {
                return;
            }

            RemoveAt(elementPosition);
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
