using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MulDiv
{
    public class XImage
    {
        private ImageSource imageSource;

        public XImage(BitmapImage bitmap)
        {
            imageSource = bitmap;
            Size = new Size(bitmap.PixelWidth, bitmap.Height); 
        }

        public ImageSource Source
        {
            get
            {
                return imageSource;
            }
        }

        public Size Size { get; set; }

        public static XImage FromFile(string filename)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);
            bi.EndInit();
            return new XImage(bi);
        }
    }
}
