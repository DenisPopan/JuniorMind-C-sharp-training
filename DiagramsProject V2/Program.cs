using System;
using System.Drawing;
using System.IO;

namespace DiagramsProjectV2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the file path:");
            string text;
            try
            {
                text = File.ReadAllText(Console.ReadLine());
            }
            catch (IOException)
            {
                Console.WriteLine("File path does not exist or it is incorrect!");
                return;
            }

            // Initialising bitmap and Graphics
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            // Initialising font, format, brushes, pen and measuring the text
            using FontFamily fontFamily = new FontFamily("Arial");
            using Font font = new (fontFamily, 23);
            SizeF stringSize = g.MeasureString(text, font);
            using var rectangleBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            using var rectanglePen = new Pen(Color.Black);
            using var textBrush = new SolidBrush(Color.Black);
            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
            var rectangle = new RectangleF(50, 50, stringSize.Width + widthAdjustment, stringSize.Height + heightAdjustment);

            // Drawing
            g.FillRectangle(rectangleBrush, rectangle);
            g.DrawRectangle(rectanglePen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            g.DrawString(text, font, textBrush, 50 + (stringSize.Width + widthAdjustment) / 2, 50 + (stringSize.Height + heightAdjustment) / 2, format);

            Console.WriteLine("Enter the file path where the PNG file will be saved:");
            bmp.Save(Console.ReadLine(), System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
