using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class PieChart
    {
        public void PreparePieChart()
        {
            var styling = new Styling();
            using Bitmap bmp = new Bitmap(1920, 1080);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            Console.WriteLine("Enter a title: ");

            using var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using FontFamily fontFamily = new FontFamily("Arial");
            using var drawFont = new Font(fontFamily, 30);

            g.DrawString(
                Console.ReadLine(),
                drawFont,
                styling.TextBrush,
                new PointF(700, 100));

            DrawPieChart(g, styling, format);

            bmp.Save(@"C:\Users\popan\Desktop\image2.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public void DrawPieChart(Graphics g, Styling styling, StringFormat format)
        {
            ProjectUtils.EnsureIsNotNull(g, nameof(g));
            ProjectUtils.EnsureIsNotNull(styling, nameof(styling));
            ProjectUtils.EnsureIsNotNull(format, nameof(format));

            Console.WriteLine("Enter strings and numbers and finish by entering a point(.)");
            var input = Console.ReadLine();
            List<string> words = new List<string>();
            List<double> numbers = new List<double>();
            double num;
            string[] splitInput;
            double total = 0;
            while (input != ".")
            {
                splitInput = input.Split(" ");
                if (double.TryParse(splitInput[0], out num))
                {
                    Console.WriteLine("The input string value is not valid!");
                    return;
                }

                words.Add(splitInput[0]);
                if (double.TryParse(splitInput[1], out num))
                {
                    numbers.Add(num);
                    total += num;
                }
                else
                {
                    Console.WriteLine("The input numeric value is not valid!");
                    return;
                }

                input = Console.ReadLine();
            }

            const double Deg2Rad = Math.PI / 180.0;
            const float diameter = 600;
            const float radius = diameter / 2f;
            var center = new PointF(600 + radius, 300 + radius);
            RectangleF bounds = new RectangleF(600, 300, diameter, diameter);

            double angleX = radius * Math.Cos(0);
            double angleY = radius * Math.Sin(0);
            double startAngle = 0;

            using var pen = new Pen(Color.Black);
            pen.Width = 4;
            var brush = new SolidBrush(Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256)));

            double percent;
            GraphicsPath path;
            int fixer = 60;

            var stringAngleX = 0.0;
            var stringAngleY = 0.0;

            for (int i = 0; i < words.Count; i++)
            {
                brush = new SolidBrush(Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256)));
                path = new GraphicsPath();

                percent = numbers[i] / total;

                path.AddLine(center, new PointF((float)(center.X + angleX), (float)(center.Y + angleY)));
                path.AddArc(600, 300, diameter, diameter, (float)startAngle, (float)percent * 360);
                angleX = radius * Math.Cos((startAngle + percent * 360) * Deg2Rad);
                angleY = radius * Math.Sin((startAngle + percent * 360) * Deg2Rad);
                stringAngleX = radius * Math.Cos((startAngle + percent * 360 / 2) * Deg2Rad);
                stringAngleY = radius * Math.Sin((startAngle + percent * 360 / 2) * Deg2Rad);
                path.AddLine(center, new PointF((float)(center.X + angleX), (float)(center.Y + angleY)));

                g.DrawPath(pen, path);
                g.FillPath(brush, path);
                g.FillRectangle(brush, bounds.Right + 100, bounds.Top + fixer, 30, 30);
                g.DrawRectangle(pen, bounds.Right + 100, bounds.Top + fixer, 30, 30);

                g.DrawString(
                    Math.Round(percent * 100, 2) + "%",
                    styling.DrawFont,
                    styling.TextBrush,
                    new PointF((float)(center.X + stringAngleX / 1.5), (float)(center.Y + stringAngleY / 1.5)),
                    format);
                g.DrawString(
                    words[i],
                    styling.DrawFont,
                    styling.TextBrush,
                    new PointF(bounds.Right + 145, bounds.Top + fixer));

                fixer += 40;
                startAngle += percent * 360;

                path.Dispose();
            }

            brush.Dispose();
        }
    }
}
