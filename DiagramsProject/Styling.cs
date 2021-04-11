using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Styling : IDisposable
    {
        public readonly SolidBrush ShapeBrush;
        public readonly SolidBrush TextBrush;
        public readonly Pen DrawPen;
        public readonly Font DrawFont;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            ShapeBrush.Dispose();
            TextBrush.Dispose();
            DrawPen.Dispose();
            DrawFont.Dispose();
        }
    }
}
