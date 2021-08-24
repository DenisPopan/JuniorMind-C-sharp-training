using System;
using System.Drawing;
using System.IO;

namespace DiagramsProjectV2
{
    public static class Program
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

            var flowchart = new Flowchart(commands);

            flowchart.Nodes[0].Build(g, 50, 50);
            flowchart.Nodes[1].Build(g, 50, 150);

            DrawSimpleRectangle(flowchart.Nodes[0].Text, flowchart.Nodes[0].Rectangle, styling);
            DrawSimpleRectangle(flowchart.Nodes[1].Text, flowchart.Nodes[1].Rectangle, styling);
            DrawLink(flowchart.Nodes[0].Rectangle, flowchart.Nodes[1].Rectangle, styling);

            bmp.Save(args[1], System.Drawing.Imaging.ImageFormat.Png);
        }

        static void DrawSimpleRectangle(string text, RectangleF rectangle, Styling styling)
        {
            SizeF stringSize = styling.Graphics.MeasureString(text, styling.Font);

            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
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

        static void DrawLink(RectangleF rect1, RectangleF rect2, Styling styling)
        {
            styling.Graphics.DrawLine(styling.ShapePen, rect1.Left + rect1.Width / 2, rect1.Bottom, rect2.Left + rect2.Width / 2, rect2.Top);
        }
    }
}
