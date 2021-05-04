using System.Drawing;

namespace DiagramsProject
{
    public class DrawnText
    {
        public DrawnText(string text, SizeF measurements)
        {
            ActualText = text;
            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
            Width = measurements.Width + widthAdjustment;
            Height = measurements.Height + heightAdjustment;
            Format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        public string ActualText { get; }

        public float Width { get; }

        public float Height { get; }

        public PointF Position { get; set; }

        public StringFormat Format { get; }
    }
}
