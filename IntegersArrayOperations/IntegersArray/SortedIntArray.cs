namespace IntegersArray
{
    public class SortedIntArray : IntArray
    {
        public SortedIntArray()
        {
            array = new int[4];
            Count = 0;
        }

        public override int this[int index]
        {
            get => array[index];
            set
            {
                array[index] = value;
                SortArray();
            }
        }

        public override void Add(int element)
        {
            ArrayResize();
            array[Count] = element;
            Count++;
            SortArray();
        }

        public override void Insert(int index, int element)
        {
            ArrayResize();
            ShiftRight(index);
            array[index] = element;
            Count++;
            SortArray();
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
    }
}
