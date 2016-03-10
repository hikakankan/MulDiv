using System;

namespace MulDiv
{
	/// <summary>
	/// MDArray �̊T�v�̐����ł��B
	/// </summary>
	public class MDArray
	{
		private System.Collections.ArrayList array;
		private const int row_count = 3;
		private MDCalc calc;
		private int starting_position; // �擪���琔�����A�����Z�̊J�n�ʒu�B�����͂��̈ʒu�ōs���B

		private ViewSettings settings;

		public MDArray(ViewSettings view)
		{
			array = new System.Collections.ArrayList();
			for ( int i = 0; i < row_count; i++ )
			{
				MDSubArray nsubarray = new MDSubArray();
				this[i] = nsubarray;
			}
			calc = new MDCalc(view);
			starting_position = 0;

			settings = view;
		}

		public void set_number_size(int size)
		{
			for ( int i = 0; i < row_count; i++ )
			{
				for ( int j = 0; j < size; j++ )
				{
					this[i][j] = "";
				}
				this[i].PointPosition = size;
			}
			starting_position = size;
		}

		public MDSubArray this[int index]
		{
			get
			{
				return (MDSubArray)array[index];
			}
			set
			{
				for ( int i = array.Count; i <= index; i++ )
				{
					array.Add(null);
				}
				array[index] = value;
			}
		}

