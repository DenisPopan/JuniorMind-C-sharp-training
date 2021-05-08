using System.Drawing;

namespace DiagramsProject.Shapes
{
    public class Subgraph : Shape
    {
        readonly Shape[] containedShapes;
        float right = -1;
        float left;
        float top;
        float bottom = -1;

        public Subgraph(Graphics graphics, string text, Styling styling, Shape[] containedShapes) : base(graphics, text, styling)
        {
            Draw.EnsureIsNotNull(graphics, nameof(graphics));
            this.containedShapes = containedShapes;
            left = graphics.VisibleClipBounds.Right;
            top = graphics.VisibleClipBounds.Bottom;
            FindBounds();
            FixBounds();
            Bounds = new RectangleF(left, top, right - left, bottom - top);
            Text.Position = new PointF(Bounds.X + Bounds.Width / 2, Bounds.Y + Text.Height / 2);
        }

        public override void DrawShape()
        {
            Graphics.FillRectangle(Styling.ShapeBrush, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
            Graphics.DrawRectangle(Styling.DrawPen, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
            Graphics.DrawString(Text.ActualText, Styling.DrawFont, Styling.TextBrush, Text.Position, Text.Format);
            DrawContainedShapes();
        }

        void DrawContainedShapes()
        {
            foreach (var shape in containedShapes)
            {
                shape.DrawShape();
            }
        }

        void FindBounds()
        {
            foreach (var shape in containedShapes)
            {
                if (right < shape.Bounds.Right)
                {
                    right = shape.Bounds.Right;
                }

                if (left > shape.Bounds.Left)
                {
                    left = shape.Bounds.Left;
                }

                if (bottom < shape.Bounds.Bottom)
                {
                    bottom = shape.Bounds.Bottom;
                }

                if (top > shape.Bounds.Top)
                {
                    top = shape.Bounds.Top;
                }
            }
        }

        void FixBounds()
        {
            const float sizeAdjustments = 10;
            right = right + sizeAdjustments + Text.Height;
            left = left - sizeAdjustments - Text.Height;
            top = top - sizeAdjustments - Text.Height;
            bottom = bottom + sizeAdjustments + Text.Height;
        }
    }
}
