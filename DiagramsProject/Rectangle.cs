using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace DiagramsProject
{
    public class Rectangle : Shape
    {
        public Rectangle(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = TextWidth;
            Height = TextHeight;
        }

        public override void DrawShape()
        {
            Draw.DrawAndFillRectangleAndText(this);
        }
    }
}
