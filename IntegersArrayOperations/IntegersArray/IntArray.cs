using System;

namespace IntegersArray
{
    public class IntArray
    {
        int[] array;

        public IntArray()
        {
            array = new int[0];
        }

        public void Add(int element)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = element;
        }

        public int Count()
        {
            return array.Length;
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
            Array.Resize(ref array, array.Length + 1);
            ShiftRight(index);
            array[index] = element;
        }

        public void Clear()
        {
            Array.Resize(ref array, 0);
        }

        public void Remove(int element)
        {
            int elementPosition = IndexOf(element);
            if (elementPosition == -1)
            {
                return;
            }

            ShiftLeft(elementPosition);
            Array.Resize(ref array, array.Length - 1);
        }

        public void RemoveAt(int index)
        {
            ShiftLeft(index);
            Array.Resize(ref array, array.Length - 1);
        }

        void ShiftRight(int insertIndex)
        {
            for (int i = array.Length - 1; i > insertIndex; i--)
            {
                array[i] = array[i - 1];
            }
        }

        void ShiftLeft(int elementToDeletePosition)
        {
            for (int i = elementToDeletePosition; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
        }
    }
}