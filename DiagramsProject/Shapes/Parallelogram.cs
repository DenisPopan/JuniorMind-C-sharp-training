using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Parallelogram : Shape
    {
        const float ShapeEndWidth = 20;
        readonly bool isLeftOriented;

        public Parallelogram(Graphics graphics, string text, Styling styling, bool isLeftOriented) : base(graphics, text, styling)
        {
            this.isLeftOriented = isLeftOriented;
        }

        public override void Prepare(PointF position)
        {
            Bounds = new RectangleF(position.X, position.Y, Text.Width + ShapeEndWidth + ShapeEndWidth, Text.Height);
            float halfWidth = Bounds.Width / 2;
            float halfHeight = Bounds.Height / 2;
            Text.Position = new PointF(position.X + halfWidth, position.Y + halfHeight);
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
