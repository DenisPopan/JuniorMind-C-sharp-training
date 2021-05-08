using System;
using System.Drawing;

namespace DiagramsProject
{
    public class Circle : Shape
    {
        public Circle(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling)
        {
            float diameter = Math.Max(Text.Width, Text.Height);
            float radius = diameter / 2;
            Center = new PointF(position.X + radius, position.Y + radius);
            Text.Position = Center;
            Bounds = new RectangleF(position.X, position.Y, diameter, diameter);
        }

        public PointF Center { get; }

        public override void DrawShape()
        {
            Draw.DrawAndFillCircleAndText(this);
        }
    }
}
