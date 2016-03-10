using System;
using System.Windows.Media;

namespace MulDiv
{
	/// <summary>
	/// MVColor ‚ÌŠT—v‚Ìà–¾‚Å‚·B
	/// </summary>
	public class MVColor
	{
		private Color col;

        public MVColor(int r, int g, int b)
        {
            col = Color.FromRgb((byte)r, (byte)g, (byte)b);
        }

        public static MVColor FromArgb(int r, int g, int b)
        {
            return new MVColor(r, g, b);
        }

        public static MVColor FromArgb(int rgb)
        {
            return new MVColor((rgb >> 16) & 0xff, (rgb >> 8) & 0xff, rgb & 0xff);
        }

        public int ToArgb()
        {
            int r = col.R;
            int g = col.G;
            int b = col.B;
            return r << 16 | g << 8 | b;
        }

        public Color WColor
        {
            get
            {
                return col;
            }
        }

        public Brush XColor
        {
            get
            {
                return new SolidColorBrush(col);
            }
        }
    }
}
