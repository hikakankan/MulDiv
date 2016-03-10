using System;

namespace MulDiv
{
	/// <summary>
	/// MDSubArray �̊T�v�̐����ł��B
	/// </summary>
	public class MDSubArray
	{
		private System.Collections.ArrayList array;
		private int point_position; // �擪���牽�ڂɏ����_�����邩��\��

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

		// �����_�ȉ����S��0
		public bool is_integer()
		{
			return are_zeroes(PointPosition, Count);
		}

		// �]����0���������A�K�v��0�͕₤
		public void normalize()
		{
			for ( int i = 0; i < Count; i++ )
			{
				if ( is_zero(i) )
				{
					if ( i < PointPosition )
					{
						// �����_����
						if ( are_zeroes(0, i) && (i != PointPosition - 1) )
						{
							// ��ʂ��S��0
							this[i] = "";
						}
						else
						{
							this[i] = "0";
						}
					}
					else
					{
						// �����_��艺
						if ( are_zeroes(i, Count) )
						{
							// ���ʂ��S��0
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
