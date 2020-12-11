using System.Collections.Generic;

namespace Linq
{
    public class Product
    {
        public string Name { get; set; }

        public ICollection<Feature> Features { get; set; }
    }
}
