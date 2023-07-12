using System;

namespace IntegersArray
{
    public class SortedList<T> : List<T>
        where T : IComparable<T>
    {
        public override T this[int index]
        {
            get => array[index];
            set
            {
                if (!ElementCanBeSet(index, value))
                {
                    return;
                }

                array[index] = value;
            }
        }

        public void Add(T element)
        {
            base.Add(element);
            SortArray();
        }

        public void Insert(int index, T element)
        {
            if (!ElementCanBeInserted(index, element))
            {
                return;
            }

            base.Insert(index, element);
        }

        void SortArray()
        {
            bool isSorted;
            do
            {
                isSorted = true;
                T temp;
                for (int i = 0; i < Count - 1; i++)
                {
                    if (array[i].CompareTo(array[i + 1]) == 1)
                    {
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isSorted = false;
                    }
                }
            }
            while (!isSorted);
        }

        bool ElementCanBeSet(int index, T value)
        {
            return CompareElements(value, index - 1) > -1 && CompareElements(value, index + 1) < 1;
        }

        bool ElementCanBeInserted(int index, T element)
        {
            return CompareElements(element, index - 1) > -1 && CompareElements(element, index) < 1;
        }

        int CompareElements(T firstElement, int secondElementPosition)
        {
            if (!ElementExists(secondElementPosition))
            {
                return 0;
            }

            return firstElement.CompareTo(array[secondElementPosition]);
        }

        bool ElementExists(int position)
        {
            return position >= 0 && position < Count;
        }
    }
}
