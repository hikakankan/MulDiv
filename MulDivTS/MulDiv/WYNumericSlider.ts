class WYNumericSlider
{
    private canvas: HTMLCanvasElement;
    private settings: ViewSettings;
    private upButton: WYSliderButton;
    private downButton: WYSliderButton;
    private numberTextBox: WYNumericSliderTextBox;

    public constructor(canvas, settings, value, min_value, max_value) {
        this.canvas = canvas;
        this.settings = settings;

        var gc = canvas.getContext("2d");
        this.upButton = new WYSliderButton(gc, settings);
        this.downButton = new WYSliderButton(gc, settings);
        this.numberTextBox = new WYNumericSliderTextBox(gc, settings);

        var bw = 30;
        this.upButton.setRect(0, 0, bw, this.canvas.height);
        this.numberTextBox.setRect(bw, 0, this.canvas.width - bw * 2, this.canvas.height);
        this.downButton.setRect(this.canvas.width - bw, 0, bw, this.canvas.height);

        this.upButton.setText("＋");
        this.downButton.setText("－");
        this.numberTextBox.setNumber(value);
        this.numberTextBox.setMinNumber(min_value);
        this.numberTextBox.setMaxNumber(max_value);

        this.upButton.numberTextBox = this.numberTextBox;	// onclick の中で使うために、numberTextBox を定義しておく
        this.downButton.numberTextBox = this.numberTextBox;	// onclick の中で使うために、numberTextBox を定義しておく

        this.upButton.onclick = function () {
            this.numberTextBox.upNumber();
        }

        this.downButton.onclick = function () {
            this.numberTextBox.downNumber();
        }
    }

	public getNumber = function(): number
	{
		return this.numberTextBox.getNumber();
	}

    public draw = function(): void
	{
		this.upButton.draw();
		this.downButton.draw();
		this.numberTextBox.draw();
	}

    public mousePressed = function (x: number, y: number): void
	{
		this.upButton.mousePressed(x, y);
		this.downButton.mousePressed(x, y);
		this.numberTextBox.mousePressed(x, y);
	}

    public mouseReleased = function (x: number, y: number): void
	{
		this.upButton.mouseReleased(x, y);
		this.downButton.mouseReleased(x, y);
		this.numberTextBox.mouseReleased(x, y);
	}

    public mouseMoved = function (x: number, y: number): void
	{
		this.numberTextBox.mouseMoved(x, y);
	}

    public touchStart = function (x: number, y: number): void
	{
		this.mousePressed(x, y);
	}

    public touchEnd = function (x: number, y: number): void
	{
		this.mouseReleased(x, y);
	}

    public touchMove = function (x: number, y: number): void
	{
		this.mouseMoved(x, y);
	}
}
