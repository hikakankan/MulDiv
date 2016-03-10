using System.Windows;

namespace MulDiv
{
	/// <summary>
	/// CommonImageSettings の概要の説明です。
	/// </summary>
	public class CommonImageSettings
	{
		private Size num_size;
		private Size op_size;
		private Size par_size;
		private int space_width = 2; // 2ドット

		public CommonImageSettings()
		{
		}

		public CommonImageSettings(CommonImageSettings settings)
		{
			if ( settings == null )
			{
			}
			else
			{
				num_size = settings.num_size;
				op_size = settings.op_size;
				par_size = settings.par_size;
				space_width = settings.space_width;
			}
		}

		public Size NumSize
		{
			get
			{
				return num_size;
			}
			set
			{
				num_size = value;
			}
		}

		public Size OpSize
		{
			get
			{
				return op_size;
			}
			set
			{
				op_size = value;
			}
		}

		public Size ParSize
		{
			get
			{
				return par_size;
			}
			set
			{
				par_size = value;
			}
		}

		public int SpaceWidth
		{
			get
			{
				return space_width;
			}
			set
			{
				space_width = value;
			}
		}
	}
}
