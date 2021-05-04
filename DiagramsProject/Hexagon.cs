using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Hexagon : Shape
    {
        const float SpecialEndWidth = 12;

        public Hexagon(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Text.Width + 2 * SpecialEndWidth;
            Height = Text.Height;
            Text.Position = new PointF(Position.X + Width / 2, Position.Y + Height / 2);
        }

        public override void DrawShape()
        {
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Width, Height);

            using GraphicsPath hexagonPath = new GraphicsPath();
            hexagonPath.AddLine(rectangle.Left, rectangle.Top + rectangle.Height / 2, rectangle.Left + SpecialEndWidth, rectangle.Top);
            hexagonPath.AddLine(rectangle.Left + SpecialEndWidth, rectangle.Top, rectangle.Right - SpecialEndWidth, rectangle.Top);
            hexagonPath.AddLine(rectangle.Right - SpecialEndWidth, rectangle.Top, rectangle.Right, rectangle.Top + rectangle.Height / 2);
            hexagonPath.AddLine(rectangle.Right, rectangle.Top + rectangle.Height / 2, rectangle.Right - SpecialEndWidth, rectangle.Bottom);
            hexagonPath.AddLine(rectangle.Right - SpecialEndWidth, rectangle.Bottom, rectangle.Left + SpecialEndWidth, rectangle.Bottom);
            hexagonPath.CloseFigure();

            Draw.DrawAndFillShapePathAndText(this, hexagonPath);
        }
    }
}
