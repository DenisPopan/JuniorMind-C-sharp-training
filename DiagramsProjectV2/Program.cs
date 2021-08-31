using System;
using System.Drawing;
using System.IO;

namespace DiagramsProjectV2
{
    public static class Program
    {
        public static void DrawSimpleRectangle(string text, RectangleF rectangle)
        {
            Canva.Graphics.FillRectangle(BasicStyling.ShapeBrush, rectangle);
            Canva.Graphics.DrawRectangle(BasicStyling.ShapePen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            Canva.Graphics.DrawString(
                text,
                BasicStyling.Font,
                BasicStyling.TextBrush,
                rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2,
                BasicStyling.Format);
        }

        public static void DrawLink(RectangleF rect1, RectangleF rect2)
        {
            Canva.Graphics.DrawLine(BasicStyling.EdgePen, rect1.Left + rect1.Width / 2, rect1.Bottom, rect2.Left + rect2.Width / 2, rect2.Top);
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

            var flowchart = new Flowchart(commands);
            flowchart.Draw(args[1]);
        }
    }
}
