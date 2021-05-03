using System.Drawing;

namespace DiagramsProject
{
    public class Rectangle : Shape
    {
        public Rectangle(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Text.Width;
            Height = Text.Height;
            Text.Position = new PointF(Position.X + Width / 2, Position.Y + Height / 2);
        }

        public override void DrawShape()
        {
            Draw.DrawAndFillRectangleAndText(this);
        }
    }
}
