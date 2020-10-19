using System.Collections;

namespace IntegersArray
{
    public class ObjectsEnumerator : IEnumerator
    {
        readonly object[] objectsArray;
        int currentPosition = -1;

        public ObjectsEnumerator(object[] givenArray)
        {
            objectsArray = givenArray;
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
            currentPosition++;
            return currentPosition < objectsArray.Length;
        }

        public void Reset()
        {
            currentPosition = -1;
        }
    }
}
