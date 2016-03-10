using System;
using System.Windows;
using System.Windows.Media;

namespace MulDiv
{
	/// <summary>
	/// MVFont ÇÃäTóvÇÃê‡ñæÇ≈Ç∑ÅB
	/// </summary>
	public class MVFont
	{
        private XFont font;
        private bool use_image;
        private ImageSettings image_settings;

        public MVFont(XFont font, bool use, ImageSettings settings)
        {
            this.font = font;
            use_image = use;
            image_settings = settings;
        }

        public XFont Typeface
        {
            get
            {
                return font;
            }
        }

        public int getHeight()
        {
            if ( use_image )
            {
                return image_settings.GetHeight();
            }
            else
            {
                return font.Height;
            }
        }

        public int stringWidth(String str)
        {
            if ( use_image )
            {
                return image_settings.GetWidth(str);
            }
            else
            {
                FormattedText text = new FormattedText(str, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, font.Typeface, font.Height, null);
                return (int)text.Width;
            }
        }

        public int getAscent()
        {
            return 0;
        }
    }
}
