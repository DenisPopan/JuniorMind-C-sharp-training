using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class AsymmetricShape : Shape
    {
        readonly bool isReversed;
        readonly float specialEndWidth;

        public AsymmetricShape(Graphics graphics, string text, Styling styling, PointF position, bool isReversed) : base(graphics, text, styling, position)
        {
            this.isReversed = isReversed;
            specialEndWidth = 0.6f * (Text.Height - 10);
            Width = Text.Width + specialEndWidth;
            Height = Text.Height;
            Text.Position = new PointF(!isReversed ? Position.X + specialEndWidth + (Width - specialEndWidth) / 2 : Position.X + (Width - specialEndWidth) / 2, Position.Y + Height / 2);
        }

        public override void DrawShape()
        {
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Width, Height);
            using GraphicsPath path = new GraphicsPath();
            if (!isReversed)
            {
                path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left + specialEndWidth, rectangle.Bottom - rectangle.Height / 2);
                path.CloseFigure();
            }
            else
            {
                path.AddLine(rectangle.Right, rectangle.Top, rectangle.Left, rectangle.Top);
                path.AddLine(rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom);
                path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Right - specialEndWidth, rectangle.Bottom - rectangle.Height / 2);
                path.CloseFigure();
            }

            Draw.DrawAndFillShapePathAndText(this, path);
        }
    }
}
