class MDArray
{
    private array: Array<MDSubArray>;
    private row_count: number;
    private calc: MDCalc;
    private starting_position: number;
    private settings: ViewSettings;

    public constructor(view_settings: ViewSettings) {
        this.array = new Array<MDSubArray>();
        this.row_count = 3;
        for (var i = 0; i < this.row_count; i++) {
            var nsubarray = new MDSubArray();
            this.array[i] = nsubarray;
        }
        this.calc = new MDCalc(view_settings);
        this.starting_position = 0; // 先頭から数えた、かけ算の開始位置。増減はこの位置で行う。

        this.settings = view_settings;
    }

    public set_number_size = function (size: number): void
	{
		for ( var i = 0; i < this.row_count; i++ )
		{
			for ( var j = 0; j < size; j++ )
			{
				this.get(i).set(j, "");
			}
			this.get(i).setPointPosition(size);
		}
		this.starting_position = size;
	}

    public get = function (index: number): MDSubArray
	{
		return this.array[index];
	}

    public set = function (index: number, value: MDSubArray): void
	{
		for ( var i = this.array.length; i <= index; i++ )
		{
			this.array.push(null);
		}
		this.array[index] = value;
	}

    public MaxCount = function(): number
	{
		var n = 0;
		for ( var i = 0; i < this.array.length; i++ )
		{
			n = Math.max(n, this.get(i).getCount());
		}
		return n;
	}

    public getnum = function(s: string): number
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

	// 各 row の表す数を返す
	// row: 何行目か
    public make_mult = function(row: number) :string
	{
		var i: number;
		var mult1: string = ""; // 小数点以上の数
		for ( i = 0; i < this.get(row).getPointPosition(); i++ ) 
		{
			if ( mult1 != "" || this.get(row).get(i) != "0" ) 
			{
				mult1 += this.get(row).get(i);
			}
		}
		if ( mult1 == "" )
		{
			mult1 = "0";
		}
		var mult2: string = ""; // 小数点以下の数
		for ( i = this.get(row).getPointPosition(); i < this.MaxCount(); i++ ) 
		{
			if ( i >= 0 )
			{
				mult2 += this.get(row).get(i);
			}
			else
			{
				mult2 += "0";
			}
		}
		// 小数点以下の後ろの0を削除する
		for ( i = mult2.length - 1; i >= 0; i-- )
		{
			if ( mult2[i] == '0' )
			{
				mult2 = mult2.substr(0, mult2.length - 1);
			}
			else
			{
				break;
			}
		}
		if ( mult2 == "" )
		{
			// 小数点以下は0
			return mult1;
		}
		else
		{
			// 小数点以下は0ではない
			return mult1 + "." + mult2;
		}
	}

    public rem0 = function(row: number): void
	{
		var cmax: number = this.MaxCount() - 1;
		var i: number;
		for ( i = 0; i < cmax; i++ ) 
		{
			if ( this.get(row).get(i) == "" ) 
			{
			}
			else if ( this.get(row).get(i) == "0" ) 
			{
				this.get(row).set(i, "");
			}
			else
			{
				break;
			}
		}
		if ( this.get(row).get(cmax) == "" ) 
		{
			this.get(row).set(cmax, "0");
		}
	}

	// 各桁の配列をコピーする
    public array_copystring = function (): MDArray
	{
		var narray = new MDArray(this.settings);
		var cmax = this.MaxCount() - 1;
		for ( var row = 0; row < this.array.length; row++ ) 
		{
			var nsubarray = new MDSubArray();
			narray.set(row, nsubarray);
			for ( var col = 0; col <= cmax; col++ ) 
			{
				narray.get(row).set(col, this.get(row).get(col));
			}
			narray.rem0(row);
			narray.get(row).setPointPosition(this.get(row).getPointPosition());
		}
		narray.starting_position = this.starting_position;
		return narray;
	}

