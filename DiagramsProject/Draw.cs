using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Draw : IDisposable
    {
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
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);

            graphics.FillRectangle(styling.ShapeBrush, px, py, shapeSize.Width, shapeSize.Height);
            graphics.DrawRectangle(styling.DrawPen, px, py, shapeSize.Width, shapeSize.Height);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px + shapeSize.Width / 2, py + shapeSize.Height / 2, textFormat);
        }

        public void Circle(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            float diameter = Math.Max(shapeSize.Width, shapeSize.Height);
            float radius = diameter / 2;
            float centerX = px + radius;
            float centerY = py + radius;
            graphics.FillEllipse(styling.ShapeBrush, centerX, centerY, diameter, diameter);
            graphics.DrawEllipse(styling.DrawPen, centerX, centerY, diameter, diameter);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, centerX + radius, centerY + radius, textFormat);
        }

        public void Rhombus(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            float diameter = Math.Max(shapeSize.Width, shapeSize.Height);
            float radius = diameter / 2;
            px += radius;
            using GraphicsPath rhombusPath = new GraphicsPath();
            rhombusPath.AddLines(new[]
            {
                new PointF(px, py),
                new PointF(px + radius, py + radius),
                new PointF(px, py + diameter),
                new PointF(px - radius, py + radius)
            });
            rhombusPath.CloseFigure();

            DrawAndFillPathAndString(rhombusPath, text, styling, px, py + radius);
        }

        public void RectangleWithRoundedCorners(float px, float py, string text, Styling styling, float radius = 8)
        {
            EnsureIsNotNull(styling, nameof(styling));
            float diameter = radius * 2;
            const int ninetyDegrees = 90;
            const int oneEightyDegrees = 180;
            const int twoSeventyDegrees = 270;
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);

            using GraphicsPath path = new GraphicsPath();
            path.AddArc(px, py, diameter, diameter, oneEightyDegrees, ninetyDegrees);
            path.AddLine(px + radius, py, px + shapeSize.Width - radius, py);
            path.AddArc(px + shapeSize.Width - diameter, py, diameter, diameter, twoSeventyDegrees, ninetyDegrees);
            path.AddLine(px + shapeSize.Width, py + radius, px + shapeSize.Width, py + shapeSize.Height - radius);
            path.AddArc(px + shapeSize.Width - diameter, py + shapeSize.Height - diameter, diameter, diameter, 0, ninetyDegrees);
            path.AddLine(px + radius, py + shapeSize.Height, px + shapeSize.Height - radius, py + shapeSize.Height);
            path.AddArc(px, py + shapeSize.Height - diameter, diameter, diameter, ninetyDegrees, ninetyDegrees);
            path.CloseFigure();

            DrawAndFillPathAndString(path, text, styling, px + shapeSize.Width / 2, py + shapeSize.Height / 2);
        }

        public void RoundedRectangle(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            float diameter = shapeSize.Height;
            float radius = diameter / 2;
            const int ninetyDegrees = 90;
            const int oneEightyDegrees = 180;
            const int twoSeventyDegrees = 270;

            using GraphicsPath path = new GraphicsPath();
            path.AddArc(px, py, diameter, diameter, ninetyDegrees, oneEightyDegrees);
            path.AddLine(px + radius, py, px + shapeSize.Width - radius, py);
            path.AddArc(px + shapeSize.Width - diameter, py, diameter, diameter, twoSeventyDegrees, oneEightyDegrees);
            path.AddLine(px + radius, py + diameter, px + shapeSize.Width - radius, py + diameter);
            path.CloseFigure();

            DrawAndFillPathAndString(path, text, styling, px + shapeSize.Width / 2, py + shapeSize.Height / 2);
        }

        public void SubroutineShape(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            const int lineDistance = 12;
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            RectangleF rectangle = new RectangleF(px, py, shapeSize.Width, shapeSize.Height);
            Rectangle(px, py, text, styling);
            graphics.DrawLine(styling.DrawPen, rectangle.X + lineDistance, rectangle.Y, rectangle.X + lineDistance, rectangle.Bottom);
            graphics.DrawLine(styling.DrawPen, rectangle.Right - lineDistance, rectangle.Y, rectangle.Right - lineDistance, rectangle.Bottom);
        }

        public void AsymmetricShape(float px, float py, string text, Styling styling, bool isReversed)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            float specialEndWidth = 0.6f * shapeSize.Height;
            FixShapeSize(ref shapeSize);
            using GraphicsPath asymmetricPath = new GraphicsPath();
            RectangleF rectangle = new RectangleF(px, py, shapeSize.Width + specialEndWidth, shapeSize.Height);

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

            DrawAndFillPathAndString(asymmetricPath, text, styling, !isReversed ? px + specialEndWidth + (rectangle.Width - specialEndWidth) / 2 : px + (rectangle.Width - specialEndWidth) / 2, py + rectangle.Height / 2);
        }

        public void Hexagon(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            const float specialEndWidth = 12;
            RectangleF rectangle = new RectangleF(px, py, shapeSize.Width, shapeSize.Height);

            using GraphicsPath hexagonPath = new GraphicsPath();
            hexagonPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
            hexagonPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right + specialEndWidth, rectangle.Top + rectangle.Height / 2);
            hexagonPath.AddLine(rectangle.Right + specialEndWidth, rectangle.Top + rectangle.Height / 2, rectangle.Right, rectangle.Bottom);
            hexagonPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
            hexagonPath.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left - specialEndWidth, rectangle.Bottom - rectangle.Height / 2);
            hexagonPath.CloseFigure();

            DrawAndFillPathAndString(hexagonPath, text, styling, px + rectangle.Width / 2, py + rectangle.Height / 2);
        }

        public void Parallelogram(float px, float py, string text, Styling styling, bool isReversed)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            const float shapeEndWidth = 20;
            RectangleF rectangle = new RectangleF(px, py, shapeSize.Width + 2 * shapeEndWidth, shapeSize.Height);
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

            DrawAndFillPathAndString(paralelogramPath, text, styling, px + rectangle.Width / 2, py + rectangle.Height / 2);
        }

        public void Trapezoid(float px, float py, string text, Styling styling, bool isReversed)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            const float shapeEndWidth = 23;
            RectangleF rectangle = new RectangleF(px, py, shapeSize.Width + 2 * shapeEndWidth, shapeSize.Height);
            using GraphicsPath trapezoidPath = new GraphicsPath();

            if (!isReversed)
            {
                trapezoidPath.AddLine(rectangle.Left + shapeEndWidth, rectangle.Top, rectangle.Right - shapeEndWidth, rectangle.Top);
                trapezoidPath.AddLine(rectangle.Right - shapeEndWidth, rectangle.Top, rectangle.Right, rectangle.Bottom);
                trapezoidPath.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                trapezoidPath.CloseFigure();
            }
            else
            {
                trapezoidPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                trapezoidPath.AddLine(rectangle.Right, rectangle.Top, rectangle.Right - shapeEndWidth, rectangle.Bottom);
                trapezoidPath.AddLine(rectangle.Right - shapeEndWidth, rectangle.Bottom, rectangle.Left + shapeEndWidth, rectangle.Bottom);
                trapezoidPath.CloseFigure();
            }

            DrawAndFillPathAndString(trapezoidPath, text, styling, px + rectangle.Width / 2, py + rectangle.Height / 2);
        }

        public void Cylinder(float px, float py, string text, Styling styling)
        {
            EnsureIsNotNull(styling, nameof(styling));
            SizeF shapeSize = graphics.MeasureString(text, styling.DrawFont);
            FixShapeSize(ref shapeSize);
            float ellipseHeight = 10 + 0.15f * shapeSize.Width;
            RectangleF ellipseRectangle = new RectangleF(px, py, shapeSize.Width, ellipseHeight);
            RectangleF middleRectangle = new RectangleF(px, py + ellipseRectangle.Height / 2, shapeSize.Width, ellipseRectangle.Height / 2 + shapeSize.Height);
            RectangleF arcRectangle = new RectangleF(px, middleRectangle.Bottom - ellipseHeight / 2, shapeSize.Width, ellipseHeight);

            graphics.FillEllipse(styling.ShapeBrush, ellipseRectangle);
            graphics.FillRectangle(styling.ShapeBrush, middleRectangle);
            graphics.FillEllipse(styling.ShapeBrush, arcRectangle);

            graphics.DrawEllipse(styling.DrawPen, ellipseRectangle);
            graphics.DrawArc(styling.DrawPen, arcRectangle, 0, 180);
            graphics.DrawLine(styling.DrawPen, px, middleRectangle.Top, px, middleRectangle.Bottom);
            graphics.DrawLine(styling.DrawPen, middleRectangle.Right, middleRectangle.Top, middleRectangle.Right, middleRectangle.Bottom);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px + shapeSize.Width / 2, middleRectangle.Top + ellipseRectangle.Height / 2 + shapeSize.Height / 2, textFormat);
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

        void DrawAndFillPathAndString(GraphicsPath path, string text, Styling styling, float px, float py)
        {
            graphics.FillPath(styling.ShapeBrush, path);
            graphics.DrawPath(styling.DrawPen, path);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, px, py, textFormat);
        }

        void FixShapeSize(ref SizeF shapeSize)
        {
            const float widthAdjustment = 20;
            const float heightAdjustment = 10;
            shapeSize.Width += widthAdjustment;
            shapeSize.Height += heightAdjustment;
        }
    }
}
