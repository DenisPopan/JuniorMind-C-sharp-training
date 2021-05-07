using System.Drawing;

namespace DiagramsProject
{
    public class SubroutineShape : Shape
    {
        public SubroutineShape(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Text.Width;
            Height = Text.Height;
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            Text.Position = new PointF(Position.X + halfWidth, Position.Y + halfHeight);
        }

        public override void DrawShape()
        {
            const float lineDistance = 12;
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Text.Width, Text.Height);
            new Rectangle(Graphics, Text.ActualText, Styling, new PointF(Position.X, Position.Y)).DrawShape();
            Graphics.DrawLine(Styling.DrawPen, rectangle.X + lineDistance, rectangle.Y, rectangle.X + lineDistance, rectangle.Bottom);
            Graphics.DrawLine(Styling.DrawPen, rectangle.Right - lineDistance, rectangle.Y, rectangle.Right - lineDistance, rectangle.Bottom);
        }
    }
}
