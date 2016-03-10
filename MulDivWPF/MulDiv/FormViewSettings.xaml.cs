using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Forms;

namespace MulDiv
{
    /// <summary>
    /// FormViewSettings.xaml の相互作用ロジック
    /// </summary>
    public partial class FormViewSettings : Window
    {
        public FormViewSettings(ViewSettings view)
        {
            InitializeComponent();

            view_settings = view;

            data = new FormViewSettingsData(view_settings);
            DataContext = data;

            data.SetFont = SetFont;

            Font = view_settings.MainFont;
        }

        private ViewSettings view_settings;

        private FormViewSettingsData data;

        public void SetFont(XFont font)
        {
            Font = font;
        }

        private class FormViewSettingsData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private ViewSettings view_settings_org;

            public FormViewSettingsData(ViewSettings view_settings)
            {
                view_settings_org = view_settings;
                ViewSettings = new ViewSettings(view_settings);
            }

            public Action<XFont> SetFont { get; set; }

            public ViewSettings ViewSettings { get; private set; }

            private void callPropertyChanged(string name)
            {
                if ( PropertyChanged != null )
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            public void ViewSettingsChanged()
            {
                callPropertyChanged(nameof(ViewSettings));
            }

            /// <summary>
            /// 表示の設定を更新する
            /// </summary>
            public void Update()
            {
                view_settings_org.CalcAreaTextColor = ViewSettings.CalcAreaTextColor;
                view_settings_org.CalcAreaBackColor = ViewSettings.CalcAreaBackColor;
                //view_settings_org.CalcAreaFrameColor = ViewSettings.CalcAreaFrameColor;
                //view_settings_org.CalcAreaFrameBackColor = ViewSettings.CalcAreaFrameBackColor;
                //view_settings_org.CalcAreaFrameSelectedBackColor = ViewSettings.CalcAreaFrameSelectedBackColor;

                view_settings_org.BodyBackColor = ViewSettings.BodyBackColor;
                view_settings_org.BodyTextColor = ViewSettings.BodyTextColor;
                view_settings_org.ButtonBackColor = ViewSettings.ButtonBackColor;
                view_settings_org.ButtonTextColor = ViewSettings.ButtonTextColor;
                view_settings_org.TextBackColor = ViewSettings.TextBackColor;
                view_settings_org.TextTextColor = ViewSettings.TextTextColor;

                //view_settings_org.AsciiMode = ViewSettings.AsciiMode;
                view_settings_org.UseImage = ViewSettings.UseImage;

                view_settings_org.MainFont = ViewSettings.MainFont;
                view_settings_org.CalcAreaFont = ViewSettings.CalcAreaFont;

                view_settings_org.ImageSettings = ViewSettings.ImageSettings;
            }
        }

        /// <summary>
        /// 色のダイアログで使うカスタムカラーの配列
        /// </summary>
        private int[] custom_colors;

        /// <summary>
        /// 色の設定ダイアログを表示して色を変更する
        /// </summary>
        /// <param name="color_org">元の色</param>
        /// <returns>変更後の色</returns>
        private MVColor select_color(MVColor color_org)
        {
            ColorDialog dlg = new ColorDialog();
            Color color = color_org.WColor;
            dlg.Color = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            dlg.FullOpen = true;
            dlg.CustomColors = custom_colors;
            if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                System.Drawing.Color newcolor = dlg.Color;
                custom_colors = dlg.CustomColors;
                data.ViewSettingsChanged();
                return new MVColor(newcolor.R, newcolor.G, newcolor.B);
            }
            return color_org;
        }

        /// <summary>
        /// フォントの設定ダイアログを表示してフォントを変更する
        /// </summary>
        /// <param name="font">現在のフォント</param>
        /// <returns>変更後のフォント</returns>
        private XFont select_font(XFont font)
        {
            FontDialog dlg = new FontDialog();
            dlg.Font = font.DrawingFont;
            if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                return XFont.FromDrawingFont(dlg.Font);
            }
            return font;
        }

        private void buttonCalcAreaTextColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.CalcAreaTextColor = select_color(data.ViewSettings.CalcAreaTextColor);
        }

        private void buttonCalcAreaBackColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.CalcAreaBackColor = select_color(data.ViewSettings.CalcAreaBackColor);
        }

        //private void buttonCalcAreaFrameColor_Click(object sender, System.EventArgs e)
        //{
        //    data.ViewSettings.CalcAreaFrameColor = select_color(data.ViewSettings.CalcAreaFrameColor);
        //}

        //private void buttonCalcAreaFrameBackColor_Click(object sender, System.EventArgs e)
        //{
        //    data.ViewSettings.CalcAreaFrameBackColor = select_color(data.ViewSettings.CalcAreaFrameBackColor);
        //}

        //private void buttonCalcAreaFrameSelectedBackColor_Click(object sender, System.EventArgs e)
        //{
        //    data.ViewSettings.CalcAreaFrameSelectedBackColor = select_color(data.ViewSettings.CalcAreaFrameSelectedBackColor);
        //}

        private void buttonBodyBackColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.BodyBackColor = select_color(data.ViewSettings.BodyBackColor);
        }

        private void buttonBodyTextColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.BodyTextColor = select_color(data.ViewSettings.BodyTextColor);
        }

        private void buttonButtonBackColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.ButtonBackColor = select_color(data.ViewSettings.ButtonBackColor);
        }

        private void buttonButtonTextColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.ButtonTextColor = select_color(data.ViewSettings.ButtonTextColor);
        }

        private void buttonTextBackColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.TextBackColor = select_color(data.ViewSettings.TextBackColor);
        }

        private void buttonTextTextColor_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.TextTextColor = select_color(data.ViewSettings.TextTextColor);
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            data.Update();
        }

        private void buttonFont_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.MainFont = select_font(data.ViewSettings.MainFont);
            SetFont(data.ViewSettings.MainFont);
        }

        private void buttonCalcAreaFont_Click(object sender, System.EventArgs e)
        {
            data.ViewSettings.CalcAreaFont = select_font(data.ViewSettings.CalcAreaFont);
        }

        private void buttonImageSettings_Click(object sender, System.EventArgs e)
        {
            FormImageSettings dlg = new FormImageSettings();
            dlg.ViewSettings = data.ViewSettings;
            if ( dlg.ShowDialog() == true )
            {
                data.ViewSettings.ImageSettings = dlg.ImageSettings;
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
