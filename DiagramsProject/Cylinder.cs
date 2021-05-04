using System.Drawing;

namespace DiagramsProject
{
    public class Cylinder : Shape
    {
        readonly RectangleF ellipseRectangle;
        readonly RectangleF middleRectangle;
        readonly RectangleF arcRectangle;

        public Cylinder(Graphics graphics, string text, Styling styling, PointF position) : base(graphics, text, styling, position)
        {
            float ellipseHeight = 10 + 0.15f * Text.Width;
            ellipseRectangle = new RectangleF(Position.X, Position.Y, Text.Width, ellipseHeight);
            middleRectangle = new RectangleF(Position.X, Position.Y + ellipseRectangle.Height / 2, Text.Width, ellipseRectangle.Height / 2 + Text.Height);
            arcRectangle = new RectangleF(Position.X, middleRectangle.Bottom - ellipseHeight / 2, Text.Width, ellipseHeight);
            Width = Text.Width;
            Height = ellipseRectangle.Height + middleRectangle.Height + arcRectangle.Height;
            Text.Position = new PointF(Position.X + Text.Width / 2, middleRectangle.Top + ellipseRectangle.Height / 2 + Text.Height / 2);
        }

        public override void DrawShape()
        {
            Graphics.FillEllipse(Styling.ShapeBrush, ellipseRectangle);
            Graphics.FillRectangle(Styling.ShapeBrush, middleRectangle);
            Graphics.FillEllipse(Styling.ShapeBrush, arcRectangle);

            Graphics.DrawEllipse(Styling.DrawPen, ellipseRectangle);
            Graphics.DrawArc(Styling.DrawPen, arcRectangle, 0, 180);
            Graphics.DrawLine(Styling.DrawPen, Position.X, middleRectangle.Top, Position.X, middleRectangle.Bottom);
            Graphics.DrawLine(Styling.DrawPen, middleRectangle.Right, middleRectangle.Top, middleRectangle.Right, middleRectangle.Bottom);
            Graphics.DrawString(Text.ActualText, Styling.DrawFont, Styling.TextBrush, Text.Position, Text.Format);
        }
    }
}
