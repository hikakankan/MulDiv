// C# の Panel
class CSPanel
{
    private element: HTMLCanvasElement;
    private backcolor: WYColor;
    private Controls: CSControls;

    public constructor(element: HTMLCanvasElement, backcolor: WYColor) {
        this.element = element;
        this.backcolor = backcolor;
        this.Controls = new CSControls();
    }

    public getContext(): CanvasRenderingContext2D
	{
		return this.element.getContext("2d");
	}

	public getWidth(): number
	{
		return this.element.width;
	}

    public getHeight(): number
	{
		return this.element.height;
	}

    public SuspendLayout(): void
	{
	}

    public ResumeLayout(b: boolean): void
	{
	}

    public clear(): void
	{
		// 背景色で塗りつぶす
		var g = new CSGraphics(this.getContext());
		g.setColor(this.backcolor);
		g.fillRect(0, 0, this.getWidth(), this.getHeight());
	}

    public repaint(): void
	{
		this.clear();
		this.Controls.repaint();
	}

    public mousePressed(x: number, y: number): void
	{
		this.Controls.mousePressed(x, y);
	}

    public mouseReleased(x: number, y: number): void
	{
		this.Controls.mouseReleased(x, y);
	}

    public mouseMoved(x: number, y: number): void
	{
		this.Controls.mouseMoved(x, y);
	}

    public touchStart(x: number, y: number): void
	{
		this.Controls.touchStart(x, y);
	}

    public touchEnd(ids): void
	{
		this.Controls.touchEnd(ids);
	}

    public touchMove(x: number, y: number): void
	{
		this.Controls.touchMove(x, y);
	}
}

// C# の Controls
class CSControls
{
    private controls: Array<CSControl>;

    public constructor() {
        this.controls = new Array<CSControl>();
    }

    public Add(control: CSControl): void
	{
		this.controls.push(control);
	}

    public Remove(control: CSControl): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			if ( this.controls[i] == control ) {
				this.controls.splice(i,1);
			}
		}
	}

    public repaint(): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].repaint();
		}
	}

    public mousePressed(x: number, y: number): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].mousePressed(x, y);
		}
	}

    public mouseReleased(x: number, y: number): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].mouseReleased(x, y);
		}
	}

    public mouseMoved(x: number, y: number): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].mouseMoved(x, y);
		}
	}

    public touchStart(x: number, y: number): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].touchStart(x, y);
		}
	}

    public touchEnd(ids): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].touchEnd(ids);
		}
	}

    public touchMove(x: number, y: number): void
	{
		for ( var i = 0; i < this.controls.length; i++ ) {
			this.controls[i].touchMove(x, y);
		}
	}
}

interface CSControl {
    repaint(): void;
    mousePressed(x: number, y: number): void;
    mouseReleased(x: number, y: number): void;
    mouseMoved(x: number, y: number): void;
    touchStart(x: number, y: number): void;
    touchEnd(ids): void;
    touchMove(x: number, y: number): void;
}
