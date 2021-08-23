﻿using System.Collections.Generic;
using System.Drawing;

namespace DiagramsProjectV2
{
    public class Node
    {
        readonly List<Node> children;
        int level;

        public Node(int id, string text)
        {
            Id = id;
            Text = text;
            children = new List<Node>();
        }

        public int Id { get; }

        public RectangleF Rectangle { get; private set; }

        public string Text { get; }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
                foreach (var child in children)
                {
                    child.Level = level + 1;
                }
            }
        }

        public Node Parent { get; set; }

        public void Build(Graphics graphics, float x, float y)
        {
            ProjectUtils.EnsureIsNotNull(graphics, nameof(graphics));
            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
            using FontFamily fontFamily = new FontFamily("Arial");
            using var font = new Font(fontFamily, 23);
            SizeF stringSize = graphics.MeasureString(Text, font);
            Rectangle = new RectangleF(x, y, stringSize.Width + widthAdjustment, stringSize.Height + heightAdjustment);
        }

        public void AddChild(Node child)
        {
            children.Add(child);
        }

        public List<Node> GetChildren()
        {
            return children;
        }
    }
}