	// 0 でない最初の桁のインデックス
    public topindex = function(row: number): number
	{
		var cmax = this.MaxCount() - 1;
		var i;
		for ( i = 0; i <= cmax; i++ ) 
		{
			if ( this.get(row).get(i) == "" ) 
			{
			}
			else if ( this.get(row).get(i) == "0" ) 
			{
			}
			else
			{
				break;
			}
		}
		return i;
	}

	// 全体を左にシフト
    public lshift_high = function (row: number, n: number): number
	{
		var cmax = this.MaxCount() - 1;
		var ret = this.getnum(this.get(row).get(0));
		var i;
		for ( i = 0; i < cmax; i++ ) 
		{
			this.get(row).set(i, this.get(row).get(i + 1));
		}
		this.get(row).set(cmax, String(n));
		return ret;
	}

	// 全体を右にシフト
    public rshift_high = function (row: number): number
	{
		// まず、右に1桁追加しておく
		this.get(row).set(this.get(row).getCount(), "");
		// 右シフトを実行
		var cmax = this.MaxCount() - 1;
		var ret = this.getnum(this.get(row).get(cmax));
		var i;
		for ( i = cmax; i > 0; i-- ) 
		{
			this.get(row).set(i, this.get(row).get(i - 1));
		}
		this.get(row).set(0, "");
		return ret;
	}

	// 足し算
    public array_add = function (src: number, dst: number): number
	{
		var cmax = this.MaxCount() - 1;
		var ret = 0;
		var i;
		for ( i = cmax; i >= 0; i-- ) 
		{
			ret += this.getnum(this.get(dst).get(i)) + this.getnum(this.get(src).get(i));
			if ( ret < 10 ) 
			{
				this.get(dst).set(i, String(ret));
				ret = 0;
			}
			else
			{
				this.get(dst).set(i, String(ret - 10));
				ret = 1;
			}
		}
		return ret;
	}

	// インクリメント
    public array_inc = function (dst: number): number
	{
		var ret = 1;
		var i;
		for ( i = this.starting_position - 1; i >= 0; i-- ) 
		{
			ret += this.getnum(this.get(dst).get(i));
			if ( ret < 10 ) 
			{
				this.get(dst).set(i, String(ret));
				ret = 0;
			}
			else
			{
				this.get(dst).set(i, String(ret - 10));
				ret = 1;
			}
		}
		return ret;
	}

	// 引き算
    public array_sub = function (src: number, dst: number): number
	{
		var cmax = this.MaxCount() - 1;
		var ret = 0;
		var i;
		for ( i = cmax; i >= 0; i-- ) 
		{
			ret += this.getnum(this.get(dst).get(i)) - this.getnum(this.get(src).get(i));
			if ( ret >= 0 ) 
			{
				this.get(dst).set(i, String(ret));
				ret = 0;
			}
			else
			{
				this.get(dst).set(i, String(ret + 10));
				ret = - 1;
			}
		}
		return ret;
	}

	// デクリメント
    public array_dec = function (dst: number): number
	{
		var ret = - 1;
		var i;
		for ( i = this.starting_position - 1; i >= 0; i-- ) 
		{
			ret += this.getnum(this.get(dst).get(i));
			if ( ret >= 0 ) 
			{
				this.get(dst).set(i, String(ret));
				ret = 0;
			}
			else
			{
				this.get(dst).set(i, String(ret + 10));
				ret = - 1;
			}
		}
		return ret;
	}

	// かけ算の開始位置の数を取得
    public calc_start_digit = function (row: number): number
	{
		return this.getnum(this.get(row).get(this.starting_position - 1));
	}

	// 0 かどうかを調べる
    public is_zero = function (row: number): boolean
	{
		var cmax = this.MaxCount() - 1;
		return this.topindex(row) > cmax;
	}

    public lshift_do = function (row: number, shift_point: boolean): void
	{
		this.lshift_high(row, 0);
		if ( shift_point )
		{
			this.get(row).lshift_point_n(1);
		}
	}

    public rshift_do = function(row: number, shift_point: boolean): void
	{
		this.rshift_high(row);
		if ( shift_point )
		{
			this.get(row).rshift_point();
		}
	}

