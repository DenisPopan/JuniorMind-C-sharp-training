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
            object1.Add(22);
            object1.Add(16);
            object1.Add(101);
            object1.Add(667);
            Assert.True(object1.Contains(15));
            Assert.True(object1.Contains(22));
            Assert.True(object1.Contains(16));
            Assert.True(object1.Contains(101));
            Assert.True(object1.Contains(667));
        }

        [Fact]
        public void IndexOfMethodWorksForAllBaseTypes()
        {
            var object1 = new List<char>();
            Assert.Equal(0, object1.Count);
            object1.Add('a');
            object1.Add('b');
            object1.Add('c');
            object1.Add('d');
            object1.Add('e');
            Assert.Equal(0, object1.IndexOf('a'));
            Assert.Equal(1, object1.IndexOf('b'));
            Assert.Equal(2, object1.IndexOf('c'));
            Assert.Equal(3, object1.IndexOf('d'));
            Assert.Equal(4, object1.IndexOf('e'));
        }

        [Fact]
        public void InsertMethodWorksForAllBaseTypes()
        {
            var object1 = new List<int>();
            Assert.Equal(0, object1.Count);
            object1.Add(15);
            object1.Add(22);
            object1.Add(64);
            object1.Add(16);
            object1.Add(2);
            object1.Insert(2, 79);
            object1.Insert(4, 28);
            object1.Insert(1, 19);
            Assert.Equal(15, object1[0]);
            Assert.Equal(19, object1[1]);
            Assert.Equal(22, object1[2]);
            Assert.Equal(79, object1[3]);
            Assert.Equal(64, object1[4]);
            Assert.Equal(28, object1[5]);
            Assert.Equal(16, object1[6]);
            Assert.Equal(2, object1[7]);
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
