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
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            // Input and string format and Font
            Console.WriteLine("Input text: ");
            string drawString = Console.ReadLine();
            using Draw draw = new Draw(g);
            using FontFamily fontFamily = new FontFamily("Arial");
            using Styling basicStyling = new Styling();
            using Styling fancyStyling = new Styling(Color.Orange, Color.Green, Color.Purple, new Font(fontFamily, 25));
            using StringFormat drawFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using Font drawFont = new Font("Arial", 25);
            SizeF stringSize = g.MeasureString(drawString, drawFont);

            // Pens and brushes
            using SolidBrush drawBrush = new SolidBrush(Color.Black);
            using SolidBrush blueBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            using Pen blackPen = new Pen(Color.Black);
            SizeF stringSize1 = g.MeasureString(drawString, drawFont);
            Console.WriteLine(stringSize1);

            // Sizes, coordinates and shapes
            const int adjustments = 5;
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
            draw.Rectangle(40, 30, drawString, fancyStyling);

            // circle - (x,y) + distance(default) + radius = circle center
            draw.Circle(centerX - radius, centerY - radius, drawString, basicStyling, radius);

            // rhombus
            draw.Rhombus(700 + radius, 300 - radius, drawString, fancyStyling, radius);

            // rectangle with rounded corners
            draw.RectangleWithRoundedCorners(centerX, 100, drawString, basicStyling);

            // rounded rectangle - jumatate din inaltime sa fie radius la ambele
            draw.RoundedRectangle(900, 100, drawString, basicStyling);

            // subroutine shape
            Rectangle drawRect3 = new Rectangle(1200, 100, (int)Math.Ceiling(stringSize1.Width) + 20, (int)Math.Ceiling(stringSize1.Height) + 10);
            g.FillRectangle(blueBrush, drawRect3);
            g.DrawRectangle(blackPen, drawRect3);
            g.DrawLine(blackPen, drawRect3.X + 10, drawRect3.Y, drawRect3.X + 10, drawRect3.Bottom);
            g.DrawLine(blackPen, drawRect3.Right - 10, drawRect3.Y, drawRect3.Right - 10, drawRect3.Bottom);
            g.DrawString(drawString, drawFont, drawBrush, drawRect3, drawFormat);

            // Asymmetric shape
            int tenPercentOfHeight = (int)Math.Ceiling(0.6 * stringSize1.Height);
            using GraphicsPath asymmetricPath = new GraphicsPath();
            asymmetricPath.AddLine(950, 300, 950 + stringSize1.Width + 20 + tenPercentOfHeight, 300);
            asymmetricPath.AddLine(950 + stringSize1.Width + 20 + tenPercentOfHeight, 300, 950 + stringSize1.Width + 20 + tenPercentOfHeight, 300 + stringSize1.Height + 10);
            asymmetricPath.AddLine(950 + stringSize1.Width + 20 + tenPercentOfHeight, 300 + stringSize1.Height + 10, 950, 300 + stringSize1.Height + 10);
            asymmetricPath.AddLine(950, 300 + stringSize1.Height + 10, 950 + tenPercentOfHeight, 300 + (stringSize1.Height + 10) / 2);
            asymmetricPath.CloseFigure();

            g.FillPath(blueBrush, asymmetricPath);
            g.DrawString(drawString, drawFont, drawBrush, 950 + tenPercentOfHeight + (stringSize1.Width + 20) / 2, 300 + (stringSize1.Height + 10) / 2, drawFormat);
            g.DrawPath(blackPen, asymmetricPath);

            // Asymmetric shape reversed
            using GraphicsPath asymmetricPathReversed = AsymmetricPathReversed(stringSize1);
            g.FillPath(blueBrush, asymmetricPathReversed);
            g.DrawString(drawString, drawFont, drawBrush, 1200 + (stringSize1.Width + 20) / 2, 300 + (stringSize1.Height + 10) / 2, drawFormat);
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
