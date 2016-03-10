using System;

namespace MulDiv
{
	/// <summary>
	/// MDSubArray の概要の説明です。
	/// </summary>
	public class MDSubArray
	{
		private System.Collections.ArrayList array;
		private int point_position; // 先頭から何個目に小数点があるかを表す

		public MDSubArray()
		{
			array = new System.Collections.ArrayList();
		}

		public string this[int index]
		{
			get
			{
				for ( int i = array.Count; i <= index; i++ )
				{
					array.Add("");
				}
				return (string)array[index];
			}
			set
			{
				for ( int i = array.Count; i <= index; i++ )
				{
					array.Add("");
				}
				array[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return array.Count;
			}
		}

		public int PointPosition
		{
			get
			{
				return point_position;
			}
			set
			{
				point_position = value;
			}
		}

		public void lshift_point()
		{
			point_position--;
		}

		public void lshift_point(int n)
		{
			point_position -= n;
		}

		public void rshift_point()
		{
			point_position++;
		}

		private bool is_zero(int index)
		{
			return this[index] == "" || this[index] == "0";
		}

		private bool are_zeroes(int index1, int index2)
		{
			for ( int i = index1; i < index2; i++ )
			{
				if ( !is_zero(i) )
				{
					return false;
				}
			}
			return true;
		}

		// 小数点以下が全部0
		public bool is_integer()
		{
			return are_zeroes(PointPosition, Count);
		}

		// 余分な0を消去し、必要な0は補う
		public void normalize()
		{
			for ( int i = 0; i < Count; i++ )
			{
				if ( is_zero(i) )
				{
					if ( i < PointPosition )
					{
						// 小数点より上
						if ( are_zeroes(0, i) && (i != PointPosition - 1) )
						{
							// 上位が全部0
							this[i] = "";
						}
						else
						{
							this[i] = "0";
						}
					}
					else
					{
						// 小数点より下
						if ( are_zeroes(i, Count) )
						{
							// 下位が全部0
							this[i] = "";
						}
						else
						{
							this[i] = "0";
						}
					}
				}
			}
		}
	}
}
