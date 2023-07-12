namespace IntegersArray
{
    public class SortedIntArray : IntArray
    {
        public override int this[int index]
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

        public void Add(int element)
        {
            base.Add(element);
            SortArray();
        }

        public void Insert(int index, int element)
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
                int temp = 0;
                for (int i = 0; i < Count - 1; i++)
                {
                    if (array[i] > array[i + 1])
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

        bool ElementCanBeSet(int index, int value)
        {
            return value <= ElementAt(index + 1, int.MaxValue) && value >= ElementAt(index - 1, int.MinValue);
        }

        bool ElementCanBeInserted(int index, int element)
        {
            return element <= ElementAt(index, int.MaxValue) && element >= ElementAt(index - 1, int.MinValue);
        }

        int ElementAt(int position, int defaultValue)
        {
            if (position >= 0 && position < Count)
            {
                return array[position];
            }

            return defaultValue;
        }
    }
}
