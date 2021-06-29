using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public static class Draw
    {
        public static void DrawAndFillRectangleAndText(Rectangle rectangle)
        {
            ProjectUtils.EnsureIsNotNull(rectangle, nameof(rectangle));
            rectangle.Graphics.FillRectangle(rectangle.Styling.ShapeBrush, rectangle.Bounds.X, rectangle.Bounds.Y, rectangle.Bounds.Width, rectangle.Bounds.Height);
            rectangle.Graphics.DrawRectangle(rectangle.Styling.ShapePen, rectangle.Bounds.X, rectangle.Bounds.Y, rectangle.Bounds.Width, rectangle.Bounds.Height);
            rectangle.Graphics.DrawString(rectangle.Text.ActualText, rectangle.Styling.DrawFont, rectangle.Styling.TextBrush, rectangle.Text.Position, rectangle.Text.Format);
        }

        public static void DrawAndFillCircleAndText(Circle circle)
        {
            ProjectUtils.EnsureIsNotNull(circle, nameof(circle));
            circle.Graphics.FillEllipse(circle.Styling.ShapeBrush, circle.Bounds.X, circle.Bounds.Y, circle.Bounds.Width, circle.Bounds.Height);
            circle.Graphics.DrawEllipse(circle.Styling.ShapePen, circle.Bounds.X, circle.Bounds.Y, circle.Bounds.Width, circle.Bounds.Height);
            circle.Graphics.DrawString(circle.Text.ActualText, circle.Styling.DrawFont, circle.Styling.TextBrush, circle.Text.Position, circle.Text.Format);
        }

        public static void DrawAndFillShapePathAndText(Shape shape, GraphicsPath shapePath)
        {
            ProjectUtils.EnsureIsNotNull(shape, nameof(shape));
            shape.Graphics.FillPath(shape.Styling.ShapeBrush, shapePath);
            shape.Graphics.DrawPath(shape.Styling.ShapePen, shapePath);
            shape.Graphics.DrawString(shape.Text.ActualText, shape.Styling.DrawFont, shape.Styling.TextBrush, shape.Text.Position, shape.Text.Format);
        }

        public static void DrawLink(Graphics graphics, Shape source, Shape destination, sbyte linkType, Styling styling, string text = "")
        {
            ProjectUtils.EnsureIsNotNull(graphics, nameof(graphics));
            ProjectUtils.EnsureIsNotNull(source, nameof(source));
            ProjectUtils.EnsureIsNotNull(destination, nameof(destination));
            ProjectUtils.EnsureIsNotNull(styling, nameof(styling));
            const float divider = 2;
            PointF firstShapeCenter = new PointF(
                source.Bounds.X + source.Bounds.Width / divider,
                source.Bounds.Y + source.Bounds.Height / divider);
            PointF secondShapeCenter = new PointF(
                destination.Bounds.X + destination.Bounds.Width / divider,
                destination.Bounds.Y + destination.Bounds.Height / divider);

            PointF intersetion1 = ProjectUtils.FindIntersectionOfTwoLines(
                firstShapeCenter,
                secondShapeCenter,
                new PointF(source.Bounds.Left, source.Bounds.Bottom),
                new PointF(source.Bounds.Right, source.Bounds.Bottom));

            PointF intersetion2 = ProjectUtils.FindIntersectionOfTwoLines(
                firstShapeCenter,
                secondShapeCenter,
                new PointF(destination.Bounds.Left, destination.Bounds.Top),
                new PointF(destination.Bounds.Right, destination.Bounds.Top));

            if (linkType == 1)
            {
                styling.ShapePen.CustomEndCap = new AdjustableArrowCap(styling.ShapePen.Width + 2, styling.ShapePen.Width + 2);
            }

            if (linkType == 2)
            {
                styling.ShapePen.CustomStartCap = new AdjustableArrowCap(styling.ShapePen.Width + 2, styling.ShapePen.Width + 2);
                styling.ShapePen.CustomEndCap = new AdjustableArrowCap(styling.ShapePen.Width + 2, styling.ShapePen.Width + 2);
            }

            graphics.DrawLine(styling.ShapePen, intersetion2, intersetion1);

            if (text == "")
            {
                return;
            }

            SizeF stringSize = graphics.MeasureString(text, styling.DrawFont);
            PointF textPosition = new PointF(
                firstShapeCenter.X + (secondShapeCenter.X - firstShapeCenter.X) / divider - stringSize.Width / divider,
                source.Bounds.Bottom + (destination.Bounds.Top - source.Bounds.Bottom) / divider - stringSize.Height / divider);
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
