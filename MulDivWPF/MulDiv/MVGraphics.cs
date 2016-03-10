using System;
using System.Windows;
using System.Windows.Media;

namespace MulDiv
{
    /// <summary>
    /// MVGraphics ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
    /// </summary>
    public class MVGraphics
    {
        private DrawingContext graph;
        private XFont font;
        private MVColor col;
        private bool use_image;
        private ImageSettings image_settings;

        public MVGraphics(DrawingContext g, XFont f, bool use, ImageSettings settings)
        {
            graph = g;
            font = f;
            use_image = use;
            image_settings = settings;
        }

        public void setColor(MVColor c)
        {
            col = c;
        }

        //public void setColor(Color c)
        //{
        //	col = c;
        //}

        public void fillRect(int x, int y, int width, int height)
        {
            System.Diagnostics.Debug.WriteLine("MVGraphics.fillRect " + x.ToString() + " " + y.ToString() + " " + width.ToString() + " " + height.ToString());
            Brush brush = col.XColor;
            graph.DrawRectangle(brush, null, new Rect(x, y, width, height));
        }

        public void drawRoundRect(int x, int y, int width, int height, int d1, int d2)
        {
            System.Diagnostics.Debug.WriteLine("MVGraphics.drawRoundRect " + x.ToString() + " " + y.ToString() + " " + width.ToString() + " " + height.ToString() + " " + d1.ToString() + " " + d2.ToString());
            int r1 = d1 / 2;
            int r2 = d2 / 2;
            Pen pen = new Pen(col.XColor, 1);
            graph.DrawRoundedRectangle(null, pen, new Rect(x, y, width, height), d1, d2);
        }

        public void fillRoundRect(int x, int y, int width, int height, int d1, int d2)
        {
            System.Diagnostics.Debug.WriteLine("MVGraphics.fillRoundRect " + x.ToString() + " " + y.ToString() + " " + width.ToString() + " " + height.ToString() + " " + d1.ToString() + " " + d2.ToString());
            int r1 = d1 / 2;
            int r2 = d2 / 2;
            Brush brush = col.XColor;
            graph.DrawRoundedRectangle(brush, null, new Rect(x, y, width, height), d1, d2);
        }

        public void drawString(String str, int x, int y)
        {
            System.Diagnostics.Debug.WriteLine("MVGraphics.drawString " + str + " " + x.ToString() + " " + y.ToString());
            if ( use_image )
            {
                image_settings.DrawString(str, x, y, this);
            }
            else
            {
                Brush brush = col.XColor;
                FormattedText text = new FormattedText(str, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, font.Typeface, font.Height, brush);
                graph.DrawText(text, new Point(x, y));
            }
        }

        public MVFont getFontMetrics()
        {
            return new MVFont(font, use_image, image_settings);
        }

        public void DrawImage(XImage image, int x, int y, int width, int height)
        {
            graph.DrawImage(image.Source, new Rect(x, y, width, height));
        }
    }
}
