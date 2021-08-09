using System;
using System.Collections.Generic;
using System.Text;

namespace DiagramsProject
{
    public class FlowchartNode
    {
        public string Id { get; }

        public Shape Shape { get; }

        public string Parent { get; }
    }
}
