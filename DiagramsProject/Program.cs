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
            draw.SubroutineShape(1200, 100, drawString, fancyStyling);

            // Asymmetric shape
            draw.AsymmetricShape(950, 300, drawString, basicStyling, false);

            // Asymmetric shape reversed
            draw.AsymmetricShape(1200, 300, drawString, basicStyling, true);

            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
