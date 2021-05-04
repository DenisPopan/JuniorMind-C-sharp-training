using System;
using System.Drawing;

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

            // circle - (x,y) + distance(default) + radius = circle center
            new Circle(g, drawString, basicStyling, new PointF(380, 200)).DrawShape();

            // rhombus
            new Rhombus(g, drawString, fancyStyling, new PointF(700, 200)).DrawShape();

            // rectangle with rounded corners
            new RectangleWithRoundedCorners(g, drawString, basicStyling, new PointF(430, 100)).DrawShape();

            // rounded rectangle - jumatate din inaltime sa fie radius la ambele
            new RoundedRectangle(g, drawString, basicStyling, new PointF(900, 100)).DrawShape();

            // subroutine shape
            new SubroutineShape(g, drawString, fancyStyling, new PointF(1200, 100)).DrawShape();

            // Asymmetric shape
            new AsymmetricShape(g, drawString, basicStyling, new PointF(950, 300), true).DrawShape();

            // Asymmetric shape reversed
            new AsymmetricShape(g, drawString, basicStyling, new PointF(1200, 300), false).DrawShape();

            // Hexagon
            new Hexagon(g, drawString, basicStyling, new PointF(100, 500)).DrawShape();

            // Normal paralelogram
            new Parallelogram(g, drawString, fancyStyling, new PointF(350, 500), true).DrawShape();

            // Reversed paralelogram
            new Parallelogram(g, drawString, basicStyling, new PointF(600, 500), false).DrawShape();

            // Basic Trapezoid
            new Trapezoid(g, drawString, basicStyling, new PointF(900, 500), false).DrawShape();

            // Reversed Trapezoid
            new Trapezoid(g, drawString, fancyStyling, new PointF(1100, 500), true).DrawShape();

            // Cylinder
            new Cylinder(g, drawString, basicStyling, new PointF(80, 250)).DrawShape();

            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}