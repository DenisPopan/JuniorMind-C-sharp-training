using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class RoundedRectangle : Shape
    {
        public RoundedRectangle(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            Width = Text.Width;
            Height = Text.Height;
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            Text.Position = new PointF(Position.X + halfWidth, Position.Y + halfHeight);
        }

        public override void DrawShape()
        {
            float diameter = Text.Height;
            float radius = diameter / 2;
            const int ninetyDegrees = 90;
            const int oneEightyDegrees = 180;
            const int twoSeventyDegrees = 270;
            var px = Position.X;
            var py = Position.Y;

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
