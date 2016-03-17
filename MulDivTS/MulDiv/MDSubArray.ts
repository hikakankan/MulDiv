class MDSubArray
{
    private array: Array<number>;
    private point_position: number;

    public constructor() {
        this.array = new Array<number>();
        this.point_position = 0; // 先頭から何個目に小数点があるかを表す
    }

    public get = function (index: number): number
	{
		for ( var i = this.array.length; i <= index; i++ )
		{
			this.array.push("");
		}
		return this.array[index];
	}

    public set = function (index: number, value: number): void
	{
		for ( var i = this.array.length; i <= index; i++ )
		{
			this.array.push("");
		}
		this.array[index] = value;
	}

    public getCount = function (): number
	{
		return this.array.length;
	}

    public getPointPosition = function (): number
	{
		return this.point_position;
	}

    public setPointPosition = function (value: number): void
	{
		this.point_position = value;
	}

//	this.lshift_point = function()
//	{
//		this.point_position--;
//	}

//	this.lshift_point = function(n)
//	{
//		this.point_position -= n;
//	}

    public lshift_point_n = function (n: number): void
	{
		this.point_position -= n;
	}

    public rshift_point = function(): void
	{
		this.point_position++;
	}

    public is_zero = function (index: number): boolean
	{
		return this.get(index) == "" || this.get(index) == "0";
	}

    public are_zeroes = function (index1: number, index2: number): boolean
	{
		for ( var i = index1; i < index2; i++ )
		{
			if ( !this.is_zero(i) )
			{
				return false;
			}
		}
		return true;
	}

	// 小数点以下が全部0
    public is_integer = function(): boolean
	{
		return this.are_zeroes(this.getPointPosition(), this.getCount());
	}

	// 余分な0を消去し、必要な0は補う
    public normalize = function(): void
	{
		for ( var i = 0; i < this.getCount(); i++ )
		{
			if ( this.is_zero(i) )
			{
				if ( i < this.getPointPosition() )
				{
					// 小数点より上
					if ( this.are_zeroes(0, i) && (i != this.getPointPosition() - 1) )
					{
						// 上位が全部0
						this.set(i, "");
					}
					else
					{
						this.set(i, "0");
					}
				}
				else
				{
					// 小数点より下
					if ( this.are_zeroes(i, this.getCount()) )
					{
						// 下位が全部0
						this.set(i, "");
					}
					else
					{
						this.set(i, "0");
					}
				}
			}
		}
	}
}
