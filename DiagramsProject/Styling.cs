using System.Drawing;

namespace DiagramsProject
{
    public class Styling
    {
        public Styling()
        {
            ShapeBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            ShapePen = new Pen(Color.Black);
            TextBrush = new SolidBrush(Color.Black);
            using FontFamily fontFamily = new FontFamily("Arial");
            DrawFont = new Font(fontFamily, 23);
        }

        public Styling(Color shapeBrushColor, Color textBrushColor, Color penColor, Font drawFont)
        {
            ShapeBrush = new SolidBrush(shapeBrushColor);
            ShapePen = new Pen(penColor);
            TextBrush = new SolidBrush(textBrushColor);
            this.DrawFont = drawFont;
        }

        public SolidBrush ShapeBrush { get; }

        public Pen ShapePen { get; }

        public SolidBrush TextBrush { get; }

        public Font DrawFont { get; }
    }
}
