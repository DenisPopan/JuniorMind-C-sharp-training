using System.Drawing;

namespace DiagramsProject
{
    public abstract class Shape
    {
        protected Shape(Graphics graphics, string text, Styling styling, PointF position)
        {
            Draw.EnsureIsNotNull(styling, nameof(styling));
            Draw.EnsureIsNotNull(graphics, nameof(graphics));
            Text = new DrawnText(text, graphics.MeasureString(text, styling.DrawFont));
            Graphics = graphics;
            Styling = styling;
            Position = position;
        }

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

        public PointF Position { get; }

        public float Width { get; protected set; }

        public float Height { get; protected set; }

        public abstract void DrawShape();
    }
}
