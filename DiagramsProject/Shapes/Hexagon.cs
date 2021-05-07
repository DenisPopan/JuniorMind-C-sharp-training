using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Hexagon : Shape
    {
        const float SpecialEndWidth = 12;
        readonly float halfHeight;

        public Hexagon(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Text.Width + SpecialEndWidth + SpecialEndWidth;
            Height = Text.Height;
            const float divider = 2;
            float halfWidth = Width / divider;
            halfHeight = Height / divider;
            Text.Position = new PointF(Position.X + halfWidth, Position.Y + halfHeight);
        }

        public override void DrawShape()
        {
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Width, Height);

            using GraphicsPath hexagonPath = new GraphicsPath();
            hexagonPath.AddLine(rectangle.Left, rectangle.Top + halfHeight, rectangle.Left + SpecialEndWidth, rectangle.Top);
            hexagonPath.AddLine(rectangle.Left + SpecialEndWidth, rectangle.Top, rectangle.Right - SpecialEndWidth, rectangle.Top);
            hexagonPath.AddLine(rectangle.Right - SpecialEndWidth, rectangle.Top, rectangle.Right, rectangle.Top + halfHeight);
            hexagonPath.AddLine(rectangle.Right, rectangle.Top + halfHeight, rectangle.Right - SpecialEndWidth, rectangle.Bottom);
            hexagonPath.AddLine(rectangle.Right - SpecialEndWidth, rectangle.Bottom, rectangle.Left + SpecialEndWidth, rectangle.Bottom);
            hexagonPath.CloseFigure();

            Draw.DrawAndFillShapePathAndText(this, hexagonPath);
        }
    }
}
