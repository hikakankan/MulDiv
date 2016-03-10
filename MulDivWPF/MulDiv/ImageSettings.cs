using System;

namespace MulDiv
{
    /// <summary>
    /// ImageSettings の概要の説明です。
    /// </summary>
    public class ImageSettings
    {
        private CommonImageSettings common_settings;
        private System.Collections.ArrayList setting_list;
        private char[] char_list = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '+', '-', '*', '/', '(', ')' };

        public ImageSettings()
        {
            common_settings = new CommonImageSettings();
            setting_list = new System.Collections.ArrayList();
            for ( int i = 0; i < char_list.Length; i++ )
            {
                setting_list.Add(new PrivateImageSettings(char_list[i]));
            }
        }

        public ImageSettings(ImageSettings settings)
        {
            common_settings = new CommonImageSettings(settings.CommonSettings);
            setting_list = new System.Collections.ArrayList();
            for ( int i = 0; i < char_list.Length; i++ )
            {
                setting_list.Add(new PrivateImageSettings(settings.GetSettings(char_list[i])));
            }
        }

        public CommonImageSettings CommonSettings
        {
            get
            {
                return common_settings;
            }
            set
            {
                common_settings = value;
            }
        }

        public PrivateImageSettings GetSettings(char ch)
        {
            for ( int i = 0; i < char_list.Length; i++ )
            {
                if ( char_list[i] == ch )
                {
                    return (PrivateImageSettings)setting_list[i];
                }
            }
            return null;
        }

        public void SetSettings(char ch, PrivateImageSettings settings)
        {
            for ( int i = 0; i < char_list.Length; i++ )
            {
                if ( char_list[i] == ch )
                {
                    setting_list[i] = settings;
                }
            }
        }

        public PrivateImageSettings this[char ch]
        {
            get
            {
                if ( ch == '#' )
                {
                    ch = '('; // '(' が XAML で使えないので変換
                }
                return GetSettings(ch);
            }
            set
            {
                if ( ch == '#' )
                {
                    ch = '('; // '(' が XAML で使えないので変換
                }
                SetSettings(ch, value);
            }
        }

        public int GetWidth(PrivateImageSettings settings)
        {
            double width = 0;
            if ( settings != null )
            {
                if ( settings.UseDefault )
                {
                    switch ( settings.CharType )
                    {
                        case PrivateImageSettings.EnumCharType.Number:
                            width = common_settings.NumSize.Width;
                            break;
                        case PrivateImageSettings.EnumCharType.Operator:
                            width = common_settings.OpSize.Width;
                            break;
                        case PrivateImageSettings.EnumCharType.Parentheses:
                            width = common_settings.ParSize.Width;
                            break;
                        case PrivateImageSettings.EnumCharType.None:
                            width = 0;
                            break;
                    }
                }
                else
                {
                    width = settings.Size.Width;
                }
            }
            return (int)width;
        }

        public int GetWidth(string str)
        {
            str = str.Replace('×', '*');
            str = str.Replace('÷', '/');
            int width = 0;
            for ( int i = 0; i < str.Length; i++ )
            {
                if ( str[i] == ' ' )
                {
                    // スペースのときは既定のドット数とする
                    width += common_settings.SpaceWidth;
                }
                else
                {
                    width += GetWidth(GetSettings(str[i]));
                }
            }
            System.Diagnostics.Debug.WriteLine("GetWidth of " + str + " = " + width.ToString());
            return width;
        }

        private int GetHeight(PrivateImageSettings settings)
        {
            double height = 0;
            if ( settings != null )
            {
                if ( settings.UseDefault )
                {
                    switch ( settings.CharType )
                    {
                        case PrivateImageSettings.EnumCharType.Number:
                            height = common_settings.NumSize.Height;
                            break;
                        case PrivateImageSettings.EnumCharType.Operator:
                            height = common_settings.OpSize.Height;
                            break;
                        case PrivateImageSettings.EnumCharType.Parentheses:
                            height = common_settings.ParSize.Height;
                            break;
                        case PrivateImageSettings.EnumCharType.None:
                            height = 0;
                            break;
                    }
                }
                else
                {
                    height = settings.Size.Height;
                }
            }
            return (int)height;
        }

        private int GetHeight(string str)
        {
            int height = 0;
            for ( int i = 0; i < str.Length; i++ )
            {
                height = Math.Max(height, GetHeight(GetSettings(str[i])));
            }
            System.Diagnostics.Debug.WriteLine("GetHeight of " + str + " = " + height.ToString());
            return height;
        }

        public int GetHeight()
        {
            return GetHeight("0123456789.+-*/()");
        }

        public void DrawString(string str, int x, int y, MVGraphics graph)
        {
            str = str.Replace('×', '*');
            str = str.Replace('÷', '/');
            for ( int i = 0; i < str.Length; i++ )
            {
                if ( str[i] == ' ' )
                {
                    // スペースのときは既定のドット数とする
                    x += common_settings.SpaceWidth;
                }
                else
                {
                    PrivateImageSettings settings = GetSettings(str[i]);
                    if ( settings != null && settings.Image != null )
                    {
                        int width = GetWidth(settings);
                        int height = GetHeight(settings);
                        graph.DrawImage(settings.Image, x, y, width, height);
                        x += width;
                    }
                }
            }
        }

        public void DrawString(string str, int x, int y, XGraphics graph)
        {
            str = str.Replace('×', '*');
            str = str.Replace('÷', '/');
            for ( int i = 0; i < str.Length; i++ )
            {
                if ( str[i] == ' ' )
                {
                    // スペースのときは既定のドット数とする
                    x += common_settings.SpaceWidth;
                }
                else
                {
                    PrivateImageSettings settings = GetSettings(str[i]);
                    if ( settings != null && settings.Image != null )
                    {
                        int width = GetWidth(settings);
                        int height = GetHeight(settings);
                        graph.DrawImage(settings.Image, x, y, width, height);
                        x += width;
                    }
                }
            }
        }
    }
}
