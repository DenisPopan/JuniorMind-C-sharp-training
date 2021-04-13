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
            SizeF stringSize = g.MeasureString(drawString, basicStyling.DrawFont);

            // Sizes, coordinates and shapes
            const int centerX = 400;
            const int centerY = 300;
            float maxLength = Math.Max(stringSize.Width, stringSize.Height) + 20;
            float radius = maxLength / 2;
            Console.WriteLine(radius);

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

            // Hexagon
            draw.Hexagon(200, 500, drawString, basicStyling);

            draw.Dispose();

            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}