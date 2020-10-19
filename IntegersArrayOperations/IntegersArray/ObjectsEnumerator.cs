using System.Collections;

namespace IntegersArray
{
    public class ObjectsEnumerator : IEnumerator
    {
        readonly object[] objectsArray;
        readonly int numberOfElements;
        int currentPosition = -1;

        public ObjectsEnumerator(object[] givenArray, int count)
        {
            objectsArray = givenArray;
            numberOfElements = count;
        }

        public object Current
        {
            get
            {
                return objectsArray[currentPosition];
            }
        }

        public bool MoveNext()
        {
            if (currentPosition == numberOfElements - 1)
            {
                return false;
            }

            currentPosition++;
            return true;
        }

        public void Reset()
        {
            currentPosition = -1;
        }
    }
}
