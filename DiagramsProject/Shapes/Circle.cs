using System;
using System.Drawing;

namespace DiagramsProject
{
    public class Circle : Shape
    {
        public Circle(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            float diameter = Math.Max(Text.Width, Text.Height);
            Width = diameter;
            Height = diameter;
            float radius = diameter / 2;
            Center = new PointF(Position.X + radius, Position.Y + radius);
            Text.Position = Center;
            Bounds = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public PointF Center { get; }

        public override void DrawShape()
        {
            Draw.DrawAndFillCircleAndText(this);
        }
    }
}
