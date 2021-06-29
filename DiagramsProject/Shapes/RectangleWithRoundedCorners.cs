using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class RectangleWithRoundedCorners : Shape
    {
        public RectangleWithRoundedCorners(Graphics graphics, string text, Styling styling) : base(graphics, text, styling)
        {
        }

        public override void Prepare(PointF position)
        {
            Bounds = new RectangleF(position.X, position.Y, Text.Width, Text.Height);
            float halfWidth = Bounds.Width / 2;
            float halfHeight = Bounds.Height / 2;
            Text.Position = new PointF(position.X + halfWidth, position.Y + halfHeight);
        }

        public override void DrawShape()
        {
            var px = Bounds.X;
            var py = Bounds.Y;
            const float radius = 8;
            const float diameter = 16;
            const int ninetyDegrees = 90;
            const int oneEightyDegrees = 180;
            const int twoSeventyDegrees = 270;

            using GraphicsPath path = new GraphicsPath();
            path.AddArc(px, py, diameter, diameter, oneEightyDegrees, ninetyDegrees);
            path.AddLine(px + radius, py, px + Text.Width - radius, py);
            path.AddArc(px + Text.Width - diameter, py, diameter, diameter, twoSeventyDegrees, ninetyDegrees);
            path.AddLine(px + Text.Width, py + radius, px + Text.Width, py + Text.Height - radius);
            path.AddArc(px + Text.Width - diameter, py + Text.Height - diameter, diameter, diameter, 0, ninetyDegrees);
            path.AddLine(px + radius, py + Text.Height, px + Text.Height - radius, py + Text.Height);
            path.AddArc(px, py + Text.Height - diameter, diameter, diameter, ninetyDegrees, ninetyDegrees);
            path.CloseFigure();

            Draw.DrawAndFillShapePathAndText(this, path);
        }
    }
}
