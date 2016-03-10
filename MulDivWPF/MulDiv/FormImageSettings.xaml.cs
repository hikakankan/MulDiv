using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MulDiv
{
    /// <summary>
    /// FormImageSettings.xaml の相互作用ロジック
    /// </summary>
    public partial class FormImageSettings : Window
    {
        public FormImageSettings()
        {
            InitializeComponent();
        }

        private ViewSettings view_settings;
        private ImageSettings image_settings;

        private FormImageSettingsData data;

        public ViewSettings ViewSettings
        {
            set
            {
                view_settings = value;
                image_settings = new ImageSettings(view_settings.ImageSettings);

                data = new FormImageSettingsData(view_settings, image_settings);
                DataContext = data;

                Font = view_settings.MainFont;
            }
        }

        /// <summary>
        /// イメージの設定を返却する
        /// </summary>
        public ImageSettings ImageSettings
        {
            get
            {
                return image_settings;
            }
        }

        private class FormImageSettingsData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public FormImageSettingsData(ViewSettings view_settings, ImageSettings image_settings)
            {
                ViewSettings = view_settings;
                ImageSettings = image_settings;
            }

            public ViewSettings ViewSettings { get; private set; }
            public ImageSettings ImageSettings { get; private set; }

            private void callPropertyChanged(string name)
            {
                if ( PropertyChanged != null )
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            public void ImageSettingsChanged()
            {
                callPropertyChanged(nameof(ImageSettings));
            }
        }

        /// <summary>
        /// イメージの読み込みダイアログを表示して文字のイメージを変更する
        /// </summary>
        /// <param name="ch">イメージに対応する文字</param>
        /// <param name="pictureBox">イメージを表示するピクチャーボックス</param>
        private void select_image(char ch)
        {
            FormLoadImage dlg = new FormLoadImage();
            dlg.SetSettings(view_settings, ImageSettings.GetSettings(ch), ImageSettings.CommonSettings);
            if ( dlg.ShowDialog() == true )
            {
                ImageSettings.CommonSettings = dlg.CommonSettings;
                ImageSettings.SetSettings(ch, dlg.PrivateSettings);

                data.ImageSettingsChanged();
            }
        }

        private void buttonImage0_Click(object sender, System.EventArgs e)
        {
            select_image('0');
        }

        private void buttonImage1_Click(object sender, System.EventArgs e)
        {
            select_image('1');
        }

        private void buttonImage2_Click(object sender, System.EventArgs e)
        {
            select_image('2');
        }

        private void buttonImage3_Click(object sender, System.EventArgs e)
        {
            select_image('3');
        }

        private void buttonImage4_Click(object sender, System.EventArgs e)
        {
            select_image('4');
        }

        private void buttonImage5_Click(object sender, System.EventArgs e)
        {
            select_image('5');
        }

        private void buttonImage6_Click(object sender, System.EventArgs e)
        {
            select_image('6');
        }

        private void buttonImage7_Click(object sender, System.EventArgs e)
        {
            select_image('7');
        }

        private void buttonImage8_Click(object sender, System.EventArgs e)
        {
            select_image('8');
        }

        private void buttonImage9_Click(object sender, System.EventArgs e)
        {
            select_image('9');
        }

        private void buttonImageDot_Click(object sender, System.EventArgs e)
        {
            select_image('.');
        }

        private void buttonImagePlus_Click(object sender, System.EventArgs e)
        {
            select_image('+');
        }

        private void buttonImageMinus_Click(object sender, System.EventArgs e)
        {
            select_image('-');
        }

        private void buttonImageMult_Click(object sender, System.EventArgs e)
        {
            select_image('*');
        }

        private void buttonImageDiv_Click(object sender, System.EventArgs e)
        {
            select_image('/');
        }

        private void buttonImageParOpen_Click(object sender, System.EventArgs e)
        {
            select_image('(');
        }

        private void buttonImageParClose_Click(object sender, System.EventArgs e)
        {
            select_image(')');
        }

        private void buttonCancel_Click_Cancel(object sender, EventArgs e)
        {
            DialogResult = false;
        }

        private void buttonOK_Click_OK(object sender, EventArgs e)
        {
            DialogResult = true;
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