		private int MaxCount()
		{
			int n = 0;
			for ( int i = 0; i < array.Count; i++ )
			{
				n = Math.Max(n, this[i].Count);
			}
			return n;
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

		public int getnum(string s)
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

		// �e row �̕\������Ԃ�
		// row: ���s�ڂ�
		public string make_mult(int row)
		{
			int i;
			string mult1 = ""; // �����_�ȏ�̐�
			for ( i = 0; i < this[row].PointPosition; i++ ) 
			{
				if ( mult1 != "" || this[row][i] != "0" ) 
				{
					mult1 += this[row][i];
				}
			}
			if ( mult1 == "" )
			{
				mult1 = "0";
			}
			string mult2 = ""; // �����_�ȉ��̐�
			for ( i = this[row].PointPosition; i < MaxCount(); i++ ) 
			{
				if ( i >= 0 )
				{
					mult2 += this[row][i];
				}
				else
				{
					mult2 += "0";
				}
			}
			System.Diagnostics.Debug.WriteLine("make_mult row=" + row.ToString() + " pos=" + this[row].PointPosition.ToString());
			System.Diagnostics.Debug.WriteLine("mult1=" + mult1);
			System.Diagnostics.Debug.WriteLine("mult2=" + mult2);
			// �����_�ȉ��̌���0���폜����
			for ( i = mult2.Length - 1; i >= 0; i-- )
			{
				if ( mult2[i] == '0' )
				{
					mult2 = mult2.Substring(0, mult2.Length - 1);
				}
				else
				{
					break;
				}
			}
			if ( mult2 == "" )
			{
				// �����_�ȉ���0
				return mult1;
			}
			else
			{
				// �����_�ȉ���0�ł͂Ȃ�
				return mult1 + "." + mult2;
			}
		}

		private void rem0(int row)
		{
			int cmax = MaxCount() - 1;
			int i;
			for ( i = 0; i < cmax; i++ ) 
			{
				if ( this[row][i] == "" ) 
				{
				}
				else if ( this[row][i] == "0" ) 
				{
					this[row][i] = "";
				}
				else
				{
					break;
				}
			}
			if ( this[row][cmax] == "" ) 
			{
				this[row][cmax] = "0";
			}
		}

		// �e���̔z����R�s�[����
		public MDArray array_copystring()
		{
			MDArray narray = new MDArray(settings);
			int cmax = MaxCount() - 1;
			for ( int row = 0; row < array.Count; row++ ) 
			{
				MDSubArray nsubarray = new MDSubArray();
				narray[row] = nsubarray;
				for ( int col = 0; col <= cmax; col++ ) 
				{
					narray[row][col] = this[row][col];
				}
				narray.rem0(row);
				narray[row].PointPosition = this[row].PointPosition;
			}
			narray.starting_position = starting_position;
			return narray;
		}

		// 0 �łȂ��ŏ��̌��̃C���f�b�N�X
		public int topindex(int row)
		{
			int cmax = MaxCount() - 1;
			int i;
			for ( i = 0; i <= cmax; i++ ) 
			{
				if ( this[row][i] == "" ) 
				{
				}
				else if ( this[row][i] == "0" ) 
				{
				}
				else
				{
					break;
				}
			}
			return i;
		}

		// �S�̂����ɃV�t�g
		public int lshift_high(int row, int n)
		{
			int cmax = MaxCount() - 1;
			int ret = getnum(this[row][0]);
			int i;
			for ( i = 0; i < cmax; i++ ) 
			{
				this[row][i] = this[row][i + 1];
			}
			this[row][cmax] = Convert.ToString(n);
			return ret;
		}

		// �S�̂��E�ɃV�t�g
		public int rshift_high(int row)
		{
			// �܂��A�E��1���ǉ����Ă���
			this[row][this[row].Count] = "";
			// �E�V�t�g�����s
			int cmax = MaxCount() - 1;
			int ret = getnum(this[row][cmax]);
			int i;
			for ( i = cmax; i > 0; i-- ) 
			{
				this[row][i] = this[row][i - 1];
			}
			this[row][0] = "";
			return ret;
		}

		// �����Z
		public int array_add(int src, int dst)
		{
			int cmax = MaxCount() - 1;
			int ret = 0;
			int i;
			for ( i = cmax; i >= 0; i-- ) 
			{
				ret += getnum(this[dst][i]) + getnum(this[src][i]);
				if ( ret < 10 ) 
				{
					this[dst][i] = Convert.ToString(ret);
					ret = 0;
				}
				else
				{
					this[dst][i] = Convert.ToString(ret - 10);
					ret = 1;
				}
			}
			return ret;
		}

		// �C���N�������g
		public int array_inc(int dst)
		{
			int ret = 1;
			int i;
			for ( i = starting_position - 1; i >= 0; i-- ) 
			{
				ret += getnum(this[dst][i]);
				if ( ret < 10 ) 
				{
					this[dst][i] = Convert.ToString(ret);
					ret = 0;
				}
				else
				{
					this[dst][i] = Convert.ToString(ret - 10);
					ret = 1;
				}
			}
			return ret;
		}

		// �����Z
		public int array_sub(int src, int dst)
		{
			int cmax = MaxCount() - 1;
			int ret = 0;
			int i;
			for ( i = cmax; i >= 0; i-- ) 
			{
				ret += getnum(this[dst][i]) - getnum(this[src][i]);
				if ( ret >= 0 ) 
				{
					this[dst][i] = Convert.ToString(ret);
					ret = 0;
				}
				else
				{
					this[dst][i] = Convert.ToString(ret + 10);
					ret = - 1;
				}
			}
			return ret;
		}

		// �f�N�������g
		public int array_dec(int dst)
		{
			int ret = - 1;
			int i;
			for ( i = starting_position - 1; i >= 0; i-- ) 
			{
				ret += getnum(this[dst][i]);
				if ( ret >= 0 ) 
				{
					this[dst][i] = Convert.ToString(ret);
					ret = 0;
				}
				else
				{
					this[dst][i] = Convert.ToString(ret + 10);
					ret = - 1;
				}
			}
			return ret;
		}

		// �����Z�̊J�n�ʒu�̐����擾
		public int calc_start_digit(int row)
		{
			return getnum(this[row][starting_position - 1]);
		}

		// 0 ���ǂ����𒲂ׂ�
		public bool is_zero(int row)
		{
			int cmax = MaxCount() - 1;
			return topindex(row) > cmax;
		}

		public void lshift_do(int row, bool shift_point)
		{
			lshift_high(row, 0);
			if ( shift_point )
			{
				this[row].lshift_point();
			}
		}

		public void rshift_do(int row, bool shift_point)
		{
			rshift_high(row);
			if ( shift_point )
			{
				this[row].rshift_point();
			}
		}

		private int top_diff(int row1, int row2)
		{
			return topindex(row1) - topindex(row2);
		}

		private void inputd(int row, int n)
		{
			int cmax = MaxCount() - 1;
			if ( this[row][0] == "" ) 
			{
				lshift_high(row, 0);
				this[row][cmax] = Convert.ToString(n);
			}
		}

		private void input_number_cmn(int row, string number)
		{
			for ( int i = 0; i < number.Length; i++ )
			{
				if ( Char.IsDigit(number[i]) )
				{
					inputd(row, number[i] - '0');
				}
			}
		}

		public void input_number(int row, string number)
		{
			input_number_cmn(row, number);
			this[row].lshift_point(lower_size(number));
		}

		public void input_number(int row, string number, int lower)
		{
			input_number_cmn(row, number);
			this[row].lshift_point(lower);
		}

		// �����_�ȉ��̌���
		public static int lower_size(string number)
		{
			if ( number.IndexOf(".") >= 0 )
			{
				return number.Length - number.IndexOf(".") - 1;
			}
			else
			{
				return 0;
			}
		}

		// �����_�ȉ��̍ŏ���0�̌�
		public static int lower_mag(string number)
		{
			if ( number.IndexOf(".") >= 0 )
			{
				int n = 0;
				for ( int i = number.IndexOf(".") + 1; i < number.Length; i++ )
				{
					if ( number[i] == '0' )
					{
						n++;
					}
					else
					{
						break;
					}
				}
				return n;
			}
			else
			{
				return 0;
			}
		}

		// �����_�Ɛ擪�A������0������������
		public static int no_point_length(string number)
		{
			// �擪��0����菜��
			int n = 0;
			for ( int i = 0; i < number.Length; i++ )
			{
				n = i;
				if ( number[i] != '0' )
				{
					break;
				}
			}
			number = number.Substring(n);
			for ( int i = number.Length - 1; i >= 0; i-- )
			{
				n = i + 1;
				if ( number[i] != '0' )
				{
					break;
				}
			}
			number = number.Substring(0, n);
			if ( number.IndexOf(".") >= 0 )
			{
				System.Diagnostics.Debug.WriteLine("no_point_length(" + number + ")=" + Convert.ToString(number.Length - 1));
				return number.Length - 1;
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("no_point_length(" + number + ")=" + Convert.ToString(number.Length));
				return number.Length;
			}
		}

		// �����_�������āA�擪��0�Ɩ�����0�������������̌�
		public static int number_size(string number)
		{
			string s = "";
			for ( int i = 0; i < number.Length; i++ )
			{
				if ( number[i] != '.' )
				{
					s += number[i];
				}
			}
			return no_point_length(s);
		}

		public void clearall(int row)
		{
			int cmax = MaxCount() - 1;
			for ( int i = 0; i < cmax; i++ ) 
			{
				this[row][i] = "";
			}
			this[row][cmax] = "0";
		}

		public void lshift(int row)
		{
			lshift_do(row, true);
		}

		public void rshift(int row)
		{
			rshift_do(row, true);
		}

		public void add(int cnt, int src, int dst)
		{
			if ( array_dec(cnt) == 0 ) 
			{
				array_add(src, dst);
			}
		}

		public void sub(int cnt, int src, int dst)
		{
			if ( array_inc(cnt) == 0 ) 
			{
				if ( array_sub(src, dst) == 0 ) 
				{
				}
			}
		}

		// �����Z���ł��邩�H
		public bool can_sub(int src, int dst)
		{
			MDArray narray = this.array_copystring();
			return narray.array_sub(src, dst) == 0;
		}

		// shiftrow ���ŏ�ʂ� torow �Ɠ����ɂȂ�悤�ɃV�t�g
		public void lshift_to_top(int lshiftrow, int torow, int rshiftrow)
		{
			int n = top_diff(lshiftrow, torow);
			if ( n > 0 )
			{
				// ���V�t�g
				for ( int i = 0; i < n; i++ )
				{
					lshift(lshiftrow);
					//rshift(rshiftrow);
					lshift_starting_position();
				}
			}
			else if ( n < 0 )
			{
				// �E�V�t�g
				for ( int i = 0; i > n; i-- )
				{
					rshift(lshiftrow);
					//lshift(rshiftrow);
					rshift_starting_position();
				}
			}
		}

		// �����Z�̊J�n�ʒu�����ɃV�t�g
		public void lshift_starting_position()
		{
			starting_position--;
			if ( starting_position < 0 )
			{
				// �G���[�B�p���s�B
				starting_position = 0;
			}
		}

		// �����Z�̊J�n�ʒu���E�ɃV�t�g
		public void rshift_starting_position()
		{
			starting_position++;
		}

		// �]����0���������A�K�v��0�͕₤
		private void normalize()
		{
			for ( int row = 0; row < array.Count; row++ ) 
			{
				this[row].normalize();
			}
		}

		public void paint(XGraphics g)
		{
			normalize();
			int cmax = MaxCount() - 1;
			for ( int row = 0; row < array.Count; row++ ) 
			{
				for ( int col = 0; col <= cmax; col++ ) 
				{
					calc.paint(g, this[row][col], col, row);
				}
				if ( !this[row].is_integer() )
				{
					calc.paint(g, ".", this[row].PointPosition, row);
				}
			}
		}
	}
}
