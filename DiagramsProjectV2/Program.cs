using System;
using System.Drawing;
using System.IO;

namespace DiagramsProjectV2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] commands;
            try
            {
                commands = File.ReadAllLines(args[0]);
            }
            catch (IOException)
            {
                Console.WriteLine("File path does not exist or it is incorrect!");
                return;
            }

            // Initialising bitmap and styling class instance
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            var styling = new Styling();
            styling.Graphics = g;

            string[] nodes = commands[1].Split(" --- ");

            DrawSimpleRectangle(nodes[0], new PointF(50, 50), styling);
            DrawSimpleRectangle(nodes[1], new PointF(50, 150), styling);

            bmp.Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }

        static void DrawSimpleRectangle(string text, PointF position, Styling styling)
        {
            SizeF stringSize = styling.Graphics.MeasureString(text, styling.Font);

            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
            var rectangle = new RectangleF(position.X, position.Y, stringSize.Width + widthAdjustment, stringSize.Height + heightAdjustment);

            // Drawing
            styling.Graphics.FillRectangle(styling.ShapeBrush, rectangle);
            styling.Graphics.DrawRectangle(styling.ShapePen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            styling.Graphics.DrawString(
                text,
                styling.Font,
                styling.TextBrush,
                rectangle.X + (stringSize.Width + widthAdjustment) / 2,
                rectangle.Y + (stringSize.Height + heightAdjustment) / 2,
                styling.Format);
        }
    }
}
