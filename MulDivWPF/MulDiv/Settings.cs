using System;
using System.Xml;
using System.Runtime.InteropServices;
using System.IO;
//using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace MulDiv
{
	/// <summary>
	/// Settings の概要の説明です。
	/// </summary>
	public class Settings
	{
		private string settings_file_name; // 設定ファイル名

		private ViewSettings view_settings;

		public Settings(string settings_file, XFont main_font)
		{
			settings_file_name = settings_file;
			view_settings = new ViewSettings(main_font);
		}

		/// <summary>
		/// 表示の設定を取得する
		/// </summary>
		/// <returns>表示の設定</returns>
		public ViewSettings get_view_settings()
		{
			return view_settings;
		}

		/// <summary>
		/// XMLのセクションの対応するエレメントを取得する
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="parent">エレメントを探す親のノード</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <param name="create">取得できなかったときは作成する</param>
		/// <returns>セクションに対応するノード</returns>
		private XmlElement get_section(string section, XmlNode parent, XmlDocument doc, bool create)
		{
			if ( parent.HasChildNodes )
			{
				foreach ( XmlNode node in parent.ChildNodes )
				{
					if ( node.NodeType == XmlNodeType.Element && node.Name == section )
					{
						return (XmlElement)node;
					}
				}
			}
			if ( create )
			{
				XmlElement child = doc.CreateElement(section);
				parent.AppendChild(child);
				return child;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void get_setting_string(string section, string key, ref string val, XmlDocument doc)
		{
			try
			{
				XmlElement conf = doc.DocumentElement;
				if ( conf != null )
				{
					XmlElement sec = get_section(section, conf, doc, false);
					if ( sec != null )
					{
						val = sec.GetAttribute(key);
					}
				}
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private string get_setting_string(string section, string key, string val, XmlDocument doc)
		{
			try
			{
				string setting = val;
				get_setting_string(section, key, ref setting, doc);
				return setting;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private int get_setting_integer(string section, string key, int val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return Convert.ToInt32(setting);
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private bool get_setting_bool(string section, string key, bool val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return setting != "f";
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private MVColor get_setting_color(string section, string key, MVColor val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return MVColor.FromArgb(Convert.ToInt32(setting, 16));
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private float get_setting_real(string section, string key, float val, XmlDocument doc)
		{
			try
			{
				string setting = "";
				get_setting_string(section, key, ref setting, doc);
				if ( setting != "" )
				{
					return Convert.ToSingle(setting);
				}
				return val;
			}
			catch ( Exception )
			{
				return val;
			}
		}

        /// <summary>
        /// フォントの設定を読み込む
        /// </summary>
        /// <param name="section">設定のセクション</param>
        /// <param name="font">設定のデフォルトの値</param>
        /// <param name="doc">XMLのドキュメント</param>
        /// <returns>設定の値</returns>
        private XFont get_setting_font(string section, XFont font, XmlDocument doc)
        {
            try
            {
                string fontFamilyName = get_setting_string(section, "family", font.Typeface.FontFamily.Source, doc);
                float fontSize = get_setting_real(section, "size", font.Height, doc);
                string fontStyleString = get_setting_string(section, "style", font.FontStyleString, doc);
                string fontWeightString = get_setting_string(section, "weight", font.FontWeightString, doc);
                return new XFont(new Typeface(new FontFamily(fontFamilyName), XFont.GetFontStyleFromString(fontStyleString), XFont.GetFontWeightFromString(fontWeightString), FontStretches.Normal), (int)fontSize);
            }
            catch ( Exception )
            {
                return font;
            }
        }

		/// <summary>
		/// サイズを読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="swidth">幅のキー</param>
		/// <param name="sheight">高さのキー</param>
		/// <param name="size">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private Size get_setting_size(string section, string swidth, string sheight, Size size, XmlDocument doc)
		{
			try
			{
				int width = get_setting_integer(section, swidth, (int)size.Width, doc);
				int height = get_setting_integer(section, sheight, (int)size.Height, doc);
				return new Size(width, height);
			}
			catch ( Exception )
			{
				return size;
			}
		}

		/// <summary>
		/// イメージの設定を読み込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="settings">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		/// <returns>設定の値</returns>
		private PrivateImageSettings get_setting_image(string section, PrivateImageSettings settings, XmlDocument doc)
		{
			try
			{
				PrivateImageSettings image = new PrivateImageSettings(settings);
				image.Path = get_setting_string(section, "path", image.Path, doc);
				image.Size = get_setting_size(section, "width", "height", image.Size, doc);
				image.UseDefault = get_setting_bool(section, "def", image.UseDefault, doc);
				return image;
			}
			catch ( Exception )
			{
				return settings;
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_string(string section, string key, string val, XmlDocument doc)
		{
			try
			{
				XmlElement conf = doc.DocumentElement;
				if ( conf == null )
				{
					conf = doc.CreateElement("configuration");
					doc.AppendChild(conf);
				}
				get_section(section, conf, doc, true).SetAttribute(key, val);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_integer(string section, string key, int val, XmlDocument doc)
		{
			try
			{
				set_setting_string(section, key, Convert.ToString(val), doc);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_bool(string section, string key, bool val, XmlDocument doc)
		{
			try
			{
				if ( val )
				{
					set_setting_string(section, key, "t", doc);
				}
				else
				{
					set_setting_string(section, key, "f", doc);
				}
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_color(string section, string key, MVColor val, XmlDocument doc)
		{
			try
			{
				set_setting_string(section, key, Convert.ToString(val.ToArgb(), 16), doc);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="key">設定のキー</param>
		/// <param name="val">設定の値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_real(string section, string key, float val, XmlDocument doc)
		{
			try
			{
				set_setting_string(section, key, Convert.ToString(val), doc);
			}
			catch ( Exception )
			{
			}
		}

        /// <summary>
        /// フォントの設定を書き込む
        /// </summary>
        /// <param name="section">設定のセクション</param>
        /// <param name="font">設定の値</param>
        /// <param name="doc">XMLのドキュメント</param>
        private void set_setting_font(string section, XFont font, XmlDocument doc)
        {
            try
            {
                set_setting_string(section, "family", font.Typeface.FontFamily.Source, doc);
                set_setting_real(section, "size", font.Height, doc);
                set_setting_string(section, "style", font.FontStyleString, doc);
                set_setting_string(section, "weight", font.FontWeightString, doc);
            }
            catch ( Exception )
            {
            }
        }

		/// <summary>
		/// サイズを書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="swidth">幅のキー</param>
		/// <param name="sheight">高さのキー</param>
		/// <param name="size">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_size(string section, string swidth, string sheight, Size size, XmlDocument doc)
		{
			try
			{
				set_setting_integer(section, swidth, (int)size.Width, doc);
				set_setting_integer(section, sheight, (int)size.Height, doc);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// イメージの設定を書き込む
		/// </summary>
		/// <param name="section">設定のセクション</param>
		/// <param name="settings">設定のデフォルトの値</param>
		/// <param name="doc">XMLのドキュメント</param>
		private void set_setting_image(string section, PrivateImageSettings settings, XmlDocument doc)
		{
			try
			{
				set_setting_string(section, "path", settings.Path, doc);
				set_setting_size(section, "width", "height", settings.Size, doc);
				set_setting_bool(section, "def", settings.UseDefault, doc);
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		/// <param name="path">設定ファイルのパス</param>
		public void load_settings(string path)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				XmlTextReader reader = new XmlTextReader(path);
				doc.Load(reader);
				reader.Close();

				left = get_setting_integer("window", "left", left, doc);
				top = get_setting_integer("window", "top", top, doc);
				width = get_setting_integer("window", "width", width, doc);
				height = get_setting_integer("window", "height", height, doc);

				view_settings.BodyBackColor = get_setting_color("color", "body", view_settings.BodyBackColor, doc);
				view_settings.BodyTextColor = get_setting_color("color", "bodytext", view_settings.BodyTextColor, doc);
				view_settings.ButtonBackColor = get_setting_color("color", "button", view_settings.ButtonBackColor, doc);
				view_settings.ButtonTextColor = get_setting_color("color", "buttontext", view_settings.ButtonTextColor, doc);
				view_settings.TextBackColor = get_setting_color("color", "textback", view_settings.TextBackColor, doc);
				view_settings.TextTextColor = get_setting_color("color", "text", view_settings.TextTextColor, doc);
				view_settings.CalcAreaTextColor = get_setting_color("color", "calctext", view_settings.CalcAreaTextColor, doc);
				view_settings.CalcAreaBackColor = get_setting_color("color", "calcback", view_settings.CalcAreaBackColor, doc);

				add_op = get_setting_string("operator", "add", add_op, doc);
				mult_op = get_setting_string("operator", "mult", mult_op, doc);
				div_op = get_setting_string("operator", "div", div_op, doc);
				eq_op = get_setting_string("operator", "eq", eq_op, doc);

				view_settings.MainFont = get_setting_font("font", view_settings.MainFont, doc);
				view_settings.CalcAreaFont = get_setting_font("calcfont", view_settings.CalcAreaFont, doc);

				view_settings.CalcAreaHorizontalMagnitude = get_setting_real("calcarea", "hmag", view_settings.CalcAreaHorizontalMagnitude, doc);
				view_settings.CalcAreaVerticalMagnitude = get_setting_real("calcarea", "vmag", view_settings.CalcAreaVerticalMagnitude, doc);

				view_settings.UseImage = get_setting_bool("img", "use", view_settings.UseImage, doc);
				view_settings.ImageSettings.CommonSettings.NumSize = get_setting_size("img", "nwidth", "nheight", view_settings.ImageSettings.CommonSettings.NumSize, doc);
//				view_settings.ImageSettings.CommonSettings.OpSize = get_setting_size("img", "owidth", "oheight", view_settings.ImageSettings.CommonSettings.OpSize, doc);
//				view_settings.ImageSettings.CommonSettings.ParSize = get_setting_size("img", "pwidth", "pheight", view_settings.ImageSettings.CommonSettings.ParSize, doc);
//				view_settings.ImageSettings.CommonSettings.SpaceWidth = get_setting_integer("img", "swidth", view_settings.ImageSettings.CommonSettings.SpaceWidth, doc);

				view_settings.ImageSettings.SetSettings('0', get_setting_image("img0", view_settings.ImageSettings.GetSettings('0'), doc));
				view_settings.ImageSettings.SetSettings('1', get_setting_image("img1", view_settings.ImageSettings.GetSettings('1'), doc));
				view_settings.ImageSettings.SetSettings('2', get_setting_image("img2", view_settings.ImageSettings.GetSettings('2'), doc));
				view_settings.ImageSettings.SetSettings('3', get_setting_image("img3", view_settings.ImageSettings.GetSettings('3'), doc));
				view_settings.ImageSettings.SetSettings('4', get_setting_image("img4", view_settings.ImageSettings.GetSettings('4'), doc));
				view_settings.ImageSettings.SetSettings('5', get_setting_image("img5", view_settings.ImageSettings.GetSettings('5'), doc));
				view_settings.ImageSettings.SetSettings('6', get_setting_image("img6", view_settings.ImageSettings.GetSettings('6'), doc));
				view_settings.ImageSettings.SetSettings('7', get_setting_image("img7", view_settings.ImageSettings.GetSettings('7'), doc));
				view_settings.ImageSettings.SetSettings('8', get_setting_image("img8", view_settings.ImageSettings.GetSettings('8'), doc));
				view_settings.ImageSettings.SetSettings('9', get_setting_image("img9", view_settings.ImageSettings.GetSettings('9'), doc));
				view_settings.ImageSettings.SetSettings('.', get_setting_image("imgd", view_settings.ImageSettings.GetSettings('.'), doc));
//				view_settings.ImageSettings.SetSettings('+', get_setting_image("imgp", view_settings.ImageSettings.GetSettings('+'), doc));
//				view_settings.ImageSettings.SetSettings('-', get_setting_image("imgm", view_settings.ImageSettings.GetSettings('-'), doc));
//				view_settings.ImageSettings.SetSettings('*', get_setting_image("imgt", view_settings.ImageSettings.GetSettings('*'), doc));
//				view_settings.ImageSettings.SetSettings('/', get_setting_image("imgv", view_settings.ImageSettings.GetSettings('/'), doc));
//				view_settings.ImageSettings.SetSettings('(', get_setting_image("imgo", view_settings.ImageSettings.GetSettings('('), doc));
//				view_settings.ImageSettings.SetSettings(')', get_setting_image("imgc", view_settings.ImageSettings.GetSettings(')'), doc));
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を読み込む
		/// </summary>
		public void load_settings()
		{
			load_settings(settings_file_name);
		}

		private string file_path;

        /// <summary>
        /// ファイルダイアログでファイル名を指定して設定を読み込む
        /// </summary>
        /// <returns>読み込んだときtrue、キャンセルのときfalse</returns>
        public bool load_settings_file()
        {
            try
            {
                if ( file_path == null )
                {
                    file_path = settings_file_name;
                }
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "情報ファイル (*.mdi)|*.mdi|すべてのファイル (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.FileName = file_path;
                openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog1.RestoreDirectory = true;
                if ( openFileDialog1.ShowDialog() == true )
                {
                    file_path = openFileDialog1.FileName;
                    load_settings(file_path);
                    return true;
                }
            }
            catch ( Exception )
            {
            }
            return false;
        }

		/// <summary>
		/// 設定を保存する
		/// </summary>
		/// <param name="path">設定ファイルのパス</param>
		public void save_settings(string path)
		{
			try
			{
				XmlDocument doc = new XmlDocument();

				set_setting_integer("window", "left", left, doc);
				set_setting_integer("window", "top", top, doc);
				set_setting_integer("window", "width", width, doc);
				set_setting_integer("window", "height", height, doc);

				set_setting_color("color", "body", view_settings.BodyBackColor, doc);
				set_setting_color("color", "bodytext", view_settings.BodyTextColor, doc);
				set_setting_color("color", "button", view_settings.ButtonBackColor, doc);
				set_setting_color("color", "buttontext", view_settings.ButtonTextColor, doc);
				set_setting_color("color", "textback", view_settings.TextBackColor, doc);
				set_setting_color("color", "text", view_settings.TextTextColor, doc);
				set_setting_color("color", "calctext", view_settings.CalcAreaTextColor, doc);
				set_setting_color("color", "calcback", view_settings.CalcAreaBackColor, doc);

				set_setting_string("operator", "add", add_op, doc);
				set_setting_string("operator", "mult", mult_op, doc);
				set_setting_string("operator", "div", div_op, doc);
				set_setting_string("operator", "eq", eq_op, doc);

				set_setting_font("font", view_settings.MainFont, doc);
				set_setting_font("calcfont", view_settings.CalcAreaFont, doc);

				set_setting_real("calcarea", "hmag", view_settings.CalcAreaHorizontalMagnitude, doc);
				set_setting_real("calcarea", "vmag", view_settings.CalcAreaVerticalMagnitude, doc);

				set_setting_bool("img", "use", view_settings.UseImage, doc);
				set_setting_size("img", "nwidth", "nheight", view_settings.ImageSettings.CommonSettings.NumSize, doc);
//				set_setting_size("img", "owidth", "oheight", view_settings.ImageSettings.CommonSettings.OpSize, doc);
//				set_setting_size("img", "pwidth", "pheight", view_settings.ImageSettings.CommonSettings.ParSize, doc);
//				set_setting_integer("img", "swidth", view_settings.ImageSettings.CommonSettings.SpaceWidth, doc);

				set_setting_image("img0", view_settings.ImageSettings.GetSettings('0'), doc);
				set_setting_image("img1", view_settings.ImageSettings.GetSettings('1'), doc);
				set_setting_image("img2", view_settings.ImageSettings.GetSettings('2'), doc);
				set_setting_image("img3", view_settings.ImageSettings.GetSettings('3'), doc);
				set_setting_image("img4", view_settings.ImageSettings.GetSettings('4'), doc);
				set_setting_image("img5", view_settings.ImageSettings.GetSettings('5'), doc);
				set_setting_image("img6", view_settings.ImageSettings.GetSettings('6'), doc);
				set_setting_image("img7", view_settings.ImageSettings.GetSettings('7'), doc);
				set_setting_image("img8", view_settings.ImageSettings.GetSettings('8'), doc);
				set_setting_image("img9", view_settings.ImageSettings.GetSettings('9'), doc);
				set_setting_image("imgd", view_settings.ImageSettings.GetSettings('.'), doc);
//				set_setting_image("imgp", view_settings.ImageSettings.GetSettings('+'), doc);
//				set_setting_image("imgm", view_settings.ImageSettings.GetSettings('-'), doc);
//				set_setting_image("imgt", view_settings.ImageSettings.GetSettings('*'), doc);
//				set_setting_image("imgv", view_settings.ImageSettings.GetSettings('/'), doc);
//				set_setting_image("imgo", view_settings.ImageSettings.GetSettings('('), doc);
//				set_setting_image("imgc", view_settings.ImageSettings.GetSettings(')'), doc);

				XmlTextWriter writer = new XmlTextWriter(path, null);
				writer.Formatting = Formatting.Indented;
				doc.Save(writer);
				writer.Close();
			}
			catch ( Exception )
			{
			}
		}

		/// <summary>
		/// 設定を保存する
		/// </summary>
		public void save_settings()
		{
			save_settings(settings_file_name);
		}

        /// <summary>
        /// ファイルダイアログでファイル名を指定して設定を保存する
        /// </summary>
        public void save_settings_file()
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.DefaultExt = ".txt";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.Filter = "情報ファイル (*.mdi)|*.mdi|すべてのファイル (*.*)|*.*";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.FileName = "";
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                saveFileDialog1.RestoreDirectory = true;
                if ( saveFileDialog1.ShowDialog() == true )
                {
                    file_path = saveFileDialog1.FileName;
                    save_settings(file_path);
                }
            }
            catch ( Exception )
            {
            }
        }

		private int left = 80;
		private int top = 80;
		private int width = 272;
		private int height = 294;
		private string add_op = " ＋ ";
		private string mult_op = " × ";
		private string div_op = " ÷ ";
		private string eq_op = " ＝ ";

		/// <summary>
		/// ウィンドウの位置(左)
		/// </summary>
		public int Left
		{
			get
			{
				return left;
			}
			set
			{
				left = value;
			}
		}

		/// <summary>
		/// ウィンドウの位置(上)
		/// </summary>
		public int Top
		{
			get
			{
				return top;
			}
			set
			{
				top = value;
			}
		}

		/// <summary>
		/// ウィンドウの幅
		/// </summary>
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}

		/// <summary>
		/// ウィンドウの高さ
		/// </summary>
		public int Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
			}
		}

		/// <summary>
		/// たし算の記号
		/// </summary>
		public string Plus
		{
			get
			{
				return add_op;
			}
			set
			{
				add_op = value;
			}
		}

		/// <summary>
		/// かけ算の記号
		/// </summary>
		public string MultiplicationOperator
		{
			get
			{
				return mult_op;
			}
			set
			{
				mult_op = value;
			}
		}

		/// <summary>
		/// わり算の記号
		/// </summary>
		public string DivisionOperator
		{
			get
			{
				return div_op;
			}
			set
			{
				div_op = value;
			}
		}

		/// <summary>
		/// 等号
		/// </summary>
		public string Equal
		{
			get
			{
				return eq_op;
			}
			set
			{
				eq_op = value;
			}
		}
	}
}
