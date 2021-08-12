using System.Drawing;

namespace DiagramsProjectV2
{
    public class Styling
    {
        public Styling()
        {
            ShapeBrush = new SolidBrush(Color.FromArgb(161, 177, 247));
            ShapePen = new Pen(Color.Black);
            TextBrush = new SolidBrush(Color.Black);
            using FontFamily fontFamily = new FontFamily("Arial");
            Font = new Font(fontFamily, 23);
            Format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public Graphics Graphics { get; set; }

        public SolidBrush ShapeBrush { get; }

        public Pen ShapePen { get; }

        public SolidBrush TextBrush { get; }

        public Font Font { get; }

        public StringFormat Format { get; }
    }
}