using System.Drawing;

namespace DiagramsProjectV2
{
    public class Node
    {
        public Node(Graphics graphics, int id, string text, float x, float y)
        {
            ProjectUtils.EnsureIsNotNull(graphics, nameof(graphics));
            Id = id;
            Text = text;
            const float widthAdjustment = 30;
            const float heightAdjustment = 10;
            using FontFamily fontFamily = new FontFamily("Arial");
            using var font = new Font(fontFamily, 23);
            SizeF stringSize = graphics.MeasureString(text, font);
            Rectangle = new RectangleF(x, y, stringSize.Width + widthAdjustment, stringSize.Height + heightAdjustment);
        }

        public int Id { get; }

        public RectangleF Rectangle { get; }

        public string Text { get; }
    }
}
