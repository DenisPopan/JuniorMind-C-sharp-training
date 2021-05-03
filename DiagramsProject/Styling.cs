using System;
using System.Drawing;
using System.Drawing.Drawing2D;

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
            TextFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public Styling(Color shapeBrushColor, Color textBrushColor, Color penColor, Font drawFont)
        {
            ShapeBrush = new SolidBrush(shapeBrushColor);
            TextBrush = new SolidBrush(textBrushColor);
            DrawPen = new Pen(penColor);
            this.DrawFont = drawFont;
            TextFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public SolidBrush ShapeBrush { get; }

        public SolidBrush TextBrush { get; }

        public Pen DrawPen { get; }

        public Font DrawFont { get; }

        public StringFormat TextFormat { get; }
    }
}
