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
            bool elementIsGreaterOrEqualThanPreviousOne = true;
            bool elementIsSmallerOrEqualThanNextOne = true;
            if (ElementExists(index - 1))
            {
                elementIsGreaterOrEqualThanPreviousOne = value.CompareTo(array[index - 1]) == 1 || value.CompareTo(array[index - 1]) == 0;
            }

            if (ElementExists(index + 1))
            {
                elementIsSmallerOrEqualThanNextOne = value.CompareTo(array[index + 1]) == -1 || value.CompareTo(array[index + 1]) == 0;
            }

            return elementIsGreaterOrEqualThanPreviousOne && elementIsSmallerOrEqualThanNextOne;
        }

        bool ElementCanBeInserted(int index, T element)
        {
            bool elementIsGreaterOrEqualThanPreviousOne = true;
            bool elementIsSmallerOrEqualThanCurrentOne = true;
            if (ElementExists(index - 1))
            {
                elementIsGreaterOrEqualThanPreviousOne = element.CompareTo(array[index - 1]) == 1 || element.CompareTo(array[index - 1]) == 0;
            }

            if (ElementExists(index))
            {
                elementIsSmallerOrEqualThanCurrentOne = element.CompareTo(array[index]) == -1 || element.CompareTo(array[index]) == 0;
            }

            return elementIsGreaterOrEqualThanPreviousOne && elementIsSmallerOrEqualThanCurrentOne;
        }

        bool ElementExists(int position)
        {
            return position >= 0 && position < Count;
        }
    }
}
