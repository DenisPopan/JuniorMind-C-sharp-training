using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    class Program
    {
        static void Main()
        {
            // Initialising bitmap and Graphics
            Console.WriteLine("Input text: ");
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            // Input and string format and Font
            string drawString1 = Console.ReadLine();

            using StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            using Font drawFont = new Font("Arial", 25);

            // Pens and brushes
            using SolidBrush drawBrush = new SolidBrush(Color.Black);
            using SolidBrush blueBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            using Pen blackPen = new Pen(Color.Black);
            SizeF stringSize1 = g.MeasureString(drawString1, drawFont);
            Console.WriteLine(stringSize1);

            // Sizes, coordinates and shapes
            const int adjustments = 5;
            const int x = 40;
            const int y = 30;
            const int centerX = 400;
            const int centerY = 300;
            int dif = Math.Abs((int)stringSize1.Width - (int)stringSize1.Height);
            int maxLength = Math.Max((int)Math.Ceiling(stringSize1.Width), (int)Math.Ceiling(stringSize1.Height));
            int radius = maxLength / 2 + adjustments;
            maxLength += (int)(0.2 * maxLength);
            if (maxLength % 2 != 0)
            {
                maxLength++;
            }

            Rectangle drawRect1 = new Rectangle(x, y, (int)Math.Ceiling(stringSize1.Width) + 20, (int)Math.Ceiling(stringSize1.Height) + 10);
            Rectangle drawRect2 = new Rectangle(
                x + drawRect1.Width / 2 - (int)Math.Ceiling(stringSize1.Width) / 2,
                y + 300,
                (int)Math.Ceiling(stringSize1.Width) + 20,
                (int)Math.Ceiling(stringSize1.Height) + 10);

            // Rhombus
            using GraphicsPath rhombusPath = new GraphicsPath();
            rhombusPath.AddLines(new[]
            {
                new Point(700, 300),
                new Point(700 + maxLength / 2, 300 - maxLength / 2),
                new Point(700 + maxLength, 300),
                new Point(700 + maxLength / 2, 300 + maxLength / 2),
                new Point(700, 300)
            });

            // Drawing
            // simple rectangle
            g.FillRectangle(blueBrush, drawRect1);
            g.DrawRectangle(blackPen, drawRect1);
            g.DrawString(drawString1, drawFont, drawBrush, drawRect1, drawFormat);

            g.FillRectangle(blueBrush, drawRect2);
            g.DrawRectangle(blackPen, drawRect2);
            g.DrawString(drawString1, drawFont, drawBrush, drawRect2, drawFormat);

            // line between rectangles
            g.DrawLine(blackPen, drawRect1.Left + drawRect1.Width / 2, drawRect1.Bottom, drawRect2.Left + drawRect2.Width / 2, drawRect2.Top);

            // circle
            g.FillEllipse(blueBrush, centerX - radius, centerY - radius, radius * 2, radius * 2);
            g.DrawEllipse(blackPen, centerX - radius, centerY - radius, radius * 2, radius * 2);
            g.DrawString(drawString1, drawFont, drawBrush, centerX, centerY, drawFormat);

            // rhombus
            g.FillPath(blueBrush, rhombusPath);
            g.DrawPath(blackPen, rhombusPath);
            g.DrawString(drawString1, drawFont, drawBrush, 700 + maxLength / 2, 300, drawFormat);

            // rectangle with rounded corners
            DrawAndFillRoundedRectangle(g, centerX, 100, stringSize1.Width + 20, stringSize1.Height + 10, 8, 8);
            g.DrawString(drawString1, drawFont, drawBrush, centerX + (stringSize1.Width + 20) / 2, 100 + (stringSize1.Height + 10) / 2, drawFormat);

            // rounded rectangle - jumatate din inaltime sa fie radius la ambele
            DrawAndFillRoundedRectangle(g, 900, 100, stringSize1.Width + 20, stringSize1.Height + 10, (stringSize1.Height + 10) / 2, (stringSize1.Height + 10) / 2);
            g.DrawString(drawString1, drawFont, drawBrush, 900 + (stringSize1.Width + 20) / 2, 100 + (stringSize1.Height + 10) / 2, drawFormat);

            // subroutine shape
            Rectangle drawRect3 = new Rectangle(1200, 100, (int)Math.Ceiling(stringSize1.Width) + 20, (int)Math.Ceiling(stringSize1.Height) + 10);
            g.FillRectangle(blueBrush, drawRect3);
            g.DrawRectangle(blackPen, drawRect3);
            g.DrawLine(blackPen, drawRect3.X + 10, drawRect3.Y, drawRect3.X + 10, drawRect3.Bottom);
            g.DrawLine(blackPen, drawRect3.Right - 10, drawRect3.Y, drawRect3.Right - 10, drawRect3.Bottom);
            g.DrawString(drawString1, drawFont, drawBrush, drawRect3, drawFormat);

            // Asymmetric shape
            int tenPercentOfHeight = (int)Math.Ceiling(0.6 * stringSize1.Height);
            using GraphicsPath asymmetricPath = new GraphicsPath();
            asymmetricPath.AddLine(950, 300, 950 + stringSize1.Width + 20 + tenPercentOfHeight, 300);
            asymmetricPath.AddLine(950 + stringSize1.Width + 20 + tenPercentOfHeight, 300, 950 + stringSize1.Width + 20 + tenPercentOfHeight, 300 + stringSize1.Height + 10);
            asymmetricPath.AddLine(950 + stringSize1.Width + 20 + tenPercentOfHeight, 300 + stringSize1.Height + 10, 950, 300 + stringSize1.Height + 10);
            asymmetricPath.AddLine(950, 300 + stringSize1.Height + 10, 950 + tenPercentOfHeight, 300 + (stringSize1.Height + 10) / 2);
            asymmetricPath.CloseFigure();

            g.FillPath(blueBrush, asymmetricPath);
            g.DrawString(drawString1, drawFont, drawBrush, 950 + tenPercentOfHeight + (stringSize1.Width + 20) / 2, 300 + (stringSize1.Height + 10) / 2, drawFormat);
            g.DrawPath(blackPen, asymmetricPath);

            // Asymmetric shape reversed
            using GraphicsPath asymmetricPathReversed = AsymmetricPathReversed(stringSize1);
            g.FillPath(blueBrush, asymmetricPathReversed);
            g.DrawString(drawString1, drawFont, drawBrush, 1200 + (stringSize1.Width + 20) / 2, 300 + (stringSize1.Height + 10) / 2, drawFormat);
            g.DrawPath(blackPen, asymmetricPathReversed);

            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        static GraphicsPath AsymmetricPathReversed(SizeF stringSize1)
        {
            int tenPercentOfHeight = (int)Math.Ceiling(0.6 * stringSize1.Height);
            GraphicsPath asymmetricPathReversed = new GraphicsPath();
            asymmetricPathReversed.AddLine(1200 + stringSize1.Width + 20 + tenPercentOfHeight, 300, 1200, 300);
            asymmetricPathReversed.AddLine(1200, 300, 1200, 300 + stringSize1.Height + 10);
            asymmetricPathReversed.AddLine(1200, 300 + stringSize1.Height + 10, 1200 + stringSize1.Width + 20 + tenPercentOfHeight, 300 + stringSize1.Height + 10);
            asymmetricPathReversed.AddLine(1200 + stringSize1.Width + 20 + tenPercentOfHeight, 300 + stringSize1.Height + 10, 1200 + stringSize1.Width + 20, 300 + (stringSize1.Height + 10) / 2);
            asymmetricPathReversed.CloseFigure();
            return asymmetricPathReversed;
        }

        static void DrawAndFillRoundedRectangle(Graphics g, float x, float y, float w, float h, float rx, float ry)
        {
            using Pen blackPen = new Pen(Color.Black);
            using GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, rx + rx, ry + ry, 180, 90);
            path.AddLine(x + rx, y, x + w - rx, y);
            path.AddArc(x + w - 2 * rx, y, 2 * rx, 2 * ry, 270, 90);
            path.AddLine(x + w, y + ry, x + w, y + h - ry);
            path.AddArc(x + w - 2 * rx, y + h - 2 * ry, rx + rx, ry + ry, 0, 90);
            path.AddLine(x + rx, y + h, x + w - rx, y + h);
            path.AddArc(x, y + h - 2 * ry, 2 * rx, 2 * ry, 90, 90);
            path.CloseFigure();
            using SolidBrush blueBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            g.FillPath(blueBrush, path);
            g.DrawPath(blackPen, path);
        }
    }
}
