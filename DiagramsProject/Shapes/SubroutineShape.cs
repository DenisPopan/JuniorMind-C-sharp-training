using System.Drawing;

namespace DiagramsProject
{
    public class SubroutineShape : Shape
    {
        public SubroutineShape(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling)
        {
            Bounds = new RectangleF(position.X, position.Y, Text.Width, Text.Height);
            float halfWidth = Bounds.Width / 2;
            float halfHeight = Bounds.Height / 2;
            Text.Position = new PointF(position.X + halfWidth, position.Y + halfHeight);
        }

        public override void DrawShape()
        {
            const float lineDistance = 12;
            new Rectangle(Graphics, Text.ActualText, Styling, new PointF(Bounds.X, Bounds.Y)).DrawShape();
            Graphics.DrawLine(Styling.DrawPen, Bounds.X + lineDistance, Bounds.Y, Bounds.X + lineDistance, Bounds.Bottom);
            Graphics.DrawLine(Styling.DrawPen, Bounds.Right - lineDistance, Bounds.Y, Bounds.Right - lineDistance, Bounds.Bottom);
        }
    }
}
