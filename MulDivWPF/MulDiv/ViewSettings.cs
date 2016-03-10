using System;
using System.Drawing;

namespace MulDiv
{
	/// <summary>
	/// ViewSettings の概要の説明です。
	/// </summary>
	public class ViewSettings
	{
		// 制御領域
		private MVColor c_body_bg;      // 背景色
		private MVColor c_body_text;    // テキストの色
		private MVColor c_button;       // ボタンの色
		private MVColor c_button_text;  // ボタンのテキストの色
		private MVColor c_textbox;      // テキストボックスの色
		private MVColor c_text_text;    // テキストボックスのテキストの色

		// 計算領域
		private MVColor c_exp_area_bg;  // 式の領域の背景色
		private MVColor c_exp_text;     // 式の文字の色

		// フォント
		private XFont font_main;                      // フォームのフォント
		private XFont font_calc;                      // 計算領域のフォント

		private float hmag;                          // 計算領域の文字の横の倍率
		private float vmag;                          // 計算領域の文字の縦の倍率

		/// <summary>
		/// イメージの設定
		/// </summary>
		private ImageSettings image_settings;

		/// <summary>
		/// イメージを使うかどうか
		/// </summary>
		private bool use_image = false;

		public ViewSettings(XFont main_font)
		{
			c_body_bg = MVColor.FromArgb(220, 240, 255);  // 背景色
			c_body_text = MVColor.FromArgb(0, 0, 0);      // テキストの色
			c_button = MVColor.FromArgb(200, 220, 255);   // ボタンの色
			c_button_text = MVColor.FromArgb(0, 0, 0);    // ボタンのテキストの色
			c_textbox = MVColor.FromArgb(255, 245, 245);  // テキストボックスの色
			c_text_text = MVColor.FromArgb(0, 0, 0);      // テキストボックスのテキストの色

			c_exp_area_bg = MVColor.FromArgb(255, 245, 245);  // 式の領域の背景色
			c_exp_text = MVColor.FromArgb(0, 0, 0);           // 式の文字の色

			font_main = main_font;
			//font_calc = main_font; // 計算領域のフォントは仮にメインウィンドウのフォントを設定しておく
			font_calc = new XFont(new System.Windows.Media.Typeface("Times New Roman"), 40);

			hmag = 1.0f;
			vmag = 1.0f;

			image_settings = new ImageSettings();
			use_image = false;
		}

        public ViewSettings(ViewSettings view_settings)
        {
            c_body_bg = view_settings.c_body_bg;             // 背景色
            c_body_text = view_settings.c_body_text;         // テキストの色
            c_button = view_settings.c_button;               // ボタンの色
            c_button_text = view_settings.c_button_text;     // ボタンのテキストの色
            c_textbox = view_settings.c_textbox;             // テキストボックスの色
            c_text_text = view_settings.c_text_text;         // テキストボックスのテキストの色

            c_exp_area_bg = view_settings.c_exp_area_bg;     // 式の領域の背景色
            c_exp_text = view_settings.c_exp_text;           // 式の文字の色

            font_main = view_settings.font_main;             // メインウィンドウのフォント
            font_calc = view_settings.font_calc;             // 計算領域のフォント

            image_settings = view_settings.image_settings;
            use_image = view_settings.use_image;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public MVColor BodyBackColor
		{
			get
			{
				return c_body_bg;
			}
			set
			{
				c_body_bg = value;
			}
		}

		/// <summary>
		/// テキストの色
		/// </summary>
		public MVColor BodyTextColor
		{
			get
			{
				return c_body_text;
			}
			set
			{
				c_body_text = value;
			}
		}

		/// <summary>
		/// ボタンの色
		/// </summary>
		public MVColor ButtonBackColor
		{
			get
			{
				return c_button;
			}
			set
			{
				c_button = value;
			}
		}

		/// <summary>
		/// ボタンの文字の色
		/// </summary>
		public MVColor ButtonTextColor
		{
			get
			{
				return c_button_text;
			}
			set
			{
				c_button_text = value;
			}
		}

		/// <summary>
		/// テキストボックスの色
		/// </summary>
		public MVColor TextBackColor
		{
			get
			{
				return c_textbox;
			}
			set
			{
				c_textbox = value;
			}
		}

		/// <summary>
		/// テキストボックスのテキストの色
		/// </summary>
		public MVColor TextTextColor
		{
			get
			{
				return c_text_text;
			}
			set
			{
				c_text_text = value;
			}
		}

		/// <summary>
		/// 計算領域の文字の色
		/// </summary>
		public MVColor CalcAreaTextColor
		{
			get
			{
				return c_exp_text;
			}
			set
			{
				c_exp_text = value;
			}
		}

		/// <summary>
		/// 計算領域の背景色
		/// </summary>
		public MVColor CalcAreaBackColor
		{
			get
			{
				return c_exp_area_bg;
			}
			set
			{
				c_exp_area_bg = value;
			}
		}

		///// <summary>
		///// ボタンに色を設定する
		///// </summary>
		///// <param name="button">ボタン</param>
		//public void SetButtonColors(System.Windows.Forms.Button button)
		//{
		//	button.BackColor = c_button;
		//	button.ForeColor = c_button_text;
		//}

		///// <summary>
		///// テキストボックスに色を設定する
		///// </summary>
		///// <param name="text">テキストボックス</param>
		//public void SetTextBoxColors(System.Windows.Forms.TextBox text)
		//{
		//	text.BackColor = c_textbox;
		//	text.ForeColor = c_text_text;
		//}

		///// <summary>
		///// テキストボックス(実際はラベル)に色を設定する
		///// </summary>
		///// <param name="text">テキストボックス</param>
		//public void SetTextBoxColors(System.Windows.Forms.Label text)
		//{
		//	text.BackColor = c_textbox;
		//	text.ForeColor = c_text_text;
		//}

		///// <summary>
		///// チェックボックスに色を設定する
		///// </summary>
		///// <param name="check">チェックボックス</param>
		//public void SetCheckBoxColors(System.Windows.Forms.CheckBox check)
		//{
		//	check.ForeColor = c_text_text;
		//}

		/// <summary>
		/// メインウィンドウのフォント
		/// </summary>
		public XFont MainFont
		{
			get
			{
				return font_main;
			}
			set
			{
				font_main = value;
			}
		}

		/// <summary>
		/// 計算領域のフォント
		/// </summary>
		public XFont CalcAreaFont
		{
			get
			{
				return font_calc;
			}
			set
			{
				font_calc = value;
			}
		}

		/// <summary>
		/// 計算領域の文字の横の倍率
		/// </summary>
		public float CalcAreaHorizontalMagnitude
		{
			get
			{
				return hmag;
			}
			set
			{
				hmag = value;
			}
		}

		/// <summary>
		/// 計算領域の文字の縦の倍率
		/// </summary>
		public float CalcAreaVerticalMagnitude
		{
			get
			{
				return vmag;
			}
			set
			{
				vmag = value;
			}
		}

		/// <summary>
		/// イメージの設定
		/// </summary>
		public ImageSettings ImageSettings
		{
			get
			{
				return image_settings;
			}
			set
			{
				image_settings = value;
			}
		}

		/// <summary>
		/// イメージを使うかどうか
		/// </summary>
		public bool UseImage
		{
			get
			{
				return use_image;
			}
			set
			{
				use_image = value;
			}
		}
	}
}
