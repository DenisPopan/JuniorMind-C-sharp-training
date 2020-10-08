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
            if (index < 0 || index >= array.Length)
            {
                return -1;
            }

            return array[index];
        }

        public void SetElement(int index, int element)
        {
            if (Element(index) == -1)
            {
                return;
            }

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
            if (Element(index) == -1)
            {
                return;
            }

            Array.Resize(ref array, array.Length + 1);
            for (int i = array.Length - 1; i > index; i--)
            {
                array[i] = array[i - 1];
            }

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

            for (int i = elementPosition; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }

            Array.Resize(ref array, array.Length - 1);
        }
    }
}
