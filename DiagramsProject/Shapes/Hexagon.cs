using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Hexagon : Shape
    {
        const float SpecialEndWidth = 12;
        readonly float halfHeight;

        public Hexagon(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling)
        {
            Bounds = new RectangleF(position.X, position.Y, Text.Width + SpecialEndWidth + SpecialEndWidth, Text.Height);
            const float divider = 2;
            float halfWidth = Bounds.Width / divider;
            halfHeight = Bounds.Height / divider;
            Text.Position = new PointF(position.X + halfWidth, position.Y + halfHeight);
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
