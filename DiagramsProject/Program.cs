using DiagramsProject.Shapes;
using QuikGraph;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    class Program
    {
        public delegate void Sure(Graphics graphics, Shape firstShape, Shape secondShape, sbyte linkType, Styling styling, string text = "");

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
            Styling fancyStyling = new Styling(Color.Orange, Color.Green, Color.Purple, new Font(fontFamily, 23));
            Styling subgraphStyling = new Styling(Color.FromArgb(252, 251, 179), Color.Black, Color.Black, new Font(fontFamily, 18));
            using StringFormat drawFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            SizeF stringSize = g.MeasureString(drawString, basicStyling.DrawFont);

            // Drawing
            // simple rectangle
            var rectangle = new Rectangle(g, drawString, fancyStyling);
            rectangle.Prepare(new PointF(60, 100));
            rectangle.DrawShape();

            // circle - (x,y) + distance(default) + radius = circle center
            var circle = new Circle(g, drawString, basicStyling);
            circle.Prepare(new PointF(380, 200));
            circle.DrawShape();

            // rhombus
            var rhombus = new Rhombus(g, drawString, fancyStyling);
            rhombus.Prepare(new PointF(700, 200));
            rhombus.DrawShape();

            // rectangle with rounded corners
            var rectangleRoundedCorners = new RectangleWithRoundedCorners(g, drawString, basicStyling);
            rectangleRoundedCorners.Prepare(new PointF(430, 100));
            rectangleRoundedCorners.DrawShape();

            // rounded rectangle - jumatate din inaltime sa fie radius la ambele
            var roundedRectangle = new RoundedRectangle(g, drawString, basicStyling);
            roundedRectangle.Prepare(new PointF(900, 100));

            // subroutine shape
            var subroutine = new SubroutineShape(g, drawString, fancyStyling);
            subroutine.Prepare(new PointF(1200, 100));
            subroutine.DrawShape();

            // Asymmetric shape
            var asim = new AsymmetricShape(g, drawString, basicStyling, true);
            asim.Prepare(new PointF(950, 300));
            asim.DrawShape();

            // Asymmetric shape reversed
            var asimrev = new AsymmetricShape(g, drawString, basicStyling, false);
            asimrev.Prepare(new PointF(1200, 300));
            asimrev.DrawShape();

            // Hexagon
            var hexagon = new Hexagon(g, drawString, basicStyling);
            hexagon.Prepare(new PointF(100, 500));
            hexagon.DrawShape();

            // Normal paralelogram
            var paralelogram = new Parallelogram(g, drawString, fancyStyling, true);
            paralelogram.Prepare(new PointF(350, 500));
            paralelogram.DrawShape();

            // Reversed paralelogram
            var paralelogram1 = new Parallelogram(g, drawString, basicStyling, false);
            paralelogram1.Prepare(new PointF(600, 500));
            paralelogram1.DrawShape();

            // Basic Trapezoid
            var trapezoid1 = new Trapezoid(g, drawString, basicStyling, false);
            trapezoid1.Prepare(new PointF(900, 500));
            trapezoid1.DrawShape();

            // Reversed Trapezoid
            var trapezoid = new Trapezoid(g, drawString, fancyStyling, true);
            trapezoid.Prepare(new PointF(1100, 500));
            trapezoid.DrawShape();

            // Cylinder
            var cylinder = new Cylinder(g, drawString, basicStyling);
            cylinder.Prepare(new PointF(80, 250));
            cylinder.DrawShape();

            var subgraph = new Subgraph(g, "Subgraph Title", subgraphStyling, new Shape[] { asimrev });
            subgraph.Prepare(new PointF(0, 0));
            subgraph.DrawShape();

            Styling linkStyling = new Styling(Color.Orange, Color.Green, Color.Purple, new Font(fontFamily, 23));
            linkStyling.ShapePen.Width = 3;
            Draw.DrawLink(g, rectangle, subgraph, 2, linkStyling, "text");
            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}