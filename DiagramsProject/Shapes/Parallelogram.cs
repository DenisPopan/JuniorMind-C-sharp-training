using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Parallelogram : Shape
    {
        const float ShapeEndWidth = 20;
        readonly bool isLeftOriented;

        public Parallelogram(Graphics graphics, string text, Styling styling, PointF position, bool isLeftOriented) : base(graphics, text, styling, position)
        {
            this.isLeftOriented = isLeftOriented;
            Width = Text.Width + ShapeEndWidth + ShapeEndWidth;
            Height = Text.Height;
            float halfWidth = Width / 2;
            float halfHeight = Height / 2;
            Text.Position = new PointF(Position.X + halfWidth, Position.Y + halfHeight);
            Bounds = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void DrawShape()
        {
            using GraphicsPath parallelogramPath = new GraphicsPath();

            if (isLeftOriented)
            {
                parallelogramPath.AddLine(Bounds.Left, Bounds.Top, Bounds.Right - ShapeEndWidth, Bounds.Top);
                parallelogramPath.AddLine(Bounds.Right - ShapeEndWidth, Bounds.Top, Bounds.Right, Bounds.Bottom);
                parallelogramPath.AddLine(Bounds.Right, Bounds.Bottom, Bounds.Left + ShapeEndWidth, Bounds.Bottom);
                parallelogramPath.CloseFigure();
            }
            else
            {
                parallelogramPath.AddLine(Bounds.Left + ShapeEndWidth, Bounds.Top, Bounds.Right, Bounds.Top);
                parallelogramPath.AddLine(Bounds.Right, Bounds.Top, Bounds.Right - ShapeEndWidth, Bounds.Bottom);
                parallelogramPath.AddLine(Bounds.Right - ShapeEndWidth, Bounds.Bottom, Bounds.Left, Bounds.Bottom);
                parallelogramPath.CloseFigure();
            }

            Draw.DrawAndFillShapePathAndText(this, parallelogramPath);
        }
    }
}
