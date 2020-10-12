using System;

namespace IntegersArray
{
    public class IntArray
    {
        int[] array;
        int elementsNumber;

        public IntArray()
        {
            array = new int[4];
            elementsNumber = 0;
        }

        public void Add(int element)
        {
            ArrayResize();
            array[elementsNumber] = element;
            elementsNumber++;
        }

        public int Count()
        {
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
            for (int i = 0; i < Count(); i++)
            {
                if (array[i] == element)
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(int element)
        {
            for (int i = 0; i < Count(); i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, int element)
        {
            ArrayResize();
            ShiftRight(index);
            array[index] = element;
            elementsNumber++;
        }

        public void Clear()
        {
            Array.Resize(ref array, 4);
            elementsNumber = 0;
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
            elementsNumber--;
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
            if (elementsNumber != array.Length)
            {
                return;
            }

            Array.Resize(ref array, array.Length * 2);
        }
    }
}
