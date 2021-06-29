using System.Drawing;

namespace DiagramsProject
{
    public class Cylinder : Shape
    {
        RectangleF ellipseRectangle;
        RectangleF middleRectangle;
        RectangleF arcRectangle;

        public Cylinder(Graphics graphics, string text, Styling styling) : base(graphics, text, styling)
        {
        }

        public override void Prepare(PointF position)
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

            Graphics.DrawEllipse(Styling.ShapePen, ellipseRectangle);
            Graphics.DrawArc(Styling.ShapePen, arcRectangle, 0, oneEightyDegrees);
            Graphics.DrawLine(Styling.ShapePen, Bounds.X, middleRectangle.Top, Bounds.X, middleRectangle.Bottom);
            Graphics.DrawLine(Styling.ShapePen, middleRectangle.Right, middleRectangle.Top, middleRectangle.Right, middleRectangle.Bottom);
            Graphics.DrawString(Text.ActualText, Styling.DrawFont, Styling.TextBrush, Text.Position, Text.Format);
        }
    }
}
