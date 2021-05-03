using System.Drawing;

namespace DiagramsProject
{
    public class Styling
    {
        public Styling()
        {
            ShapeBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            TextBrush = new SolidBrush(Color.Black);
            DrawPen = new Pen(Color.Black);
            using FontFamily fontFamily = new FontFamily("Arial");
            DrawFont = new Font(fontFamily, 25);
        }

        public Styling(Color shapeBrushColor, Color textBrushColor, Color penColor, Font drawFont)
        {
            ShapeBrush = new SolidBrush(shapeBrushColor);
            TextBrush = new SolidBrush(textBrushColor);
            DrawPen = new Pen(penColor);
            this.DrawFont = drawFont;
        }

        public SolidBrush ShapeBrush { get; }

        public SolidBrush TextBrush { get; }

        public Pen DrawPen { get; }

        public Font DrawFont { get; }
    }
}
