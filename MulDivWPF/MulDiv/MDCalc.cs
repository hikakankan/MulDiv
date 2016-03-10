using System;
using System.Windows.Media;
using System.Windows;

namespace MulDiv
{
	/// <summary>
	/// MDCalc �̊T�v�̐����ł��B
	/// </summary>
	public class MDCalc
	{
//		private System.Drawing.Color c_area_bg;       // �v�Z�̗̈�̔w�i�F
//		private System.Drawing.Color c_text;          // �v�Z�̕����̐F
//		private System.Drawing.Font font_normal;
//		private System.Drawing.Pen pen_text;
//		private System.Drawing.SolidBrush brush_text;
//		private int char_width = 40;
//		private int char_height = 40;
		private int x_margin = 10;
		private int y_margin = 10;
		private const float hmag = 0.6f;
		private const float vmag = 0.6f;

		private ViewSettings settings;

		public MDCalc(ViewSettings view)
		{
//			c_area_bg = System.Drawing.Color.FromArgb(255, 245, 245); // �v�Z�̗̈�̔w�i�F
//			c_text = System.Drawing.Color.FromArgb(0, 0, 0);          // �v�Z�̕����̐F
//			font_normal = new System.Drawing.Font("Times New Roman", 40);
//			pen_text = new System.Drawing.Pen(c_text);
//			brush_text = new System.Drawing.SolidBrush(c_text);
			settings = view;
		}

		public void paint(XGraphics g, string str, int col, int row)
		{
			if ( str != "" )
			{
				if ( settings.UseImage )
				{
					int char_width = settings.ImageSettings.GetWidth("0.");
					int char_height = settings.ImageSettings.GetHeight();
					if ( str == "." )
					{
						int width = settings.ImageSettings.GetWidth(".");
						settings.ImageSettings.DrawString(str, col * char_width - width + x_margin, row * char_height + y_margin, g);
					}
					else
					{
						settings.ImageSettings.DrawString(str, col * char_width + x_margin, row * char_height + y_margin, g);
					}
				}
				else
				{
					XFont font = settings.CalcAreaFont;
					Size size = g.MeasureString("0", font);
					int char_width = (int)(size.Width * settings.CalcAreaHorizontalMagnitude * hmag);
					int char_height = (int)(size.Height * settings.CalcAreaVerticalMagnitude * vmag);
					Brush brush_text = new SolidColorBrush(settings.CalcAreaTextColor.WColor);
					if ( str == "." )
					{
						g.DrawString(str, font, brush_text, (int)((col - 0.4f) * char_width + x_margin), row * char_height + y_margin);
					}
					else
					{
						g.DrawString(str, font, brush_text, col * char_width + x_margin, row * char_height + y_margin);
					}
				}
			}
		}
	}
}
