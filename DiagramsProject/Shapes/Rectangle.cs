using System.Drawing;

namespace DiagramsProject
{
    public class Rectangle : Shape
    {
        public Rectangle(Graphics graphics, string text, Styling styling) : base(graphics, text, styling)
        {
        }

        public override void DrawShape()
        {
            Draw.DrawAndFillRectangleAndText(this);
        }

        public override void Prepare(PointF position)
        {
            Bounds = new RectangleF(position.X, position.Y, Text.Width, Text.Height);
            float halfWidth = Bounds.Width / 2;
            float halfHeight = Bounds.Height / 2;
            Text.Position = new PointF(position.X + halfWidth, position.Y + halfHeight);
        }
    }
}
