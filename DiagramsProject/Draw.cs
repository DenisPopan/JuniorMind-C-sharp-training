using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Draw : IDisposable
    {
        const float WidthAdjustment = 20;
        const float HeightAdjustment = 10;
        readonly StringFormat textFormat;
        readonly Graphics graphics;

        public Draw(Graphics graphics)
        {
            this.graphics = graphics;
            textFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rectangle(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            RectangleF rectangle = new RectangleF(px, py, textSize.Width + WidthAdjustment, textSize.Height + HeightAdjustment);
            graphics.FillRectangle(styling.ShapeBrush, px, py, textSize.Width + WidthAdjustment, textSize.Height + HeightAdjustment);
            graphics.DrawRectangle(styling.DrawPen, px, py, textSize.Width + WidthAdjustment, textSize.Height + HeightAdjustment);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, rectangle, textFormat);
        }

        public void Circle(float centerX, float centerY, string text, Styling styling, float radius)
        {
            EnsureIsNotNull(styling, nameof(styling));
            float diameter = radius * 2;
            graphics.FillEllipse(styling.ShapeBrush, centerX, centerY, diameter, diameter);
            graphics.DrawEllipse(styling.DrawPen, centerX, centerY, diameter, diameter);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, centerX + radius, centerY + radius, textFormat);
        }

        internal static void EnsureIsNotNull<T>(T source, string name)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
        }

        protected virtual void Dispose(bool disposing)
        {
            textFormat.Dispose();
            graphics.Dispose();
        }
    }
}
