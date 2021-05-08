using System.Drawing;

namespace DiagramsProject
{
    public class Cylinder : Shape
    {
        readonly RectangleF ellipseRectangle;
        readonly RectangleF middleRectangle;
        readonly RectangleF arcRectangle;

        public Cylinder(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling)
        {
            float ellipseHeight = 10 + 0.15f * Text.Width;
            const int divider = 2;
            ellipseRectangle = new RectangleF(position.X, position.Y, Text.Width, ellipseHeight);
            middleRectangle = new RectangleF(position.X, position.Y + ellipseRectangle.Height / divider, Text.Width, ellipseRectangle.Height / divider + Text.Height);
            arcRectangle = new RectangleF(position.X, middleRectangle.Bottom - ellipseHeight / divider, Text.Width, ellipseHeight);
            Text.Position = new PointF(position.X + Text.Width / divider, middleRectangle.Top + ellipseRectangle.Height / divider + Text.Height / divider);
            Bounds = new RectangleF(position.X, position.Y, Text.Width, ellipseRectangle.Height / divider + middleRectangle.Height + arcRectangle.Height / divider);
        }

        public override void DrawShape()
        {
            const int oneEightyDegrees = 180;
            Graphics.FillEllipse(Styling.ShapeBrush, ellipseRectangle);
            Graphics.FillRectangle(Styling.ShapeBrush, middleRectangle);
            Graphics.FillEllipse(Styling.ShapeBrush, arcRectangle);

            Graphics.DrawEllipse(Styling.DrawPen, ellipseRectangle);
            Graphics.DrawArc(Styling.DrawPen, arcRectangle, 0, oneEightyDegrees);
            Graphics.DrawLine(Styling.DrawPen, Bounds.X, middleRectangle.Top, Bounds.X, middleRectangle.Bottom);
            Graphics.DrawLine(Styling.DrawPen, middleRectangle.Right, middleRectangle.Top, middleRectangle.Right, middleRectangle.Bottom);
            Graphics.DrawString(Text.ActualText, Styling.DrawFont, Styling.TextBrush, Text.Position, Text.Format);
        }
    }
}
