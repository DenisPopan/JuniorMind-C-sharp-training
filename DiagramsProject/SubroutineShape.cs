using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class SubroutineShape : Shape
    {
        public SubroutineShape(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Text.Width;
            Height = Text.Height;
            Text.Position = new PointF(Position.X + Width / 2, Position.Y + Height / 2);
        }

        public override void DrawShape()
        {
            const int lineDistance = 12;
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Text.Width, Text.Height);
            new Rectangle(Graphics, Text.ActualText, Styling, new PointF(Position.X, Position.Y)).DrawShape();
            Graphics.DrawLine(Styling.DrawPen, rectangle.X + lineDistance, rectangle.Y, rectangle.X + lineDistance, rectangle.Bottom);
            Graphics.DrawLine(Styling.DrawPen, rectangle.Right - lineDistance, rectangle.Y, rectangle.Right - lineDistance, rectangle.Bottom);
        }
    }
}
