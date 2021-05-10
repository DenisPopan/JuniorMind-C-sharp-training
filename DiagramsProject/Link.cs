using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Link
    {
        readonly sbyte linkType;
        readonly string text;
        readonly Pen pen;
        readonly Graphics graphics;

        public Link(Graphics graphics, Shape firstShape, Shape secondShape, sbyte linkType, Pen pen, string text = "")
        {
            FirstShape = firstShape;
            SecondShape = secondShape;
            this.linkType = linkType;
            this.pen = pen;
            this.graphics = graphics;
            this.text = text;
        }

        public Shape FirstShape { get; }

        public Shape SecondShape { get; }

        public void DrawLink()
            {
            const float divider = 2;
            PointF firstShapeCenter = new PointF(
                FirstShape.Bounds.X + FirstShape.Bounds.Width / divider,
                FirstShape.Bounds.Y + FirstShape.Bounds.Height / divider);
            PointF secondShapeCenter = new PointF(
                SecondShape.Bounds.X + SecondShape.Bounds.Width / divider,
                SecondShape.Bounds.Y + SecondShape.Bounds.Height / divider);

            PointF intersetion1 = Utils.FindIntersection(
                firstShapeCenter,
                secondShapeCenter,
                new PointF(SecondShape.Bounds.Left, SecondShape.Bounds.Top),
                new PointF(SecondShape.Bounds.Right, SecondShape.Bounds.Top));

            PointF intersetion2 = Utils.FindIntersection(
                firstShapeCenter,
                secondShapeCenter,
                new PointF(FirstShape.Bounds.Left, FirstShape.Bounds.Bottom),
                new PointF(FirstShape.Bounds.Right, FirstShape.Bounds.Bottom));

            if (linkType == 1)
            {
                pen.CustomEndCap = new AdjustableArrowCap(pen.Width + 2, pen.Width + 2);
            }

            if (linkType == 2)
            {
                pen.CustomStartCap = new AdjustableArrowCap(pen.Width + 2, pen.Width + 2);
                pen.CustomEndCap = new AdjustableArrowCap(pen.Width + 2, pen.Width + 2);
            }

            graphics.DrawLine(pen, intersetion2, intersetion1);

            if (text == "")
            {
                return;
            }

            Styling styling = new Styling();
            SizeF stringSize = graphics.MeasureString(text, styling.DrawFont);
            PointF textPosition = new PointF(
                firstShapeCenter.X + (secondShapeCenter.X - firstShapeCenter.X) / divider - stringSize.Width / divider,
                FirstShape.Bounds.Bottom + (SecondShape.Bounds.Top - FirstShape.Bounds.Bottom) / divider - stringSize.Height / divider);
            graphics.FillRectangle(
                styling.ShapeBrush,
                textPosition.X,
                textPosition.Y,
                stringSize.Width,
                stringSize.Height);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, textPosition);
        }
    }
}
