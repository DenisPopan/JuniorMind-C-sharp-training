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

        public void Rhombus(float px, float py, string text, Styling styling, float radius)
        {
            EnsureIsNotNull(styling, nameof(styling));
            float diameter = radius * 2;
            using GraphicsPath rhombusPath = new GraphicsPath();
            rhombusPath.AddLines(new[]
            {
                new PointF(px, py),
                new PointF(px + radius, py + radius),
                new PointF(px, py + diameter),
                new PointF(px - radius, py + radius)
            });
            rhombusPath.CloseFigure();
            graphics.FillPath(styling.ShapeBrush, rhombusPath);
            graphics.DrawPath(styling.DrawPen, rhombusPath);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px, py + radius, textFormat);
        }

        public void RectangleWithRoundedCorners(float px, float py, string text, Styling styling, float radius = 8)
        {
            EnsureIsNotNull(styling, nameof(styling));
            float diameter = radius * 2;
            const int ninetyDegrees = 90;
            const int oneEightyDegrees = 180;
            const int twoSeventyDegrees = 270;
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            float fixedWidth = textSize.Width + WidthAdjustment;
            float fixedHeight = textSize.Height + HeightAdjustment;

            using GraphicsPath path = new GraphicsPath();
            path.AddArc(px, py, diameter, diameter, oneEightyDegrees, ninetyDegrees);
            path.AddLine(px + radius, py, px + fixedWidth - radius, py);
            path.AddArc(px + fixedWidth - diameter, py, diameter, diameter, twoSeventyDegrees, ninetyDegrees);
            path.AddLine(px + fixedWidth, py + radius, px + fixedWidth, py + fixedHeight - radius);
            path.AddArc(px + fixedWidth - diameter, py + fixedHeight - diameter, diameter, diameter, 0, ninetyDegrees);
            path.AddLine(px + radius, py + fixedHeight, px + fixedWidth - radius, py + fixedHeight);
            path.AddArc(px, py + fixedHeight - diameter, diameter, diameter, ninetyDegrees, ninetyDegrees);
            path.CloseFigure();

            graphics.FillPath(styling.ShapeBrush, path);
            graphics.DrawPath(styling.DrawPen, path);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px + fixedWidth / 2, py + fixedHeight / 2, textFormat);
        }

        public void RoundedRectangle(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            RectangleWithRoundedCorners(px, py, text, styling, (textSize.Height + HeightAdjustment) / 2);
        }

        public void SubroutineShape(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            const int lineDistance = 12;
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            RectangleF rectangle = new RectangleF(px, py, textSize.Width + WidthAdjustment, textSize.Height + HeightAdjustment);
            Rectangle(px, py, text, styling);
            graphics.DrawLine(styling.DrawPen, rectangle.X + lineDistance, rectangle.Y, rectangle.X + lineDistance, rectangle.Bottom);
            graphics.DrawLine(styling.DrawPen, rectangle.Right - lineDistance, rectangle.Y, rectangle.Right - lineDistance, rectangle.Bottom);
        }

        public void AsymmetricShape(float px, float py, string text, Styling styling, bool isReversed)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            float specialEndWidth = 0.6f * textSize.Height;
            using GraphicsPath asymmetricPath = new GraphicsPath();
            RectangleF rectangle = new RectangleF(px, py, textSize.Width + WidthAdjustment + specialEndWidth, textSize.Height + HeightAdjustment);

            if (!isReversed)
            {
                asymmetricPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                asymmetricPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                asymmetricPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                asymmetricPath.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left + specialEndWidth, rectangle.Bottom - rectangle.Height / 2);
                asymmetricPath.CloseFigure();
            }
            else
            {
                asymmetricPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Left, rectangle.Top);
                asymmetricPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom);
                asymmetricPath.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Right, rectangle.Bottom);
                asymmetricPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Right - specialEndWidth, rectangle.Bottom - rectangle.Height / 2);
                asymmetricPath.CloseFigure();
            }

            graphics.FillPath(styling.ShapeBrush, asymmetricPath);
            graphics.DrawPath(styling.DrawPen, asymmetricPath);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, !isReversed ? px + specialEndWidth + rectangle.Width / 2 : px + rectangle.Width / 2, py + rectangle.Height / 2, textFormat);
        }

        public void Hexagon(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            const float specialEndWidth = 12;

            using GraphicsPath hexagonPath = new GraphicsPath();
            RectangleF rectangle = new RectangleF(px, py, textSize.Width + WidthAdjustment, textSize.Height + HeightAdjustment);
            hexagonPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
            hexagonPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right + specialEndWidth, rectangle.Top + rectangle.Height / 2);
            hexagonPath.AddLine(rectangle.Right + specialEndWidth, rectangle.Top + rectangle.Height / 2, rectangle.Right, rectangle.Bottom);
            hexagonPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
            hexagonPath.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left - specialEndWidth, rectangle.Bottom - rectangle.Height / 2);
            hexagonPath.CloseFigure();

            graphics.FillPath(styling.ShapeBrush, hexagonPath);
            graphics.DrawPath(styling.DrawPen, hexagonPath);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px + rectangle.Width / 2, py + rectangle.Height / 2, textFormat);
        }

        public void Parallelogram(float px, float py, string text, Styling styling, bool isReversed)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF textSize = graphics.MeasureString(text, styling.DrawFont);
            const float shapeEndWidth = 20;
            RectangleF rectangle = new RectangleF(px, py, textSize.Width + WidthAdjustment + 2 * shapeEndWidth, textSize.Height + HeightAdjustment);
            using GraphicsPath paralelogramPath = new GraphicsPath();

            if (!isReversed)
            {
                paralelogramPath.AddLine(rectangle.Left + shapeEndWidth, rectangle.Top, rectangle.Right, rectangle.Top);
                paralelogramPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right - shapeEndWidth, rectangle.Bottom);
                paralelogramPath.AddLine(rectangle.Right - shapeEndWidth, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                paralelogramPath.CloseFigure();
            }
            else
            {
                paralelogramPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - shapeEndWidth, rectangle.Top);
                paralelogramPath.AddLine(rectangle.Right - shapeEndWidth, rectangle.Top, rectangle.Right, rectangle.Bottom);
                paralelogramPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left + shapeEndWidth, rectangle.Bottom);
                paralelogramPath.CloseFigure();
            }

            graphics.FillPath(styling.ShapeBrush, paralelogramPath);
            graphics.DrawPath(styling.DrawPen, paralelogramPath);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px + rectangle.Width / 2, py + rectangle.Height / 2, textFormat);
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
