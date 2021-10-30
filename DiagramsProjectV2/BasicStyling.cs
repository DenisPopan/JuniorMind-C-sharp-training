using System.Drawing;

namespace DiagramsProjectV2
{
    public static class BasicStyling
    {
        public static SolidBrush ShapeBrush { get; } = new SolidBrush(Color.FromArgb(161, 177, 247));

        public static Pen ShapePen { get; } = new Pen(Color.Black);

        public static Pen EdgePen { get; } = new Pen(Color.Black) { Width = 2 };

        public static SolidBrush TextBrush { get; } = new SolidBrush(Color.Black);

        public static Font Font { get; } = new Font(new FontFamily("Arial"), 23);

        public static StringFormat Format { get; } = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public static float WidthAdjustment { get; } = 20;

        public static float HeightAdjustment { get; } = 35;
    }
}