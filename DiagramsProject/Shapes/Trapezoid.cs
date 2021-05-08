using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Trapezoid : Shape
    {
        const float ShapeEndWidth = 23;
        readonly bool isUpsideDown;

        public Trapezoid(Graphics graphics, string text, Styling styling, PointF position, bool isUpsideDown) : base(graphics, text, styling, position)
        {
            this.isUpsideDown = isUpsideDown;
            Width = Text.Width + ShapeEndWidth + ShapeEndWidth;
            Height = Text.Height;
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            Text.Position = new PointF(Position.X + halfWidth, Position.Y + halfHeight);
            Bounds = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void DrawShape()
        {
            using GraphicsPath trapezoidPath = new GraphicsPath();

            if (isUpsideDown)
            {
                trapezoidPath.AddLine(Bounds.Left, Bounds.Top, Bounds.Right, Bounds.Top);
                trapezoidPath.AddLine(Bounds.Right, Bounds.Top, Bounds.Right - ShapeEndWidth, Bounds.Bottom);
                trapezoidPath.AddLine(Bounds.Right - ShapeEndWidth, Bounds.Bottom, Bounds.Left + ShapeEndWidth, Bounds.Bottom);
                trapezoidPath.CloseFigure();
            }
            else
            {
                trapezoidPath.AddLine(Bounds.Left + ShapeEndWidth, Bounds.Top, Bounds.Right - ShapeEndWidth, Bounds.Top);
                trapezoidPath.AddLine(Bounds.Right - ShapeEndWidth, Bounds.Top, Bounds.Right, Bounds.Bottom);
                trapezoidPath.AddLine(Bounds.Right, Bounds.Bottom, Bounds.Left, Bounds.Bottom);
                trapezoidPath.CloseFigure();
            }

            Draw.DrawAndFillShapePathAndText(this, trapezoidPath);
        }
    }
}
