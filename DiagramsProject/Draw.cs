using System;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public static class Draw
    {
        public static void DrawAndFillRectangleAndText(Rectangle rectangle)
        {
            EnsureIsNotNull(rectangle, nameof(rectangle));
            rectangle.Graphics.FillRectangle(rectangle.Styling.ShapeBrush, rectangle.Bounds.X, rectangle.Bounds.Y, rectangle.Bounds.Width, rectangle.Bounds.Height);
            rectangle.Graphics.DrawRectangle(rectangle.Styling.DrawPen, rectangle.Bounds.X, rectangle.Bounds.Y, rectangle.Bounds.Width, rectangle.Bounds.Height);
            rectangle.Graphics.DrawString(rectangle.Text.ActualText, rectangle.Styling.DrawFont, rectangle.Styling.TextBrush, rectangle.Text.Position, rectangle.Text.Format);
        }

        public static void DrawAndFillCircleAndText(Circle circle)
        {
            EnsureIsNotNull(circle, nameof(circle));
            circle.Graphics.FillEllipse(circle.Styling.ShapeBrush, circle.Bounds.X, circle.Bounds.Y, circle.Bounds.Width, circle.Bounds.Height);
            circle.Graphics.DrawEllipse(circle.Styling.DrawPen, circle.Bounds.X, circle.Bounds.Y, circle.Bounds.Width, circle.Bounds.Height);
            circle.Graphics.DrawString(circle.Text.ActualText, circle.Styling.DrawFont, circle.Styling.TextBrush, circle.Text.Position, circle.Text.Format);
        }

        public static void DrawAndFillShapePathAndText(Shape shape, GraphicsPath shapePath)
        {
            EnsureIsNotNull(shape, nameof(shape));
            shape.Graphics.FillPath(shape.Styling.ShapeBrush, shapePath);
            shape.Graphics.DrawPath(shape.Styling.DrawPen, shapePath);
            shape.Graphics.DrawString(shape.Text.ActualText, shape.Styling.DrawFont, shape.Styling.TextBrush, shape.Text.Position, shape.Text.Format);
        }

        internal static void EnsureIsNotNull<T>(T source, string name)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
        }
    }
}
