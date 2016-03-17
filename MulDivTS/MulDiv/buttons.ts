function is_num(c: string): boolean
{
	return c >= '0' && c <= '9' || c == '.';
}

function is_op(c: string): boolean
{
	return c == '+' || c == '-' || c == '*' || c == '/';
}

function last_is_point_num(text: string): boolean
{
	for ( var i = text.length - 1; i >= 0; i-- ) 
	{
		if ( !is_num(text[i]) )
		{
			return false;
		}
		else if ( text[i] == '.' )
		{
			return true;
		}
	}
	return false;
}

function isspace(c: string): boolean
{
	return c == " " || c == "\t";
}

function trim(str: string): string
{
	var start = -1;
	for ( var i = 0; i < str.length; i++ ) {
		start = i;
		var c = str.charAt(i);
		if ( !isspace(c) ) {
			break;
		}
	}
	if ( start == -1 ) {
		return "";
	}
	var end = -1;
	for ( var i = str.length - 1; i >= 0; i-- ) {
		end = i;
		var c = str.charAt(i);
		if ( !isspace(c) ) {
			break;
		}
	}
	return str.substring(start, end - start + 1);
}

var inputText: WYCanvasTextBox;
var equationText: WYCanvasTextBox;

function make_button(id: string, settings: ViewSettings, left: number, top: number, width: number, height: number): WYRoundButton
{
	var canvas = <HTMLCanvasElement>document.getElementById(id);
	var gc = canvas.getContext("2d");
	var button = new WYRoundButton(gc, settings, 12);
	button.setRect(left, top, width, height);
	return button;
}

class buttonBlock
{
    public width: number;
    private height: number;

    public constructor(id: string, settings: ViewSettings, rows: number, cols: number, start_left: number, start_top: number, width: number, height: number, space_width: number, space_height: number, buttons: Buttons) {
        var max_left = 0;
        var top = start_top;
        for (var r = 0; r < rows; r++) {
            var left = start_left;
            for (var c = 0; c < cols; c++) {
                var button = make_button(id, settings, left, top, width, height);
                buttons.add(button);
                left += width + space_width;
                max_left = Math.max(max_left, left);
            }
            top += height + space_height;
        }
        this.width = max_left - start_left - space_width;
        this.height = top - start_top - space_height;
    }
}

class Buttons
{
    private buttons: Array<WYRoundButton>;

    public constructor() {
        this.buttons = new Array<WYRoundButton>();
    }

    public add = function (button: WYRoundButton): void
	{
		this.buttons.push(button);
	}

    public setText = function (text_list: string[]): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			this.buttons[i].setText(text_list[i]);
		}
	}

    public setOnClick = function (text: string, func: ()=>void): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			if ( this.buttons[i].text == text ) {
				this.buttons[i].onclick = func;
			}
		}
	}

    public draw = function (): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			this.buttons[i].draw();
		}
	}

    public mousePressed = function (x: number, y: number): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			this.buttons[i].mousedown(x, y);
		}
	}

    public mouseReleased = function (x: number, y: number): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			this.buttons[i].mouseup(x, y);
		}
	}

    public mouseMoved = function (x: number, y: number): void
	{
	}

    public touchStart = function (x: number, y: number): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			this.buttons[i].touchstart(x, y);
		}
	}

    public touchEnd = function (ids): void
	{
		for ( var i = 0; i < this.buttons.length; i++ ) {
			this.buttons[i].touchend();
		}
	}

    public touchMove = function (x: number, y: number): void
	{
	}
}

function make_buttons(id: string, settings: ViewSettings): Buttons
{
	var w = 40;
	var h = 30;
	var ws = 6;
	var hs = 6;
	var x = 8;
	var y = 8;
	var wss = 12;
	var buttons = new Buttons();
	var block1 = new buttonBlock(id, settings, 4, 3, x, y, w, h, ws, hs, buttons);
	x += block1.width + wss;
	var block2 = new buttonBlock(id, settings, 4, 1, x, y, 45, h, ws, hs, buttons);
	x += block2.width + wss;
	var block3 = new buttonBlock(id, settings, 2, 1, x, y + (h + hs) * 2, 60, h, ws, hs, buttons);
	buttons.setText(new Array("7", "8", "9", "4", "5", "6", "1", "2", "3", "0", ".", "=", "BS", "CLR", "×", "÷", "× Next", "÷ Next"));
	return buttons;
}

function init_input(settings: ViewSettings): void
{
	inputText = createTextBox("input-canvas", settings, "");
	equationText = createTextBox("equation-canvas", settings, "");

	var buttons = make_buttons("buttons", settings);
	var buttons_canvas = <HTMLCanvasElement>document.getElementById("buttons");
	buttons.draw();
	buttons.setOnClick("0", buttonN0_Click);
	buttons.setOnClick("1", buttonN1_Click);
	buttons.setOnClick("2", buttonN2_Click);
	buttons.setOnClick("3", buttonN3_Click);
	buttons.setOnClick("4", buttonN4_Click);
	buttons.setOnClick("5", buttonN5_Click);
	buttons.setOnClick("6", buttonN6_Click);
	buttons.setOnClick("7", buttonN7_Click);
	buttons.setOnClick("8", buttonN8_Click);
	buttons.setOnClick("9", buttonN9_Click);
	buttons.setOnClick(".", buttonDot_Click);
	buttons.setOnClick("×", buttonMult_Click);
	buttons.setOnClick("÷", buttonDiv_Click);
	buttons.setOnClick("× Next", buttonMultNext_Click);
	buttons.setOnClick("÷ Next", buttonDivNext_Click);
	buttons.setOnClick("=", buttonEqual_Click);
	buttons.setOnClick("BS", buttonBS_Click);
	buttons.setOnClick("CLR", buttonClear_Click);

	def_mouse_event(buttons_canvas, buttons);
}
