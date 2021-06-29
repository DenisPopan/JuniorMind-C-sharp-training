using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class RoundedRectangle : Shape
    {
        public RoundedRectangle(Graphics graphics, string text, Styling styling) : base(graphics, text, styling)
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
            float diameter = Text.Height;
            float radius = diameter / 2;
            const int ninetyDegrees = 90;
            const int oneEightyDegrees = 180;
            const int twoSeventyDegrees = 270;
            var px = Bounds.X;
            var py = Bounds.Y;

            using GraphicsPath path = new GraphicsPath();
            path.AddArc(px, py, diameter, diameter, ninetyDegrees, oneEightyDegrees);
            path.AddLine(px + radius, py, px + Text.Width - radius, py);
            path.AddArc(px + Text.Width - diameter, py, diameter, diameter, twoSeventyDegrees, oneEightyDegrees);
            path.AddLine(px + radius, py + diameter, px + Text.Width - radius, py + diameter);
            path.CloseFigure();

            Draw.DrawAndFillShapePathAndText(this, path);
        }
    }
}
