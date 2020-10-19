using Xunit;
using IntegersArray;

namespace IntegersArrayFacts
{
    public class ObjectArrayFacts
    {
        [Fact]
        public void AddMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            Assert.Equal(5, object1.Count);
            Assert.Equal(15, object1[0]);
            Assert.Equal("hey", object1[1]);
            Assert.Equal(10.2f, object1[2]);
            Assert.Equal('A', object1[3]);
            Assert.Equal(true, object1[4]);
        }

        [Fact]
        public void ContainsMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            Assert.True(object1.Contains(15));
            Assert.True(object1.Contains("hey"));
            Assert.True(object1.Contains(10.2f));
            Assert.True(object1.Contains('A'));
            Assert.True(object1.Contains(true));
        }

        [Fact]
        public void IndexOfMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            Assert.Equal(0, object1.IndexOf(15));
            Assert.Equal(1, object1.IndexOf("hey"));
            Assert.Equal(2, object1.IndexOf(10.2f));
            Assert.Equal(3, object1.IndexOf('A'));
            Assert.Equal(4, object1.IndexOf(true));
        }

        [Fact]
        public void InsertMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            object1.Insert(2, false);
            object1.Insert(4, "spinner");
            object1.Insert(1, 66);
            Assert.Equal(15, object1[0]);
            Assert.Equal(66, object1[1]);
            Assert.Equal("hey", object1[2]);
            Assert.Equal(false, object1[3]);
            Assert.Equal(10.2f, object1[4]);
            Assert.Equal("spinner", object1[5]);
            Assert.Equal('A', object1[6]);
            Assert.Equal(true, object1[7]);
        }

        [Fact]
        public void ClearMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            object1.Insert(2, false);
            object1.Insert(4, "spinner");
            object1.Insert(1, 66);
            object1.Clear();
            Assert.Equal(0, object1.Count);
        }

        [Fact]
        public void RemoveMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            object1.Insert(2, false);
            object1.Insert(4, "spinner");
            object1.Insert(1, 66);
            object1.Remove(10.2f);
            object1.Remove("hey");

            Assert.Equal(15, object1[0]);
            Assert.Equal(66, object1[1]);
            Assert.Equal(false, object1[2]);
            Assert.Equal("spinner", object1[3]);
            Assert.Equal('A', object1[4]);
            Assert.Equal(true, object1[5]);
        }

        [Fact]
        public void RemoveAtMethodWorksForAllBaseTypes()
        {
            var object1 = new ObjectArray();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add("hey");
            object1.Add(10.2f);
            object1.Add('A');
            object1.Add(true);
            object1.Insert(2, false);
            object1.Insert(4, "spinner");
            object1.Insert(1, 66);
            object1.RemoveAt(4);
            object1.RemoveAt(1);


            Assert.Equal(15, object1[0]);
            Assert.Equal("hey", object1[1]);
            Assert.Equal(false, object1[2]);
            Assert.Equal("spinner", object1[3]);
            Assert.Equal('A', object1[4]);
            Assert.Equal(true, object1[5]);
        }
    }
}
