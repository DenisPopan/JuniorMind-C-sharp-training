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
        }

        public override void DrawShape()
        {
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Width, Height);
            using GraphicsPath trapezoidPath = new GraphicsPath();

            if (isUpsideDown)
            {
                trapezoidPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                trapezoidPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right - ShapeEndWidth, rectangle.Bottom);
                trapezoidPath.AddLine(rectangle.Right - ShapeEndWidth, rectangle.Bottom, rectangle.Left + ShapeEndWidth, rectangle.Bottom);
                trapezoidPath.CloseFigure();
            }
            else
            {
                trapezoidPath.AddLine(rectangle.Left + ShapeEndWidth, rectangle.Top, rectangle.Right - ShapeEndWidth, rectangle.Top);
                trapezoidPath.AddLine(rectangle.Right - ShapeEndWidth, rectangle.Top, rectangle.Right, rectangle.Bottom);
                trapezoidPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                trapezoidPath.CloseFigure();
            }

            Draw.DrawAndFillShapePathAndText(this, trapezoidPath);
        }
    }
}
