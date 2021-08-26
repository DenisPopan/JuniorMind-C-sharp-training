using System.Drawing;

namespace DiagramsProjectV2
{
    public class Styling
    {
        public SolidBrush ShapeBrush { get; } = new SolidBrush(Color.FromArgb(161, 177, 247));

        public Pen ShapePen { get; } = new Pen(Color.Black);

        public Pen EdgePen { get; } = new Pen(Color.Black) { Width = 2 };

        public SolidBrush TextBrush { get; } = new SolidBrush(Color.Black);

        public Font Font { get; } = new Font(new FontFamily("Arial"), 19);

        public StringFormat Format { get; } = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }
}