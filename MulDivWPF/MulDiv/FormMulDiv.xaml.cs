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
    /// FormMulDiv.xaml の相互作用ロジック
    /// </summary>
    public partial class FormMulDiv : Window
    {
        public FormMulDiv()
        {
            InitializeComponent();

            init();
        }

        private MDArray main_mdarray;
        private const int row_count = 3;
        private string exp0;
        private string exp1;
        private string InputText;
        private bool calc_mode; // 入力モード・計算モード

        private Settings settings;

        /// <summary>
        /// ヘルプファイル名
        /// </summary>
        private const string help_file = "MulDiv.chm";
        /// <summary>
        /// 設定ファイル名
        /// </summary>
        private const string settings_file_name = "MulDiv.mdi";

        private void init()
        {
            settings = new Settings(settings_file_name, Font);
            load_settings();

            init_colors();

            array_init();
            InputText = "";
            calc_mode = false; // 入力モード・計算モード。入力モードに設定

            DataContext = new FormMulDivData(settings.get_view_settings());
        }

        /// <summary>
        /// コントロールの色を設定する
        /// </summary>
        private void init_colors()
        {
            //BackColor = settings.get_view_settings().BodyBackColor;
            //panelCalc.BackColor = settings.get_view_settings().CalcAreaBackColor;
            //settings.get_view_settings().SetButtonColors(buttonN0);
            //settings.get_view_settings().SetButtonColors(buttonN1);
            //settings.get_view_settings().SetButtonColors(buttonN2);
            //settings.get_view_settings().SetButtonColors(buttonN3);
            //settings.get_view_settings().SetButtonColors(buttonN4);
            //settings.get_view_settings().SetButtonColors(buttonN5);
            //settings.get_view_settings().SetButtonColors(buttonN6);
            //settings.get_view_settings().SetButtonColors(buttonN7);
            //settings.get_view_settings().SetButtonColors(buttonN8);
            //settings.get_view_settings().SetButtonColors(buttonN9);
            //settings.get_view_settings().SetButtonColors(buttonDot);
            //settings.get_view_settings().SetButtonColors(buttonEqual);
            //settings.get_view_settings().SetButtonColors(buttonMult);
            //settings.get_view_settings().SetButtonColors(buttonDiv);
            //settings.get_view_settings().SetButtonColors(buttonBS);
            //settings.get_view_settings().SetButtonColors(buttonClear);
            //settings.get_view_settings().SetButtonColors(buttonMultNext);
            //settings.get_view_settings().SetButtonColors(buttonDivNext);
            //settings.get_view_settings().SetTextBoxColors(labelInput);
            //settings.get_view_settings().SetTextBoxColors(labelRel);

            Font = settings.get_view_settings().MainFont;
            //panelCalc.Font = settings.get_view_settings().CalcAreaFont;
        }

        public class FormMulDivData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public FormMulDivData(ViewSettings view_settings)
            {
                ViewSettings = view_settings;
            }

            public ViewSettings ViewSettings { get; private set; }

            private void callPropertyChanged(string name)
            {
                if ( PropertyChanged != null )
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            public void UpdateWindowView()
            {
                callPropertyChanged(nameof(ViewSettings));
            }
        }

        /// <summary>
        /// 設定ファイルから設定を読み込む
        /// </summary>
        private void load_settings()
        {
            settings.load_settings();
        }

        /// <summary>
        /// ファイルから読み込んだ後の処理
        /// </summary>
        private void load_settings_post_proc()
        {
            Left = settings.Left;
            Top = settings.Top;
            Width = settings.Width;
            Height = settings.Height;
        }

        /// <summary>
        /// ファイルに書き込む前に行う処理
        /// </summary>
        private void save_settings_pre_proc()
        {
            settings.Left = (int)Left;
            settings.Top = (int)Top;
            settings.Width = (int)Width;
            settings.Height = (int)Height;
        }

        /// <summary>
        /// 設定ファイルに設定を保存
        /// </summary>
        private void save_settings()
        {
            save_settings_pre_proc();
            settings.save_settings();
        }

        /// <summary>
        /// ファイルダイアログでファイルを指定して設定ファイルから設定を読み込む
        /// </summary>
        private void load_settings_file()
        {
            save_settings_pre_proc(); // 設定を更新しておく
            if ( settings.load_settings_file() )
            {
                init_colors();
                load_settings_post_proc();
            }
        }

        /// <summary>
        /// ファイルダイアログでファイルを指定して設定ファイルに設定を保存
        /// </summary>
        private void save_settings_file()
        {
            save_settings_pre_proc();
            settings.save_settings_file();
        }

        /// <summary>
        /// ヘルプを表示
        /// </summary>
        private void help()
        {
            try
            {
                System.Diagnostics.Process.Start(@"http://www2.biglobe.ne.jp/~optimist/software/muldiv21/help/main.html");
                //System.Diagnostics.Process.Start(Path.Combine(Environment.CurrentDirectory, help_file));
                //Help.ShowHelp(this, help_file);
            }
            catch ( Exception )
            {
            }
        }

        private void array_init()
        {
            main_mdarray = new MDArray(settings.get_view_settings());
        }

        private int Number(string s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch ( Exception )
            {
                return 0;
            }
        }

        private int getnum(string s)
        {
            if ( s == "" )
            {
                return 0;
            }
            else
            {
                return Number(s);
            }
        }

        private MDArray array_load()
        {
            return main_mdarray;
        }

        private void array_store(MDArray narray_src)
        {
            MDArray narray = narray_src;
            store_mult1(narray);
            //panelCalc.Refresh();
            panelCalc_Paint();
        }

        private void show_message(string s)
        {
            labelRel.Content = s;
        }

        private void print_rel()
        {
            labelRel.Content = exp0 + settings.Equal + exp1;
        }

        private string make_exp(string e0, string e1, string e2)
        {
            return e0 + settings.MultiplicationOperator + e1 + settings.Plus + e2;
        }

        private void store_mult0(MDArray narray)
        {
            exp0 = make_exp(narray.make_mult(0), narray.make_mult(1), narray.make_mult(2));
            print_rel();
        }

        private void store_mult1(MDArray narray)
        {
            exp1 = make_exp(narray.make_mult(0), narray.make_mult(1), narray.make_mult(2));
            print_rel();
        }

        private string FormatInputText()
        {
            if ( InputText.IndexOf("*") >= 0 )
            {
                return InputText.Replace("*", settings.MultiplicationOperator);
            }
            else if ( InputText.IndexOf("/") >= 0 )
            {
                return InputText.Replace("/", settings.DivisionOperator);
            }
            else
            {
                return InputText;
            }
        }

        private void SetInputText(string text)
        {
            InputText = text;
            labelInput.Content = FormatInputText();
        }

        private void ClearResult()
        {
            labelInput.Content = FormatInputText();
        }

        private void SetMultResult(string s)
        {
            if ( InputText.IndexOf("*") >= 0 )
            {
                labelInput.Content = FormatInputText() + settings.Equal + s;
            }
        }

        private void SetDivResult(string s)
        {
            if ( InputText.IndexOf("/") >= 0 )
            {
                labelInput.Content = FormatInputText() + settings.Equal + s;
            }
        }

        private void calc_start()
        {
            String text = InputText;
            int mi = text.IndexOf("*");
            int di = text.IndexOf("/");
            if ( mi >= 0 )
            {
                string n1 = text.Substring(0, mi);
                string n2 = text.Substring(mi + 1);
                if ( n1 != "" && n2 != "" )
                {
                    MultStart(n1, n2);
                }
            }
            else if ( di >= 0 )
            {
                string n1 = text.Substring(0, di);
                string n2 = text.Substring(di + 1);
                if ( n1 != "" && n2 != "" )
                {
                    DivStart(n1, n2);
                }
            }
        }

        private void MultStart(string n1, string n2)
        {
            try
            {
                array_init();
                int size = MDArray.no_point_length(n1) + MDArray.no_point_length(n2) + 1;
                MDArray narray = array_load();
                narray.set_number_size(size);
                narray.input_number(0, n1);
                narray.input_number(1, n2);
                narray.input_number(2, "0", MDArray.lower_size(n1) + MDArray.lower_size(n2));
                array_store(narray);
                store_mult0(narray);
                ClearResult();
                calc_mode = true; // 入力モード・計算モード。計算モードに設定
            }
            catch ( Exception )
            {
                show_message("エラーです");
            }
        }

        private void mult_next()
        {
            try
            {
                // かけ算を続ける
                // 計算モードのときだけ
                if ( calc_mode )
                {
                    MDArray narray = array_load();
                    if ( narray.is_zero(0) )
                    {
                        // かけ算は終了
                    }
                    else if ( narray.calc_start_digit(0) == 0 )
                    {
                        // 1の位が0なので、シフトする
                        // narray.rshift(0);
                        narray.lshift_starting_position(); // かけ算の開始位置を左にシフト
                        narray.lshift(1); // かける数は左にシフト
                    }
                    else
                    {
                        // 1の位が0ではないので、足し算をする
                        narray.add(0, 1, 2);
                    }
                    array_store(narray);
                    // かけ算は終了なので、結果を表示する
                    if ( narray.is_zero(0) )
                    {
                        SetMultResult(narray.make_mult(2));
                    }
                }
            }
            catch ( Exception )
            {
                show_message("エラーです");
            }
        }

        private void DivStart(string n1, string n2)
        {
            try
            {
                array_init();
                int size = Math.Max(MDArray.number_size(n1), MDArray.number_size(n2));
                //size = Math.Max(size, MDArray.lower_size(n1) + MDArray.lower_size(n2) + 1);
                size += Math.Max(MDArray.lower_mag(n2), MDArray.lower_mag(n1)) + 1;
                MDArray narray = array_load();
                narray.set_number_size(size);
                narray.input_number(0, "0", MDArray.lower_size(n1) - MDArray.lower_size(n2));
                narray.input_number(1, n2);
                narray.input_number(2, n1);
                narray.lshift_to_top(1, 2, 0);
                array_store(narray);
                store_mult0(narray);
                ClearResult();
                calc_mode = true; // 入力モード・計算モード。計算モードに設定
            }
            catch ( Exception )
            {
                show_message("エラーです");
            }
        }

        private void div_next()
        {
            try
            {
                // わり算を続ける
                // 計算モードのときだけ
                if ( calc_mode )
                {
                    MDArray narray = array_load();
                    if ( narray.is_zero(2) )
                    {
                        // 割り切れた。割り算は終了
                    }
                    else if ( !narray.can_sub(1, 2) )
                    {
                        // 引き算ができないので、シフトする
                        //narray.lshift(0);
                        narray.rshift_starting_position(); // かけ算の開始位置を右にシフト
                        narray.rshift(1); // わる数は右にシフト
                    }
                    else
                    {
                        // 引き算ができるので、引き算をする
                        narray.sub(0, 1, 2);
                    }
                    array_store(narray);
                    // 割り算は終了なので、結果を表示する
                    if ( narray.is_zero(2) )
                    {
                        SetDivResult(narray.make_mult(0));
                    }
                }
            }
            catch ( Exception )
            {
                show_message("エラーです");
            }
        }

        private void add_string(string str)
        {
            // 1文字入力
            // 入力モードのときだけ
            if ( !calc_mode )
            {
                if ( str.Length == 1 && (Char.IsDigit(str[0]) || str == "." || str == "*" || str == "/") )
                {
                    String text = InputText;
                    int mi = text.IndexOf("*");
                    int di = text.IndexOf("/");
                    if ( mi >= 0 )
                    {
                        if ( str == "*" || str == "/" )
                        {
                            return;
                        }
                        int pi = text.IndexOf(".", mi + 1);
                        if ( pi >= 0 )
                        {
                            if ( str == "." )
                            {
                                return;
                            }
                        }
                    }
                    else if ( di >= 0 )
                    {
                        if ( str == "*" || str == "/" )
                        {
                            return;
                        }
                        int pi = text.IndexOf(".", di + 1);
                        if ( pi >= 0 )
                        {
                            if ( str == "." )
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        int pi = text.IndexOf(".");
                        if ( pi >= 0 )
                        {
                            if ( str == "." )
                            {
                                return;
                            }
                        }
                    }
                    text += str;
                    SetInputText(text);
                }
            }
        }

        private void do_back_space()
        {
            // 1文字消去
            // 入力モードのときだけ
            if ( !calc_mode )
            {
                String str = InputText;
                str = str.Trim();
                if ( str.Length > 0 )
                {
                    str = str.Substring(0, str.Length - 1);
                    str = str.Trim();
                }
                SetInputText(str);
            }
        }

        private void do_clear()
        {
            // 全部消去
            SetInputText("");
            labelRel.Content = "";
            array_init();
            //panelCalc.Refresh();
            panelCalc_Paint();
            calc_mode = false; // 入力モード・計算モード。入力モードに設定
        }

        private void buttonN0_Click(object sender, System.EventArgs e)
        {
            add_string("0");
        }

        private void buttonN1_Click(object sender, System.EventArgs e)
        {
            add_string("1");
        }

        private void buttonN2_Click(object sender, System.EventArgs e)
        {
            add_string("2");
        }

        private void buttonN3_Click(object sender, System.EventArgs e)
        {
            add_string("3");
        }

        private void buttonN4_Click(object sender, System.EventArgs e)
        {
            add_string("4");
        }

        private void buttonN5_Click(object sender, System.EventArgs e)
        {
            add_string("5");
        }

        private void buttonN6_Click(object sender, System.EventArgs e)
        {
            add_string("6");
        }

        private void buttonN7_Click(object sender, System.EventArgs e)
        {
            add_string("7");
        }

        private void buttonN8_Click(object sender, System.EventArgs e)
        {
            add_string("8");
        }

        private void buttonN9_Click(object sender, System.EventArgs e)
        {
            add_string("9");
        }

        private void buttonDot_Click(object sender, System.EventArgs e)
        {
            add_string(".");
        }

        private void buttonMult_Click(object sender, System.EventArgs e)
        {
            add_string("*");
        }

        private void buttonDiv_Click(object sender, System.EventArgs e)
        {
            add_string("/");
        }

        private void buttonMultNext_Click(object sender, System.EventArgs e)
        {
            mult_next();
        }

        private void buttonDivNext_Click(object sender, System.EventArgs e)
        {
            div_next();
        }

        private void buttonEqual_Click(object sender, System.EventArgs e)
        {
            calc_start();
        }

        private void buttonBS_Click(object sender, System.EventArgs e)
        {
            do_back_space();
        }

        private void buttonClear_Click(object sender, System.EventArgs e)
        {
            do_clear();
        }

        //private void panelCalc_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        //{
        //    if ( main_mdarray != null )
        //    {
        //        main_mdarray.paint(e.Graphics);
        //    }
        //}

        private void panelCalc_Paint()
        {
            if ( main_mdarray != null )
            {
                panelCalc.Children.Clear();
                XGraphics g = new XGraphics(panelCalc, Font, settings.get_view_settings().UseImage, settings.get_view_settings().ImageSettings);
                main_mdarray.paint(g);
            }
        }

        // ボタンのテキストの前に「＆」をつけてもよいが、アンダーラインが表示されるのでやらない。
        // そうすると、これをすべてのボタンに設定する必要がある？
        private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // Clear ボタンはキーボードで操作できないとする。
            //System.Diagnostics.Debug.WriteLine(Convert.ToInt32(e.KeyChar));
            switch ( e.KeyChar )
            {
                case '0':
                    add_string("0");
                    buttonN0.Focus();
                    break;
                case '1':
                    add_string("1");
                    buttonN1.Focus();
                    break;
                case '2':
                    add_string("2");
                    buttonN2.Focus();
                    break;
                case '3':
                    add_string("3");
                    buttonN3.Focus();
                    break;
                case '4':
                    add_string("4");
                    buttonN4.Focus();
                    break;
                case '5':
                    add_string("5");
                    buttonN5.Focus();
                    break;
                case '6':
                    add_string("6");
                    buttonN6.Focus();
                    break;
                case '7':
                    add_string("7");
                    buttonN7.Focus();
                    break;
                case '8':
                    add_string("8");
                    buttonN8.Focus();
                    break;
                case '9':
                    add_string("9");
                    buttonN9.Focus();
                    break;
                case '.':
                    add_string(".");
                    buttonDot.Focus();
                    break;
                case '*':
                    add_string("*");
                    buttonMult.Focus();
                    break;
                case '/':
                    add_string("/");
                    buttonDiv.Focus();
                    break;
                case '=':
                    calc_start();
                    buttonEqual.Focus();
                    break;
                case '<':
                    mult_next();
                    buttonMultNext.Focus();
                    break;
                case '>':
                    div_next();
                    buttonDivNext.Focus();
                    break;
                case '\b':
                    do_back_space();
                    buttonBS.Focus();
                    break;
            }
        }

        private void menuItemView_Click(object sender, System.EventArgs e)
        {
            FormViewSettings dlg = new FormViewSettings(settings.get_view_settings());
            if ( dlg.ShowDialog() == true )
            {
                init_colors();
                //Refresh();
            }
        }

        private void menuItemVersion_Click(object sender, System.EventArgs e)
        {
            FormAboutDialog dlg = new FormAboutDialog(settings.get_view_settings());
            if ( dlg.ShowDialog() == true )
            {
            }
        }

        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            save_settings();
        }

        private void Main_Load(object sender, RoutedEventArgs e)
        {
            load_settings_post_proc();
        }

        private void menuItemLoadSettings_Click(object sender, System.EventArgs e)
        {
            load_settings_file();
        }

        private void menuItemSaveSettings_Click(object sender, System.EventArgs e)
        {
            save_settings_file();
        }

        private void menuItemHelp_Click(object sender, System.EventArgs e)
        {
            help();
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
