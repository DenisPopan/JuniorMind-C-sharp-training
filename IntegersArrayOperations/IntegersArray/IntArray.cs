using System;

namespace IntegersArray
{
    public class IntArray
    {
        int[] array;

        public IntArray()
        {
            array = new int[4];
        }

        public void Add(int element)
        {
            ArrayResize();
            array[Count()] = element;
        }

        public int Count()
        {
            int elementsNumber = array.Length;
            while (elementsNumber > 0 && array[elementsNumber - 1] == 0)
            {
                elementsNumber--;
            }

            return elementsNumber;
        }

        public int Element(int index)
        {
            return array[index];
        }

        public void SetElement(int index, int element)
        {
            array[index] = element;
        }

        public bool Contains(int element)
        {
            return Array.IndexOf(array, element) != -1;
        }

        public int IndexOf(int element)
        {
            return Array.IndexOf(array, element);
        }

        public void Insert(int index, int element)
        {
            ArrayResize();
            ShiftRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            for (int i = 0; i < Count(); i++)
            {
                array[i] = 0;
            }
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
            array[Count() - 1] = 0;
        }

        void ShiftRight(int insertIndex)
        {
            for (int i = Count(); i > insertIndex; i--)
            {
                array[i] = array[i - 1];
            }
        }

        void ShiftLeft(int elementToDeletePosition)
        {
            for (int i = elementToDeletePosition; i < Count() - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }

        void ArrayResize()
        {
            if (Count() != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }
    }
}
