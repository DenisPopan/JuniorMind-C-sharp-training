using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class AsymmetricShape : Shape
    {
        readonly bool isLeftOriented;
        readonly float specialEndWidth;

        public AsymmetricShape(Graphics graphics, string text, Styling styling, PointF position, bool isLeftOriented) : base(graphics, text, styling, position)
        {
            this.isLeftOriented = isLeftOriented;
            specialEndWidth = 0.6f * (Text.Height - 10);
            Width = Text.Width + specialEndWidth;
            Height = Text.Height;
            Text.Position = new PointF(isLeftOriented ? Position.X + specialEndWidth + (Width - specialEndWidth) / 2 : Position.X + (Width - specialEndWidth) / 2, Position.Y + Height / 2);
            Bounds = new RectangleF(Position.X, Position.Y, Width, Height);
        }

        public override void DrawShape()
        {
            RectangleF rectangle = new RectangleF(Position.X, Position.Y, Width, Height);
            using GraphicsPath path = new GraphicsPath();
            if (isLeftOriented)
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
