using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;

namespace MulDiv
{
    /// <summary>
    /// FormLoadImage.xaml の相互作用ロジック
    /// </summary>
    public partial class FormLoadImage : Window
    {
        public FormLoadImage()
        {
            InitializeComponent();
        }

        private PrivateImageSettings private_settings;
        private CommonImageSettings common_settings;
        private ViewSettings view_settings;

        private FormLoadImageData data;

        public void SetSettings(ViewSettings view, PrivateImageSettings p_settings, CommonImageSettings c_settings)
        {
            view_settings = view;
            private_settings = new PrivateImageSettings(p_settings);
            common_settings = new CommonImageSettings(c_settings);

            data = new FormLoadImageData(view_settings, private_settings, common_settings);
            DataContext = data;

            Font = view_settings.MainFont;
        }

        public PrivateImageSettings PrivateSettings
        {
            get
            {
                return private_settings;
            }
        }

        public CommonImageSettings CommonSettings
        {
            get
            {
                return common_settings;
            }
        }

        private class FormLoadImageData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private ViewSettings _view_settings;
            private PrivateImageSettings _private_settings;
            //private CommonImageSettings _common_settings;

            public FormLoadImageData(ViewSettings view_settings, PrivateImageSettings private_settings, CommonImageSettings common_settings)
            {
                _view_settings = view_settings;
                _private_settings = private_settings;
                //_common_settings = common_settings;

                textBoxWidth_Text = Convert.ToString(private_settings.Size.Width);
                textBoxHeight_Text = Convert.ToString(private_settings.Size.Height);
                checkBoxDefault_Checked = private_settings.UseDefault;
                switch ( private_settings.CharType )
                {
                    case PrivateImageSettings.EnumCharType.Number:
                        textBoxDefaultWidth_Text = Convert.ToString(common_settings.NumSize.Width);
                        textBoxDefaultHeight_Text = Convert.ToString(common_settings.NumSize.Height);
                        break;
                    case PrivateImageSettings.EnumCharType.Operator:
                        textBoxDefaultWidth_Text = Convert.ToString(common_settings.OpSize.Width);
                        textBoxDefaultHeight_Text = Convert.ToString(common_settings.OpSize.Height);
                        break;
                    case PrivateImageSettings.EnumCharType.Parentheses:
                        textBoxDefaultWidth_Text = Convert.ToString(common_settings.ParSize.Width);
                        textBoxDefaultHeight_Text = Convert.ToString(common_settings.ParSize.Height);
                        break;
                    case PrivateImageSettings.EnumCharType.None:
                        textBoxDefaultWidth_Text = "";
                        textBoxDefaultHeight_Text = "";
                        break;
                }
            }

            public PrivateImageSettings PrivateImageSettings
            {
                get
                {
                    return _private_settings;
                }
            }

            public void PrivateImageSettingsChanged()
            {
                callPropertyChanged(nameof(PrivateImageSettings));
            }

            public ViewSettings ViewSettings
            {
                get
                {
                    return _view_settings;
                }
            }

            private string _textBoxWidth_Text = "";
            private string _textBoxHeight_Text = "";
            private string _textBoxDefaultWidth_Text = "";
            private string _textBoxDefaultHeight_Text = "";
            private bool _checkBoxDefault_Checked = false;

            private void callPropertyChanged(string name)
            {
                if ( PropertyChanged != null )
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            public string textBoxWidth_Text
            {
                get
                {
                    return _textBoxWidth_Text;
                }
                set
                {
                    _textBoxWidth_Text = value;
                    callPropertyChanged(nameof(textBoxWidth_Text));
                }
            }

            public string textBoxHeight_Text
            {
                get
                {
                    return _textBoxHeight_Text;
                }
                set
                {
                    _textBoxHeight_Text = value;
                    callPropertyChanged(nameof(textBoxHeight_Text));
                }
            }

            public string textBoxDefaultWidth_Text
            {
                get
                {
                    return _textBoxDefaultWidth_Text;
                }
                set
                {
                    _textBoxDefaultWidth_Text = value;
                    callPropertyChanged(nameof(textBoxDefaultWidth_Text));
                }
            }

            public string textBoxDefaultHeight_Text
            {
                get
                {
                    return _textBoxDefaultHeight_Text;
                }
                set
                {
                    _textBoxDefaultHeight_Text = value;
                    callPropertyChanged(nameof(textBoxDefaultHeight_Text));
                }
            }

            public bool checkBoxDefault_Checked
            {
                get
                {
                    return _checkBoxDefault_Checked;
                }
                set
                {
                    _checkBoxDefault_Checked = value;
                    callPropertyChanged(nameof(checkBoxDefault_Checked));
                }
            }
        }

        /// <summary>
        /// ファイルダイアログでファイル名を指定してイメージを読み込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoad_Click(object sender, System.EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "画像ファイル (*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png;*.bmp|すべてのファイル (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.FileName = data.PrivateImageSettings.Path;
                openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog1.RestoreDirectory = true;
                if ( openFileDialog1.ShowDialog() == true )
                {
                    data.PrivateImageSettings.Path = openFileDialog1.FileName;
                    data.PrivateImageSettings.Size = data.PrivateImageSettings.Image.Size;
                    data.textBoxWidth_Text = Convert.ToString(data.PrivateImageSettings.Size.Width);
                    data.textBoxHeight_Text = Convert.ToString(data.PrivateImageSettings.Size.Height);
                    if ( data.textBoxDefaultWidth_Text == "0" )
                    {
                        data.textBoxDefaultWidth_Text = data.textBoxWidth_Text;
                    }
                    if ( data.textBoxDefaultHeight_Text == "0" )
                    {
                        data.textBoxDefaultHeight_Text = data.textBoxHeight_Text;
                    }
                    data.PrivateImageSettingsChanged();
                }
            }
            catch ( Exception )
            {
            }
        }

        private static Size textbox_to_size(string text_width, string text_height)
        {
            try
            {
                return new Size(Convert.ToInt32(text_width), Convert.ToInt32(text_height));
            }
            catch ( Exception )
            {
                return new Size(0, 0);
            }
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            Size psize = textbox_to_size(data.textBoxWidth_Text, data.textBoxHeight_Text);
            Size csize = textbox_to_size(data.textBoxDefaultWidth_Text, data.textBoxDefaultHeight_Text);
            data.PrivateImageSettings.Size = psize;
            data.PrivateImageSettings.UseDefault = data.checkBoxDefault_Checked;
            switch ( data.PrivateImageSettings.CharType )
            {
                case PrivateImageSettings.EnumCharType.Number:
                    common_settings.NumSize = csize;
                    break;
                case PrivateImageSettings.EnumCharType.Operator:
                    common_settings.OpSize = csize;
                    break;
                case PrivateImageSettings.EnumCharType.Parentheses:
                    common_settings.ParSize = csize;
                    break;
                case PrivateImageSettings.EnumCharType.None:
                    break;
            }
        }

        private void buttonCancel_Click_Cancel(object sender, EventArgs e)
        {
            DialogResult = false;
        }

        private void buttonOK_Click_OK(object sender, EventArgs e)
        {
            DialogResult = true;
            buttonOK_Click(sender, e);
        }

        private int fontHeight;

        public XFont Font
        {
            get
            {
                return new XFont(new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), fontHeight);
            }
            set
            {
                if ( value != null )
                {
                    FontFamily = value.Typeface.FontFamily;
                    FontStyle = value.Typeface.Style;
                    FontWeight = value.Typeface.Weight;
                    FontStretch = value.Typeface.Stretch;
                    fontHeight = value.Height;
                }
            }
        }
    }
}
