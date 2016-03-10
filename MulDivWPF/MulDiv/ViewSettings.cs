using System;
using System.Drawing;

namespace MulDiv
{
	/// <summary>
	/// ViewSettings �̊T�v�̐����ł��B
	/// </summary>
	public class ViewSettings
	{
		// ����̈�
		private MVColor c_body_bg;      // �w�i�F
		private MVColor c_body_text;    // �e�L�X�g�̐F
		private MVColor c_button;       // �{�^���̐F
		private MVColor c_button_text;  // �{�^���̃e�L�X�g�̐F
		private MVColor c_textbox;      // �e�L�X�g�{�b�N�X�̐F
		private MVColor c_text_text;    // �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F

		// �v�Z�̈�
		private MVColor c_exp_area_bg;  // ���̗̈�̔w�i�F
		private MVColor c_exp_text;     // ���̕����̐F

		// �t�H���g
		private XFont font_main;                      // �t�H�[���̃t�H���g
		private XFont font_calc;                      // �v�Z�̈�̃t�H���g

		private float hmag;                          // �v�Z�̈�̕����̉��̔{��
		private float vmag;                          // �v�Z�̈�̕����̏c�̔{��

		/// <summary>
		/// �C���[�W�̐ݒ�
		/// </summary>
		private ImageSettings image_settings;

		/// <summary>
		/// �C���[�W���g�����ǂ���
		/// </summary>
		private bool use_image = false;

		public ViewSettings(XFont main_font)
		{
			c_body_bg = MVColor.FromArgb(220, 240, 255);  // �w�i�F
			c_body_text = MVColor.FromArgb(0, 0, 0);      // �e�L�X�g�̐F
			c_button = MVColor.FromArgb(200, 220, 255);   // �{�^���̐F
			c_button_text = MVColor.FromArgb(0, 0, 0);    // �{�^���̃e�L�X�g�̐F
			c_textbox = MVColor.FromArgb(255, 245, 245);  // �e�L�X�g�{�b�N�X�̐F
			c_text_text = MVColor.FromArgb(0, 0, 0);      // �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F

			c_exp_area_bg = MVColor.FromArgb(255, 245, 245);  // ���̗̈�̔w�i�F
			c_exp_text = MVColor.FromArgb(0, 0, 0);           // ���̕����̐F

			font_main = main_font;
			//font_calc = main_font; // �v�Z�̈�̃t�H���g�͉��Ƀ��C���E�B���h�E�̃t�H���g��ݒ肵�Ă���
			font_calc = new XFont(new System.Windows.Media.Typeface("Times New Roman"), 40);

			hmag = 1.0f;
			vmag = 1.0f;

			image_settings = new ImageSettings();
			use_image = false;
		}

        public ViewSettings(ViewSettings view_settings)
        {
            c_body_bg = view_settings.c_body_bg;             // �w�i�F
            c_body_text = view_settings.c_body_text;         // �e�L�X�g�̐F
            c_button = view_settings.c_button;               // �{�^���̐F
            c_button_text = view_settings.c_button_text;     // �{�^���̃e�L�X�g�̐F
            c_textbox = view_settings.c_textbox;             // �e�L�X�g�{�b�N�X�̐F
            c_text_text = view_settings.c_text_text;         // �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F

            c_exp_area_bg = view_settings.c_exp_area_bg;     // ���̗̈�̔w�i�F
            c_exp_text = view_settings.c_exp_text;           // ���̕����̐F

            font_main = view_settings.font_main;             // ���C���E�B���h�E�̃t�H���g
            font_calc = view_settings.font_calc;             // �v�Z�̈�̃t�H���g

            image_settings = view_settings.image_settings;
            use_image = view_settings.use_image;
        }

        /// <summary>
        /// �w�i�F
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
		/// �e�L�X�g�̐F
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
		/// �{�^���̐F
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
		/// �{�^���̕����̐F
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
		/// �e�L�X�g�{�b�N�X�̐F
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
		/// �e�L�X�g�{�b�N�X�̃e�L�X�g�̐F
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
		/// �v�Z�̈�̕����̐F
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
		/// �v�Z�̈�̔w�i�F
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
		///// �{�^���ɐF��ݒ肷��
		///// </summary>
		///// <param name="button">�{�^��</param>
		//public void SetButtonColors(System.Windows.Forms.Button button)
		//{
		//	button.BackColor = c_button;
		//	button.ForeColor = c_button_text;
		//}

		///// <summary>
		///// �e�L�X�g�{�b�N�X�ɐF��ݒ肷��
		///// </summary>
		///// <param name="text">�e�L�X�g�{�b�N�X</param>
		//public void SetTextBoxColors(System.Windows.Forms.TextBox text)
		//{
		//	text.BackColor = c_textbox;
		//	text.ForeColor = c_text_text;
		//}

		///// <summary>
		///// �e�L�X�g�{�b�N�X(���ۂ̓��x��)�ɐF��ݒ肷��
		///// </summary>
		///// <param name="text">�e�L�X�g�{�b�N�X</param>
		//public void SetTextBoxColors(System.Windows.Forms.Label text)
		//{
		//	text.BackColor = c_textbox;
		//	text.ForeColor = c_text_text;
		//}

		///// <summary>
		///// �`�F�b�N�{�b�N�X�ɐF��ݒ肷��
		///// </summary>
		///// <param name="check">�`�F�b�N�{�b�N�X</param>
		//public void SetCheckBoxColors(System.Windows.Forms.CheckBox check)
		//{
		//	check.ForeColor = c_text_text;
		//}

		/// <summary>
		/// ���C���E�B���h�E�̃t�H���g
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
		/// �v�Z�̈�̃t�H���g
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
		/// �v�Z�̈�̕����̉��̔{��
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
		/// �v�Z�̈�̕����̏c�̔{��
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
		/// �C���[�W�̐ݒ�
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
		/// �C���[�W���g�����ǂ���
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
