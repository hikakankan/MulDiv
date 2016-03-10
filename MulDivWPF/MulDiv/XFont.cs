using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace MulDiv
{
    public class XFont
    {
        public Typeface Typeface { get; set; }
        public int Height { get; set; }

        public XFont(Typeface tf, int height)
        {
            Typeface = tf;
            Height = height;
        }

        public string FontStyleString
        {
            get
            {
                if ( Typeface.Style == FontStyles.Italic )
                {
                    return "Italic";
                }
                if ( Typeface.Style == FontStyles.Oblique )
                {
                    return "Oblique";
                }
                return "Normal";
            }
        }

        public static FontStyle GetFontStyleFromString(string fs)
        {
            if ( fs == "Italic" )
            {
                return FontStyles.Italic;
            }
            if ( fs == "Oblique" )
            {
                return FontStyles.Oblique;
            }
            return FontStyles.Normal;
        }

        public string FontWeightString
        {
            get
            {
                if ( Typeface.Weight == FontWeights.Regular )
                {
                    return "Regular";
                }
                if ( Typeface.Weight == FontWeights.Black )
                {
                    return "Black";
                }
                if ( Typeface.Weight == FontWeights.Bold )
                {
                    return "bold";
                }
                if ( Typeface.Weight == FontWeights.DemiBold )
                {
                    return "DemiBold";
                }
                if ( Typeface.Weight == FontWeights.ExtraBlack )
                {
                    return "ExtraBlack";
                }
                if ( Typeface.Weight == FontWeights.ExtraBold )
                {
                    return "ExtraBold";
                }
                if ( Typeface.Weight == FontWeights.ExtraLight )
                {
                    return "ExtraLight";
                }
                if ( Typeface.Weight == FontWeights.Heavy )
                {
                    return "Heavy";
                }
                if ( Typeface.Weight == FontWeights.Light )
                {
                    return "Light";
                }
                if ( Typeface.Weight == FontWeights.Medium )
                {
                    return "Medium";
                }
                if ( Typeface.Weight == FontWeights.SemiBold )
                {
                    return "SemiBold";
                }
                if ( Typeface.Weight == FontWeights.Thin )
                {
                    return "Thin";
                }
                if ( Typeface.Weight == FontWeights.UltraBlack )
                {
                    return "UltraBlack";
                }
                if ( Typeface.Weight == FontWeights.UltraBold )
                {
                    return "UltraBold";
                }
                if ( Typeface.Weight == FontWeights.UltraLight )
                {
                    return "UltraLight";
                }
                return "Normal";
            }
        }

        public static FontWeight GetFontWeightFromString(string fw)
        {
            if ( fw == "Regular" )
            {
                return FontWeights.Regular;
            }
            if ( fw == "Black" )
            {
                return FontWeights.Black;
            }
            if ( fw == "Bold" )
            {
                return FontWeights.Bold;
            }
            if ( fw == "DemiBold" )
            {
                return FontWeights.DemiBold;
            }
            if ( fw == "ExtraBlack" )
            {
                return FontWeights.ExtraBlack;
            }
            if ( fw == "ExtraBold" )
            {
                return FontWeights.ExtraBold;
            }
            if ( fw == "ExtraLight" )
            {
                return FontWeights.ExtraLight;
            }
            if ( fw == "Heavy" )
            {
                return FontWeights.Heavy;
            }
            if ( fw == "Light" )
            {
                return FontWeights.Light;
            }
            if ( fw == "Medium" )
            {
                return FontWeights.Medium;
            }
            if ( fw == "SemiBold" )
            {
                return FontWeights.SemiBold;
            }
            if ( fw == "Thin" )
            {
                return FontWeights.Thin;
            }
            if ( fw == "UltraBlack" )
            {
                return FontWeights.UltraBlack;
            }
            if ( fw == "UltraBold" )
            {
                return FontWeights.UltraBold;
            }
            if ( fw == "UltraLight" )
            {
                return FontWeights.UltraLight;
            }
            return FontWeights.Normal;
        }

        private System.Drawing.FontFamily DrawingFontFamily
        {
            get
            {
                return new System.Drawing.FontFamily(Typeface.FontFamily.Source);
            }
        }

        private System.Drawing.FontStyle DrawingFontStyle
        {
            get
            {
                System.Drawing.FontStyle fs = Typeface.Style == FontStyles.Normal ? System.Drawing.FontStyle.Regular : System.Drawing.FontStyle.Italic;
                System.Drawing.FontStyle fw = Typeface.Weight == FontWeights.Normal || Typeface.Weight == FontWeights.Regular ? System.Drawing.FontStyle.Regular : System.Drawing.FontStyle.Bold;
                return fs | fw;
            }
        }

        public System.Drawing.Font DrawingFont
        {
            get
            {
                return new System.Drawing.Font(DrawingFontFamily, Height, DrawingFontStyle);
            }
        }

        public static XFont FromDrawingFont(System.Drawing.Font font)
        {
            FontFamily ff = new FontFamily(font.FontFamily.Name);
            double size = font.SizeInPoints / 72.0 * 96.0;
            FontStyle fs = (font.Style & System.Drawing.FontStyle.Italic) == 0 ? FontStyles.Normal : FontStyles.Italic;
            FontWeight fw = (font.Style & System.Drawing.FontStyle.Bold) == 0 ? FontWeights.Normal : FontWeights.Bold;
            return new XFont(new Typeface(ff, fs, fw, FontStretches.Normal), (int)size);
        }
    }
}
