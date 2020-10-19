using System.Collections;

namespace IntegersArray
{
    public class ObjectsEnumerator : IEnumerator
    {
        readonly ObjectArray objectsArray;
        int currentPosition = -1;

        public ObjectsEnumerator(ObjectArray givenArray)
        {
            objectsArray = givenArray;
        }

        public object Current => objectsArray[currentPosition];

        public bool MoveNext()
        {
            if (currentPosition == objectsArray.Count - 1)
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
