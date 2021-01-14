using System;

namespace Linq
{
    public class ProductEventArgs : EventArgs
    {
        public string Name { get; set; }

        internal int Quantity { get; set; }
    }
}
