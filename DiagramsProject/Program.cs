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
            const string drawString = "element";
            using FontFamily fontFamily = new FontFamily("Arial");
            Styling basicStyling = new Styling();
            Styling fancyStyling = new Styling(Color.Orange, Color.Green, Color.Purple, new Font(fontFamily, 25));
            using StringFormat drawFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            SizeF stringSize = g.MeasureString(drawString, basicStyling.DrawFont);

            // Sizes, coordinates and shapes
            float maxLength = Math.Max(stringSize.Width, stringSize.Height) + 20;
            float radius = maxLength / 2;

            // Drawing
            // simple rectangle
            new Rectangle(g, drawString, fancyStyling, new PointF(60, 100)).DrawShape();

            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}