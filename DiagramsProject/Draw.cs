using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public static class Draw
    {
        public static void DrawAndFillRectangleAndText(Rectangle rectangle)
        {
            EnsureIsNotNull(rectangle, nameof(rectangle));
            rectangle.Graphics.FillRectangle(rectangle.Styling.ShapeBrush, rectangle.Position.X, rectangle.Position.Y, rectangle.Width, rectangle.Height);
            rectangle.Graphics.DrawRectangle(rectangle.Styling.DrawPen, rectangle.Position.X, rectangle.Position.Y, rectangle.Width, rectangle.Height);
            rectangle.Graphics.DrawString(rectangle.Text, rectangle.Styling.DrawFont, rectangle.Styling.TextBrush, rectangle.Position.X + rectangle.Width / 2, rectangle.Position.Y + rectangle.Height / 2, rectangle.TextFormat);
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
