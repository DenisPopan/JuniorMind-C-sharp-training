using System;

namespace DiagramsProject
{
    public static class Draw
    {
        public static void DrawAndFillRectangleAndText(Rectangle rectangle)
        {
            EnsureIsNotNull(rectangle, nameof(rectangle));
            rectangle.Graphics.FillRectangle(rectangle.Styling.ShapeBrush, rectangle.Position.X, rectangle.Position.Y, rectangle.Width, rectangle.Height);
            rectangle.Graphics.DrawRectangle(rectangle.Styling.DrawPen, rectangle.Position.X, rectangle.Position.Y, rectangle.Width, rectangle.Height);
            rectangle.Graphics.DrawString(rectangle.Text.ActualText, rectangle.Styling.DrawFont, rectangle.Styling.TextBrush, rectangle.Text.Position, rectangle.Text.Format);
        }

        public static void DrawAndFillCircleAndText(Circle circle)
        {
            EnsureIsNotNull(circle, nameof(circle));
            circle.Graphics.FillEllipse(circle.Styling.ShapeBrush, circle.Position.X, circle.Position.Y, circle.Width, circle.Height);
            circle.Graphics.DrawEllipse(circle.Styling.DrawPen, circle.Position.X, circle.Position.Y, circle.Width, circle.Height);
            circle.Graphics.DrawString(circle.Text.ActualText, circle.Styling.DrawFont, circle.Styling.TextBrush, circle.Text.Position, circle.Text.Format);
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
