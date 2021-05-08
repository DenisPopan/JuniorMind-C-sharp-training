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
            Bounds = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void DrawShape()
        {
            const float lineDistance = 12;
            new Rectangle(Graphics, Text.ActualText, Styling, new PointF(Position.X, Position.Y)).DrawShape();
            Graphics.DrawLine(Styling.DrawPen, Bounds.X + lineDistance, Bounds.Y, Bounds.X + lineDistance, Bounds.Bottom);
            Graphics.DrawLine(Styling.DrawPen, Bounds.Right - lineDistance, Bounds.Y, Bounds.Right - lineDistance, Bounds.Bottom);
        }
    }
}
