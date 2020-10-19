using System;
using System.Collections;

namespace IntegersArray
{
    public class ObjectArray : IEnumerable
    {
        object[] array;

        public ObjectArray()
        {
            array = new object[4];
            Count = 0;
        }

        public int Count { get; private set; }

        public object this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public IEnumerator GetEnumerator()
        {
            return new ObjectsEnumerator(array, Count);
        }

        public void Add(object element)
        {
            ArrayResize();
            array[Count] = element;
            Count++;
        }

        public bool Contains(object element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(object element)
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

        public void Insert(int index, object element)
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

        public void Remove(object element)
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
