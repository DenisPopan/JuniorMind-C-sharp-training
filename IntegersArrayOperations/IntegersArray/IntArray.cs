using System;

namespace IntegersArray
{
    public class IntArray
    {
        protected int[] array;

        public IntArray()
        {
            array = new int[4];
            Count = 0;
        }

        public int Count { get; protected set; }

        public virtual int this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public virtual void Add(int element)
        {
            ArrayResize();
            array[Count] = element;
            Count++;
        }

        public bool Contains(int element)
        {
            return IndexOf(element) != -1;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public virtual void Insert(int index, int element)
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

        public void Remove(int element)
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

        protected void ShiftRight(int insertIndex)
        {
            for (int i = Count; i > insertIndex; i--)
            {
                array[i] = array[i - 1];
            }
        }

        protected void ShiftLeft(int elementToDeletePosition)
        {
            for (int i = elementToDeletePosition; i < Count - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }

        protected void ArrayResize()
        {
            if (Count != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }
    }
}
