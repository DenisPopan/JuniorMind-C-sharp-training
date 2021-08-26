using System;
using System.Drawing;
using System.IO;

namespace DiagramsProjectV2
{
    public static class Program
    {
        public static Graphics Graphics { get; set; }

        public static void DrawSimpleRectangle(string text, RectangleF rectangle, Styling styling)
        {
            ProjectUtils.EnsureIsNotNull(styling, nameof(styling));

            Graphics.FillRectangle(styling.ShapeBrush, rectangle);
            Graphics.DrawRectangle(styling.ShapePen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            Graphics.DrawString(
                text,
                styling.Font,
                styling.TextBrush,
                rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2,
                styling.Format);
        }

        public static void DrawLink(RectangleF rect1, RectangleF rect2, Styling styling)
        {
            ProjectUtils.EnsureIsNotNull(styling, nameof(styling));
            Graphics.DrawLine(styling.EdgePen, rect1.Left + rect1.Width / 2, rect1.Bottom, rect2.Left + rect2.Width / 2, rect2.Top);
        }

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
            Graphics = Graphics.FromImage(bmp);
            Graphics.Clear(Color.White);

            var flowchart = new Flowchart(commands);
            flowchart.Draw();

            bmp.Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
