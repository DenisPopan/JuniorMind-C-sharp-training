using DiagramsProject.Shapes;
using System.Drawing;
using System.Drawing.Drawing2D;

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
            var rectangle = new Rectangle(g, drawString, fancyStyling, new PointF(60, 100));
            rectangle.DrawShape();

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
            var asim = new AsymmetricShape(g, drawString, basicStyling, new PointF(950, 300), true);
            asim.DrawShape();

            // Asymmetric shape reversed
            var asimrev = new AsymmetricShape(g, drawString, basicStyling, new PointF(1200, 300), false);
            asimrev.DrawShape();

            // Hexagon
            new Hexagon(g, drawString, basicStyling, new PointF(100, 500)).DrawShape();

            // Normal paralelogram
            new Parallelogram(g, drawString, fancyStyling, new PointF(350, 500), true).DrawShape();

            // Reversed paralelogram
            new Parallelogram(g, drawString, basicStyling, new PointF(600, 500), false).DrawShape();

            // Basic Trapezoid
            new Trapezoid(g, drawString, basicStyling, new PointF(900, 500), false).DrawShape();

            // Reversed Trapezoid
            var trapezoid = new Trapezoid(g, drawString, fancyStyling, new PointF(1100, 500), true);
            trapezoid.DrawShape();

            // Cylinder
            var cylinder = new Cylinder(g, drawString, basicStyling, new PointF(80, 250));
            cylinder.DrawShape();

            var subgraph = new Subgraph(g, "Subgraph Title", subgraphStyling, new Shape[] { asimrev });
            subgraph.DrawShape();

            new Link(g, rectangle, trapezoid, 2, new Pen(Color.Black, 3), "text").DrawLink();

            bmp.Save(@"C:\Users\popan\Desktop\image.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}