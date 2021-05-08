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
            Bounds = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void DrawShape()
        {
            using GraphicsPath hexagonPath = new GraphicsPath();
            hexagonPath.AddLine(Bounds.Left, Bounds.Top + halfHeight, Bounds.Left + SpecialEndWidth, Bounds.Top);
            hexagonPath.AddLine(Bounds.Left + SpecialEndWidth, Bounds.Top, Bounds.Right - SpecialEndWidth, Bounds.Top);
            hexagonPath.AddLine(Bounds.Right - SpecialEndWidth, Bounds.Top, Bounds.Right, Bounds.Top + halfHeight);
            hexagonPath.AddLine(Bounds.Right, Bounds.Top + halfHeight, Bounds.Right - SpecialEndWidth, Bounds.Bottom);
            hexagonPath.AddLine(Bounds.Right - SpecialEndWidth, Bounds.Bottom, Bounds.Left + SpecialEndWidth, Bounds.Bottom);
            hexagonPath.CloseFigure();

            Draw.DrawAndFillShapePathAndText(this, hexagonPath);
        }
    }
}
