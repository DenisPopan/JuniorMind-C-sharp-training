using System.Drawing;
using System.Drawing.Drawing2D;

namespace DiagramsProject
{
    public class Link
    {
        readonly sbyte linkType;
        readonly string text;
        readonly Styling styling;
        readonly Graphics graphics;

        public Link(Graphics graphics, Shape source, Shape destination, sbyte linkType, Styling styling, string text = "")
        {
            Source = source;
            Destination = destination;
            this.linkType = linkType;
            this.styling = styling;
            this.graphics = graphics;
            this.text = text;
        }

        public Shape Source { get; }

        public Shape Destination { get; }

        public void DrawLink()
        {
            const float divider = 2;
            PointF sourceCenter = new PointF(
                Source.Bounds.X + Source.Bounds.Width / divider,
                Source.Bounds.Y + Source.Bounds.Height / divider);

            PointF destinationCenter = new PointF(
                Destination.Bounds.X + Destination.Bounds.Width / divider,
                Destination.Bounds.Y + Destination.Bounds.Height / divider);

            PointF intersetion1 = ProjectUtils.FindIntersectionOfTwoLines(
                sourceCenter,
                destinationCenter,
                new PointF(Source.Bounds.Left, Source.Bounds.Bottom),
                new PointF(Source.Bounds.Right, Source.Bounds.Bottom));

            PointF intersetion2 = ProjectUtils.FindIntersectionOfTwoLines(
                sourceCenter,
                destinationCenter,
                new PointF(Destination.Bounds.Left, Destination.Bounds.Top),
                new PointF(Destination.Bounds.Right, Destination.Bounds.Top));

            SetArrowEnds();

            graphics.DrawLine(styling.ShapePen, intersetion2, intersetion1);

            if (text == "")
            {
                return;
            }

            DrawText(sourceCenter, destinationCenter);
        }

        void SetArrowEnds()
        {
            if (linkType == 1)
            {
                styling.ShapePen.CustomEndCap = new AdjustableArrowCap(styling.ShapePen.Width + 2, styling.ShapePen.Width + 2);
            }

            if (linkType != 2)
            {
                return;
            }

            styling.ShapePen.CustomStartCap = new AdjustableArrowCap(styling.ShapePen.Width + 2, styling.ShapePen.Width + 2);
            styling.ShapePen.CustomEndCap = new AdjustableArrowCap(styling.ShapePen.Width + 2, styling.ShapePen.Width + 2);
        }

        void DrawText(PointF sourceCenter, PointF destinationCenter)
        {
            const float divider = 2;
            SizeF stringSize = graphics.MeasureString(text, styling.DrawFont);
            PointF textPosition = new PointF(
                sourceCenter.X + (destinationCenter.X - sourceCenter.X) / divider - stringSize.Width / divider,
                Source.Bounds.Bottom + (Destination.Bounds.Top - Source.Bounds.Bottom) / divider - stringSize.Height / divider);
            graphics.FillRectangle(
                styling.ShapeBrush,
                textPosition.X,
                textPosition.Y,
                stringSize.Width,
                stringSize.Height);
            graphics.DrawString(text, styling.DrawFont, styling.TextBrush, textPosition);
        }
    }
}
