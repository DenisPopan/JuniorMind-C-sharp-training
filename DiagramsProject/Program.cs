using System;
using System.Drawing;

namespace DiagramsProject
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Input text: ");
            Bitmap bmp = new Bitmap(920, 780);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            string drawString = Console.ReadLine();

            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            const int x = 100;
            const int y = 80;
            const int width = 100;
            const int height = 50;
            RectangleF drawRect = new RectangleF(x, y, width, height);
            SolidBrush abrush = new SolidBrush(Color.FromArgb(161, 177, 247));

            Pen blackPen = new Pen(Color.Black);

            g.FillRectangle(abrush, x, y, width, height);
            g.DrawRectangle(blackPen, x, y, width, height);

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat);

            abrush.Dispose();
            blackPen.Dispose();
            drawFormat.Dispose();
            drawFont.Dispose();
            drawBrush.Dispose();

            g.Dispose();
            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
        }
    }
}
