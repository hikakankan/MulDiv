using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MulDiv
{
    /// <summary>
    /// PrivateImageSettings の概要の説明です。
    /// </summary>
    public class PrivateImageSettings
    {
        private XImage image;
        private string image_path;
        private Size image_size;
        private bool use_default;

        public enum EnumCharType { Number, Operator, Parentheses, None }
        private EnumCharType char_type;

        public PrivateImageSettings(char ch)
        {
            image = null;
            image_path = "";
            image_size = new Size(0, 0);
            use_default = false;
            char_type = GetCharType(ch);
        }

        public PrivateImageSettings(PrivateImageSettings settings)
        {
            if ( settings == null )
            {
                image = null;
                image_path = "";
                image_size = new Size(0, 0);
                use_default = false;
                char_type = EnumCharType.None;
            }
            else
            {
                image = settings.image;
                image_path = settings.image_path;
                image_size = settings.image_size;
                use_default = settings.use_default;
                char_type = settings.char_type;
            }
        }

        private EnumCharType GetCharType(char ch)
        {
            EnumCharType char_type;
            if ( Char.IsDigit(ch) )
            {
                char_type = EnumCharType.Number;
            }
            else if ( ch == '+' || ch == '-' || ch == '*' || ch == '/' )
            {
                char_type = EnumCharType.Operator;
            }
            else if ( ch == '(' || ch == ')' )
            {
                char_type = EnumCharType.Parentheses;
            }
            else
            {
                char_type = EnumCharType.None;
            }
            return char_type;
        }

        public XImage Image
        {
            get
            {
                if ( image_path != "" && image == null )
                {
                    try
                    {
                        image = XImage.FromFile(image_path);
                    }
                    catch ( Exception )
                    {
                    }
                }
                return image;
            }
        }

        public string Path
        {
            get
            {
                return image_path;
            }
            set
            {
                image_path = value;
                // パスを設定したらイメージを更新する
                if ( image_path != "" )
                {
                    try
                    {
                        image = XImage.FromFile(image_path);
                    }
                    catch ( Exception )
                    {
                    }
                }
            }
        }

        public Size Size
        {
            get
            {
                return image_size;
            }
            set
            {
                image_size = value;
            }
        }

        public bool UseDefault
        {
            get
            {
                return use_default;
            }
            set
            {
                use_default = value;
            }
        }

        public EnumCharType CharType
        {
            get
            {
                return char_type;
            }
            set
            {
                char_type = value;
            }
        }

        /// <summary>
        /// イメージを描画する
        /// </summary>
        /// <param name="pic">イメージを表示するピクチャーボックス</param>
        /// <param name="g">イメージを表示するGraphics</param>
        public void draw_image(Panel pic, MVGraphics g)
        {
            if ( Image != null )
            {
                double width = Image.Size.Width;
                double height = Image.Size.Height;
                if ( width != 0 && height != 0 )
                {
                    if ( width > pic.Width )
                    {
                        height = height * pic.Width / width;
                        width = pic.Width;
                    }
                    if ( height > pic.Height )
                    {
                        width = width * pic.Width / height;
                        height = pic.Height;
                    }
                    if ( width < pic.Width && height < pic.Height )
                    {
                        if ( height * pic.Width / width <= pic.Height )
                        {
                            height = height * pic.Width / width;
                            width = pic.Width;
                        }
                        else
                        {
                            width = width * pic.Width / height;
                            height = pic.Height;
                        }
                    }
                }
                int x = (int)((pic.Width - width) / 2);
                int y = (int)((pic.Height - height) / 2);
                g.DrawImage(Image, x, y, (int)width, (int)height);
            }
        }
    }
}
