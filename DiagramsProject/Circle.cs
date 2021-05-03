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
            Center = new PointF(Position.X + diameter / 2, Position.Y + diameter / 2);
            Text.Position = Center;
        }

        public PointF Center { get; }

        public override void DrawShape()
        {
            Draw.DrawAndFillCircleAndText(this);
        }
    }
}
