using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class AsymmetricShape : Shape
    {
        readonly bool isLeftOriented;
        readonly float specialEndWidth;

        public AsymmetricShape(Graphics graphics, string text, Styling styling, bool isLeftOriented) : base(graphics, text, styling)
        {
            this.isLeftOriented = isLeftOriented;
            specialEndWidth = 0.6f * (Text.Height - 10);
        }

        public override void Prepare(PointF position)
        {
            Bounds = new RectangleF(position.X, position.Y, Text.Width + specialEndWidth, Text.Height);
            Text.Position = new PointF(
                isLeftOriented ? position.X + specialEndWidth + (Bounds.Width - specialEndWidth) / 2 : position.X + (Bounds.Width - specialEndWidth) / 2,
                position.Y + Bounds.Height / 2);
        }

        public override void DrawShape()
        {
            using GraphicsPath path = new GraphicsPath();
            if (isLeftOriented)
            {
                path.AddLine(Bounds.Left, Bounds.Top, Bounds.Right, Bounds.Top);
                path.AddLine(Bounds.Right, Bounds.Top, Bounds.Right, Bounds.Bottom);
                path.AddLine(Bounds.Right, Bounds.Bottom, Bounds.Left, Bounds.Bottom);
                path.AddLine(Bounds.Left, Bounds.Bottom, Bounds.Left + specialEndWidth, Bounds.Bottom - Bounds.Height / 2);
                path.CloseFigure();
            }
            else
            {
                path.AddLine(Bounds.Right, Bounds.Top, Bounds.Left, Bounds.Top);
                path.AddLine(Bounds.Left, Bounds.Top, Bounds.Left, Bounds.Bottom);
                path.AddLine(Bounds.Left, Bounds.Bottom, Bounds.Right, Bounds.Bottom);
                path.AddLine(Bounds.Right, Bounds.Bottom, Bounds.Right - specialEndWidth, Bounds.Bottom - Bounds.Height / 2);
                path.CloseFigure();
            }

            Draw.DrawAndFillShapePathAndText(this, path);
        }
    }
}
