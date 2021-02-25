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

            string drawString1 = Console.ReadLine();
            drawString1 += "asdas\nasdas";

            string drawString2 = Console.ReadLine();

            using StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            using Font drawFont = new Font("Times New Roman", 20);
            using SolidBrush drawBrush = new SolidBrush(Color.Black);
            SizeF stringSize = g.MeasureString(drawString1, drawFont);

            const int x = 40;
            const int y = 30;
            const int adjustments = 5;

            using SolidBrush blueBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            using Pen blackPen = new Pen(Color.Black);
            Rectangle drawRect1 = new Rectangle(x, y, (int)stringSize.Width + adjustments, (int)stringSize.Height + adjustments);
            Rectangle drawRect2 = new Rectangle(x, y + 300, (int)stringSize.Width + adjustments, (int)stringSize.Height + adjustments);

            g.FillRectangle(blueBrush, drawRect1);
            g.DrawRectangle(blackPen, drawRect1);
            g.DrawString(drawString1, drawFont, drawBrush, drawRect1, drawFormat);

            g.FillRectangle(blueBrush, drawRect2);
            g.DrawRectangle(blackPen, drawRect2);
            g.DrawString(drawString2, drawFont, drawBrush, drawRect2, drawFormat);
            g.DrawLine(blackPen, drawRect1.Left + drawRect1.Width / 2, drawRect1.Bottom, drawRect2.Left + drawRect2.Width / 2, drawRect2.Top);
            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
