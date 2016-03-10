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
using System.ComponentModel;

namespace MulDiv
{
    /// <summary>
    /// FormAboutDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class FormAboutDialog : Window
    {
        public FormAboutDialog(ViewSettings view)
        {
            InitializeComponent();

            view_settings = view;
            Font = view_settings.MainFont;

            data = new FormAboutDialogData(view_settings);
            DataContext = data;
        }

        private FormAboutDialogData data;

        private ViewSettings view_settings;

        private class FormAboutDialogData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ViewSettings ViewSettings { get; private set; }

            public FormAboutDialogData(ViewSettings view_settings)
            {
                ViewSettings = view_settings;
            }
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
