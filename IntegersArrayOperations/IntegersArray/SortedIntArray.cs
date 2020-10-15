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
            if (Count == 1)
            {
                return true;
            }

            if (index == 0)
            {
                return value <= array[index + 1];
            }

            if (index == Count - 1)
            {
                return value >= array[index - 1];
            }

            return value <= array[index + 1] && value >= array[index - 1];
        }

        bool ElementCanBeInserted(int index, int element)
        {
            if (array[index] < element)
            {
                return false;
            }

            if (index != 0 && array[index - 1] > element)
            {
                return false;
            }

            return true;
        }
    }
}
