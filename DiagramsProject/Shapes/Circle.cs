using System;
using System.Drawing;

namespace DiagramsProject
{
    public class Circle : Shape
    {
        public Circle(Graphics graphics, string text, Styling styling) : base(graphics, text, styling)
        {
        }

        public PointF Center { get; private set; }

        public override void Prepare(PointF position)
        {
            float diameter = Math.Max(Text.Width, Text.Height);
            float radius = diameter / 2;
            Center = new PointF(position.X + radius, position.Y + radius);
            Text.Position = Center;
            Bounds = new RectangleF(position.X, position.Y, diameter, diameter);
        }

        public override void DrawShape()
        {
            Draw.DrawAndFillCircleAndText(this);
        }
    }
}
