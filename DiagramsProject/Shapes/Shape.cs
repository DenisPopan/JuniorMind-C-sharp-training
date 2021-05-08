using System.Drawing;

namespace DiagramsProject
{
    public abstract class Shape
    {
        protected Shape(Graphics graphics, string text, Styling styling)
        {
            Draw.EnsureIsNotNull(styling, nameof(styling));
            Draw.EnsureIsNotNull(graphics, nameof(graphics));
            Text = new DrawnText(text, graphics.MeasureString(text, styling.DrawFont));
            Graphics = graphics;
            Styling = styling;
        }

        public RectangleF Bounds { get; protected set; }

        public DrawnText Text { get; }

        public Graphics Graphics { get; }

        public Styling Styling { get; }

        public abstract void DrawShape();
    }
}
