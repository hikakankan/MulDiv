function CharIsDigit(c: string): boolean
{
	return c >= '0' && c <= '9';
}

// 小数点以下の桁数
function lower_size(number: string): number
{
	if ( number.indexOf(".") >= 0 )
	{
		return number.length - number.indexOf(".") - 1;
	}
	else
	{
		return 0;
	}
}

// 小数点以下の最初の0の個数
function lower_mag(number: string): number
{
	if ( number.indexOf(".") >= 0 )
	{
		var n = 0;
		for ( var i = number.indexOf(".") + 1; i < number.length; i++ )
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

// 小数点と先頭、末尾の0を除いた長さ
function no_point_length(number: string): number
{
	// 先頭の0を取り除く
	var n = 0;
	for ( var i = 0; i < number.length; i++ )
	{
		n = i;
		if ( number[i] != '0' )
		{
			break;
		}
	}
	number = number.substr(n);
	for ( var i = number.length - 1; i >= 0; i-- )
	{
		n = i + 1;
		if ( number[i] != '0' )
		{
			break;
		}
	}
	number = number.substr(0, n);
	if ( number.indexOf(".") >= 0 )
	{
		return number.length - 1;
	}
	else
	{
		return number.length;
	}
}

// 小数点を除いて、先頭の0と末尾の0を除いた数字の個数
function number_size(number: string): number
{
	var s = "";
	for ( var i = 0; i < number.length; i++ )
	{
		if ( number[i] != '.' )
		{
			s += number[i];
		}
	}
	return no_point_length(s);
}

class MultiplyAndDivide
{
    private fig: HTMLCanvasElement;
    private labelInput: WYCanvasTextBox;
    private labelRel: WYCanvasTextBox;
    private settings: ViewSettings;
    private row_count: number;
    private exp0: string;
    private exp1: string;
    private InputText: string;
    private calc_mode: boolean;
    private PlusOperator: string;
    private MultiplicationOperator: string;
    private DivisionOperator: string;
    private EqualOperator: string;

    public constructor(fig, inputText, equationText, view_settings: ViewSettings) {
        this.fig = fig;
        this.labelInput = inputText;
        this.labelRel = equationText;
        this.settings = view_settings;

        this.row_count = 3;
        this.exp0 = "";
        this.exp1 = "";
        this.InputText = "";
        this.calc_mode = false; // 入力モード・計算モード。入力モードに設定

        this.PlusOperator = " ＋ ";
        this.MultiplicationOperator = " × ";
        this.DivisionOperator = " ÷ ";
        this.EqualOperator = " ＝ ";

        this.array_init();
    }

    public array_init = function (): void
    {
        this.main_mdarray = new MDArray(this.settings);
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

    public array_store = function(narray_src: MDArray): void
	{
		var narray = narray_src;
		this.store_mult1(narray);
		this.panelCalcRefresh();
	}

    public show_message = function(s: string): void
	{
		this.labelRel.setText(s);
	}

    public print_rel = function (): void
	{
		this.labelRel.setText(this.exp0 + this.EqualOperator + this.exp1);
	}

    public make_exp = function (e0: string, e1: string, e2: string): string
	{
		return e0 + this.MultiplicationOperator + e1 + this.PlusOperator + e2;
	}

    public store_mult0 = function (narray: MDArray): void
	{
		this.exp0 = this.make_exp(narray.make_mult(0), narray.make_mult(1), narray.make_mult(2));
		this.print_rel();
	}

    public store_mult1 = function (narray: MDArray): void
	{
		this.exp1 = this.make_exp(narray.make_mult(0), narray.make_mult(1), narray.make_mult(2));
		this.print_rel();
	}

    public FormatInputText = function (): void
	{
		if ( this.InputText.indexOf("*") >= 0 )
		{
			return this.InputText.replace("*", this.MultiplicationOperator);
		}
		else if ( this.InputText.indexOf("/") >= 0 )
		{
			return this.InputText.replace("/", this.DivisionOperator);
		}
		else
		{
			return this.InputText;
		}
	}

    public SetInputText = function (text: string): void
	{
		this.InputText = text;
		this.labelInput.setText(this.FormatInputText());
	}

    public ClearResult = function (): void
	{
		this.labelInput.setText(this.FormatInputText());
	}

    public SetMultResult = function (s: string): void
	{
		if ( this.InputText.indexOf("*") >= 0 )
		{
			this.labelInput.setText(this.FormatInputText() + this.EqualOperator + s);
		}
	}

    public SetDivResult = function (s: string): void
	{
		if ( this.InputText.indexOf("/") >= 0 )
		{
			this.labelInput.setText(this.FormatInputText() + this.EqualOperator + s);
		}
	}

    public calc_start = function (): void
	{
		var text = this.InputText;
		var mi = text.indexOf("*");
		var di = text.indexOf("/");
		if ( mi >= 0 )
		{
			var n1 = text.substr(0, mi);
			var n2 = text.substr(mi + 1);
			if ( n1 != "" && n2 != "" )
			{
				this.MultStart(n1, n2);
			}
		}
		else if ( di >= 0 )
		{
			var n1 = text.substr(0, di);
			var n2 = text.substr(di + 1);
			if ( n1 != "" && n2 != "" )
			{
				this.DivStart(n1, n2);
			}
		}
	}

    public MultStart = function (n1: string, n2: string): void
	{
		this.array_init();
		var size = no_point_length(n1) + no_point_length(n2) + 1;
		var narray = this.main_mdarray;
		narray.set_number_size(size);
		narray.input_number_2(0, n1);
		narray.input_number_2(1, n2);
		narray.input_number_3(2, "0", lower_size(n1) + lower_size(n2));
		this.array_store(narray);
		this.store_mult0(narray);
		this.ClearResult();
		this.calc_mode = true; // 入力モード・計算モード。計算モードに設定
	}

    public mult_next = function (): void
	{
		// かけ算を続ける
		// 計算モードのときだけ
		if ( this.calc_mode )
		{
			var narray = this.main_mdarray;
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
			this.array_store(narray);
			// かけ算は終了なので、結果を表示する
			if ( narray.is_zero(0) )
			{
				this.SetMultResult(narray.make_mult(2));
			}
		}
	}

    public DivStart = function (n1: string, n2: string): void
	{
		this.array_init();
		var size = Math.max(number_size(n1), number_size(n2));
		size += Math.max(lower_mag(n2), lower_mag(n1)) + 1;
		var narray = this.main_mdarray;
		narray.set_number_size(size);
		narray.input_number_3(0, "0", lower_size(n1) - lower_size(n2));
		narray.input_number_2(1, n2);
		narray.input_number_2(2, n1);
		narray.lshift_to_top(1, 2, 0);
		this.array_store(narray);
		this.store_mult0(narray);
		this.ClearResult();
		this.calc_mode = true; // 入力モード・計算モード。計算モードに設定
	}

    public div_next = function (): void
	{
		// わり算を続ける
		// 計算モードのときだけ
		if ( this.calc_mode )
		{
			var narray = this.main_mdarray;
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
			this.array_store(narray);
			// 割り算は終了なので、結果を表示する
			if ( narray.is_zero(2) )
			{
				this.SetDivResult(narray.make_mult(0));
			}
		}
	}

    public add_string = function (str: string): void
	{
		// 1文字入力
		// 入力モードのときだけ
		if ( !this.calc_mode )
		{
			if ( str.length == 1 && (CharIsDigit(str[0]) || str == "." || str == "*" || str == "/") )
			{
				var text = this.InputText;
				var mi = text.indexOf("*");
				var di = text.indexOf("/");
				if ( mi >= 0 )
				{
					if ( str == "*" || str == "/" )
					{
						return;
					}
					var pi = text.indexOf(".", mi + 1);
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
					var pi = text.indexOf(".", di + 1);
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
					var pi = text.indexOf(".");
					if ( pi >= 0 )
					{
						if ( str == "." )
						{
							return;
						}
					}
				}
				text += str;
				this.SetInputText(text);
			}
		}
	}

    public do_back_space = function (): void
	{
		// 1文字消去
		// 入力モードのときだけ
		if ( !this.calc_mode )
		{
			var str = this.InputText;
			str = str.Trim();
			if ( str.length > 0 ) 
			{
				str = str.substr(0, str.length - 1);
				str = str.Trim();
			}
			this.SetInputText(str);
		}
	}

    public do_clear = function (): void
	{
		// 全部消去
		this.SetInputText("");
		this.labelRel.setText("");
		this.array_init();
		this.panelCalcRefresh();
		this.calc_mode = false; // 入力モード・計算モード。入力モードに設定
	}

    public panelCalcRefresh = function (): void
	{
		var gc = this.fig.getContext("2d");
		var g = new WYGraphics(gc, this.settings.CalcAreaFont, false, null);
		g.setColor(this.settings.CalcAreaBackColor);
		g.fillRect(0, 0, this.fig.width, this.fig.height);
		this.main_mdarray.paint(gc);
	}

    public Refresh = function (): void
	{
		this.panelCalcRefresh();
	}
}

function buttonN0_Click(): void
{
	mad.add_string("0");
}

function buttonN1_Click(): void
{
	mad.add_string("1");
}

function buttonN2_Click(): void
{
	mad.add_string("2");
}

function buttonN3_Click(): void
{
	mad.add_string("3");
}

function buttonN4_Click(): void
{
	mad.add_string("4");
}

function buttonN5_Click(): void
{
	mad.add_string("5");
}

function buttonN6_Click(): void
{
	mad.add_string("6");
}

function buttonN7_Click(): void
{
	mad.add_string("7");
}

function buttonN8_Click(): void
{
	mad.add_string("8");
}

function buttonN9_Click(): void
{
	mad.add_string("9");
}

function buttonDot_Click(): void
{
	mad.add_string(".");
}

function buttonMult_Click(): void
{
	mad.add_string("*");
}

function buttonDiv_Click(): void
{
	mad.add_string("/");
}

function buttonMultNext_Click(): void
{
	mad.mult_next();
}

function buttonDivNext_Click(): void
{
	mad.div_next();
}

function buttonEqual_Click(): void
{
	mad.calc_start();
}

function buttonBS_Click(): void
{
	mad.do_back_space();
}

function buttonClear_Click(): void
{
	mad.do_clear();
}

var mad: MultiplyAndDivide;

function init_fig(settings: ViewSettings): void
{
	var fig = document.getElementById("fig");
	mad = new MultiplyAndDivide(fig, inputText, equationText, settings);
	mad.Refresh();
}

function init_all(): void
{
	var settings = new ViewSettings();
	document.body.style.background = settings.BodyBackColor.getColor();
	document.body.style.color = settings.BodyTextColor.getColor();
	init_input(settings);
	init_fig(settings);
}
