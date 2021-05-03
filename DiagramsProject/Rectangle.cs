using System.Drawing;

namespace DiagramsProject
{
    public class Rectangle : Shape
    {
        public Rectangle(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = TextWidth;
            Height = TextHeight;
        }

        public override void DrawShape()
        {
            Draw.DrawAndFillRectangleAndText(this);
        }
    }
}
