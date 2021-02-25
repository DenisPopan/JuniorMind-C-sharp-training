using System;
using System.Drawing;

namespace DiagramsProject
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Input text: ");
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            string drawString = Console.ReadLine();
            drawString += "asdas\nasdas";

            using StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            using Font drawFont = new Font("Times New Roman", 20);
            using SolidBrush drawBrush = new SolidBrush(Color.Black);
            SizeF stringSize = g.MeasureString(drawString, drawFont);

            const int x = 40;
            const int y = 30;
            const int adjustments = 5;

            using SolidBrush blueBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            using Pen blackPen = new Pen(Color.Black);
            Rectangle drawRect = new Rectangle(x, y, (int)stringSize.Width + adjustments, (int)stringSize.Height + adjustments);

            g.FillRectangle(blueBrush, drawRect);
            g.DrawRectangle(blackPen, drawRect);
            g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);
            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
