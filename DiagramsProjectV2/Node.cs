using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DiagramsProjectV2
{
    public class Node
    {
        int level;

        public Node(string id, string text, Flowchart flowchart)
        {
            Id = id;
            Text = text;
            Flowchart = flowchart;

            SizeF stringSize = Canva.Graphics.MeasureString(Text, BasicStyling.Font);
            Width = stringSize.Width + BasicStyling.WidthAdjustment;
            Height = stringSize.Height + BasicStyling.HeightAdjustment;
        }

        public string Id { get; }

        public RectangleF Rectangle { get; set; }

        public string Text { get; }

        public float Width { get; }

        public float ChildrenWidth { get; set; }

        public float Height { get; }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
                foreach (var child in GetChildren())
                {
                    child.Level = level + 1;
                }
            }
        }

        public Node Parent { get; set; }

        Flowchart Flowchart { get; }

        public List<Node> GetChildren()
        {
            return Flowchart.Nodes.Where(x => x.Level > 1 && x.Parent.Equals(this)).ToList();
        }

        public int GetChildrenCount()
        {
            return Flowchart.Nodes.Count(x => x.Level > 1 && x.Parent.Equals(this));
        }
    }
}
