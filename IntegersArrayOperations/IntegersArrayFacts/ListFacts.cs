using Xunit;
using IntegersArray;

namespace IntegersArrayFacts
{
    public class ListFacts
    {
        [Fact]
        public void AddMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add(22);
            object1.Add(10);
            object1.Add(16);
            object1.Add(28);
            Assert.Equal(5, object1.Count);
            Assert.Equal(15, object1[0]);
            Assert.Equal(22, object1[1]);
            Assert.Equal(10, object1[2]);
            Assert.Equal(16, object1[3]);
            Assert.Equal(28, object1[4]);

            var object2 = new List<string>();
            object2.Add("hey");
            object2.Add("house");
            object2.Add("party");
            object2.Add("sounds");
            object2.Add("great");
            Assert.Equal(5, object2.Count);
            Assert.Equal("hey", object2[0]);
            Assert.Equal("house", object2[1]);
            Assert.Equal("party", object2[2]);
            Assert.Equal("sounds", object2[3]);
            Assert.Equal("great", object2[4]);

            var object3 = new List<bool>();
            object3.Add(true);
            object3.Add(false);
            Assert.True(object3[0]);
            Assert.False(object3[1]);

            var object4 = new List<double>();
            object4.Add(16.2);
            object4.Add(36e16);
            Assert.Equal(16.2, object4[0]);
            Assert.Equal(36e16, object4[1]);

            var object5 = new List<char>
            {
                'e',
                'g'
            };
            Assert.Equal('e', object5[0]);
            Assert.Equal('g', object5[1]);
        }

        [Fact]
        public void ContainsMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            Assert.True(object1.Contains(15));

            var object2 = new List<string>();
            Assert.Equal(0, object2.Count);
            object2.Add("hey");
            Assert.True(object2.Contains("hey"));

            var object3 = new List<bool>();
            Assert.Equal(0, object3.Count);
            object3.Add(true);
            Assert.True(object3.Contains(true));

            var object4 = new List<char>();
            Assert.Equal(0, object4.Count);
            object4.Add('e');
            Assert.True(object4.Contains('e'));

            var object5 = new List<double>();
            Assert.Equal(0, object5.Count);
            object5.Add(15.3e4);
            Assert.True(object5.Contains(15.3e4));

        }

        [Fact]
        public void IndexOfMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            Assert.Equal(0, object1.IndexOf(15));

            var object2 = new List<string>();
            Assert.Equal(0, object2.Count);
            object2.Add("hey");
            Assert.Equal(0, object2.IndexOf("hey"));

            var object3 = new List<bool>();
            Assert.Equal(0, object3.Count);
            object3.Add(true);
            Assert.Equal(0, object3.IndexOf(true));

            var object4 = new List<char>();
            Assert.Equal(0, object4.Count);
            object4.Add('e');
            Assert.Equal(0, object4.IndexOf('e'));

            var object5 = new List<double>();
            Assert.Equal(0, object5.Count);
            object5.Add(15.3e4);
            Assert.Equal(0, object5.IndexOf(15.3e4));
        }

        [Fact]
        public void InsertMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Insert(0, 12);
            object1.Insert(0, 16);
            Assert.Equal(16, object1[0]);
            Assert.Equal(12, object1[1]);

            var object2 = new List<string>();
            Assert.Equal(0, object2.Count);
            object2.Add("hey");
            object2.Insert(0, "after");
            object2.Insert(0, "cyborg");
            Assert.Equal("cyborg", object2[0]);
            Assert.Equal("after", object2[1]);

            var object3 = new List<bool>();
            Assert.Equal(0, object3.Count);
            object3.Add(true);
            object3.Insert(0, false);
            object3.Insert(0, true);
            Assert.Equal(true, object3[0]);
            Assert.Equal(false, object3[1]);

            var object4 = new List<char>();
            Assert.Equal(0, object4.Count);
            object4.Add('e');
            object4.Insert(0, 'a');
            object4.Insert(0, 'c');
            Assert.Equal('c', object4[0]);
            Assert.Equal('a', object4[1]);

            var object5 = new List<double>();
            Assert.Equal(0, object5.Count);
            object5.Add(15.3e4);
            object5.Insert(0, 11.2);
            object5.Insert(0, 124.21);
            Assert.Equal(124.21, object5[0]);
            Assert.Equal(11.2, object5[1]);
        }

        [Fact]
        public void ClearMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add(22);
            object1.Add(10);
            object1.Add(346);
            object1.Add(734);
            object1.Insert(2, 745);
            object1.Insert(4, 234);
            object1.Insert(1, 61);
            object1.Clear();
            Assert.Equal(0, object1.Count);
        }

        [Fact]
        public void IsReadOnlyMethodReturnsFalse()
        {
            var object1 = new List<int>();
            object1.Add(15);
            object1.Add(22);
            object1.Add(10);
            Assert.False(object1.IsReadOnly);
        }

        [Fact]
        public void CopyToMethodWorksAccordingly()
        {
            int[] array = new int[4];
            var object1 = new List<int>();
            object1.Add(15);
            object1.Add(22);
            object1.Add(10);
            object1.Add(235);
            object1.CopyTo(array, 0);
            Assert.Equal(15, array[0]);
            Assert.Equal(22, array[1]);
            Assert.Equal(10, array[2]);
            Assert.Equal(235, array[3]);
        }

        [Fact]
        public void RemoveMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add(22);
            object1.Add(16);
            object1.Add(69);
            object1.Add(47);
            object1.Insert(2, 37);
            object1.Insert(4, 7457);
            object1.Insert(1, 66);
            object1.Remove(69);
            object1.Remove(7457);

            Assert.Equal(15, object1[0]);
            Assert.Equal(66, object1[1]);
            Assert.Equal(22, object1[2]);
            Assert.Equal(37, object1[3]);
            Assert.Equal(16, object1[4]);
            Assert.Equal(47, object1[5]);

        }

        [Fact]
        public void RemoveAtMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add(22);
            object1.Add(16);
            object1.Add(69);
            object1.Add(47);
            object1.Insert(2, 37);
            object1.Insert(4, 7457);
            object1.Insert(1, 66);
            object1.RemoveAt(4);
            object1.RemoveAt(1);


            Assert.Equal(15, object1[0]);
            Assert.Equal(22, object1[1]);
            Assert.Equal(37, object1[2]);
            Assert.Equal(7457, object1[3]);
            Assert.Equal(69, object1[4]);
            Assert.Equal(47, object1[5]);
        }
    }
}
