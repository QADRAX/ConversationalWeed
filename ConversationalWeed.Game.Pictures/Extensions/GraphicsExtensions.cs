using ConversationalWeed.Game.Pictures;

namespace System.Drawing
{
    public static class GraphicsExtensions
    {
        public static void SetText(this Graphics graphics, string text, RectangleF rectf)
        {
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphics.DrawString(text, ImageGenerator.DEFAULT_FONT, Brushes.Black, rectf, sf);
        }
    }
}