    public top_diff = function (row1: number, row2: number): number
	{
		return this.topindex(row1) - this.topindex(row2);
	}

    public inputd = function (row: number, n: number): void
	{
		var cmax = this.MaxCount() - 1;
		if ( this.get(row).get(0) == "" ) 
		{
			this.lshift_high(row, 0);
			this.get(row).set(cmax, String(n));
		}
	}

    public input_number_cmn = function (row: number, number: string) :void
	{
		for ( var i = 0; i < number.length; i++ )
		{
			if ( CharIsDigit(number[i]) )
			{
				this.inputd(row, Number(number[i]));
			}
		}
	}

//	this.input_number = function(row, number)
//	{
//		this.input_number_cmn(row, number);
//		this.get(row).lshift_point_n(this.lower_size(number));
//	}

//	this.input_number = function(row, number, lower)
//	{
//		this.input_number_cmn(row, number);
//		this.get(row).lshift_point_n(lower);
//	}

    public input_number_2 = function (row: number, number: string): void
	{
		this.input_number_cmn(row, number);
		this.get(row).lshift_point_n(lower_size(number));
	}

    public input_number_3 = function(row: number, number :string, lower: number): void
	{
		this.input_number_cmn(row, number);
		this.get(row).lshift_point_n(lower);
	}

    public clearall = function(row: number): void
	{
		var cmax = this.MaxCount() - 1;
		for ( var i = 0; i < cmax; i++ ) 
		{
			this.get(row).set(i, "");
		}
		this.get(row).set(cmax, "0");
	}

    public lshift = function(row: number): void
	{
		this.lshift_do(row, true);
	}

    public rshift = function(row: number): void
	{
		this.rshift_do(row, true);
	}

    public add = function (cnt: number, src: number, dst: number): void
	{
		if ( this.array_dec(cnt) == 0 ) 
		{
			this.array_add(src, dst);
		}
	}

    public sub = function (cnt: number, src: number, dst: number): void
	{
		if ( this.array_inc(cnt) == 0 ) 
		{
			if ( this.array_sub(src, dst) == 0 ) 
			{
			}
		}
	}

	// 引き算ができるか？
    public can_sub = function (src: number, dst: number): boolean
	{
		var narray = this.array_copystring();
		return narray.array_sub(src, dst) == 0;
	}

	// shiftrow を最上位が torow と同じになるようにシフト
    public lshift_to_top = function (lshiftrow: number, torow: number, rshiftrow: number): void
	{
		var n = this.top_diff(lshiftrow, torow);
		if ( n > 0 )
		{
			// 左シフト
			for ( var i = 0; i < n; i++ )
			{
				this.lshift(lshiftrow);
				//rshift(rshiftrow);
				this.lshift_starting_position();
			}
		}
		else if ( n < 0 )
		{
			// 右シフト
			for ( var i = 0; i > n; i-- )
			{
				this.rshift(lshiftrow);
				//lshift(rshiftrow);
				this.rshift_starting_position();
			}
		}
	}

	// かけ算の開始位置を左にシフト
    public lshift_starting_position = function(): void
	{
		this.starting_position--;
		if ( this.starting_position < 0 )
		{
			// エラー。継続不可。
			this.starting_position = 0;
		}
	}

	// かけ算の開始位置を右にシフト
    public rshift_starting_position = function(): void
	{
		this.starting_position++;
	}

	// 余分な0を消去し、必要な0は補う
    public normalize = function(): void
	{
		for ( var row = 0; row < this.array.length; row++ ) 
		{
			this.get(row).normalize();
		}
	}

    public paint = function(g: CanvasRenderingContext2D): void
	{
		this.normalize();
		var cmax = this.MaxCount() - 1;
		for ( var row = 0; row < this.array.length; row++ ) 
		{
			for ( var col = 0; col <= cmax; col++ ) 
			{
				this.calc.paint(g, this.get(row).get(col), col, row);
			}
			if ( !this.get(row).is_integer() )
			{
				this.calc.paint(g, ".", this.get(row).getPointPosition(), row);
			}
		}
	}
}
