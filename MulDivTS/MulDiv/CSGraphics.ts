// C# の Graphics
class CSGraphics
{
    private graph: CanvasRenderingContext2D;

    public constructor(graph: CanvasRenderingContext2D) {
        this.graph = graph;
    }

    public setColor(color: WYColor): void
	{
		this.graph.fillStyle = color.getColor();
	}

    public drawRect(x: number, y: number, width: number, height: number): void
	{
		this.graph.strokeRect(x, y, width, height);
		//this.graph.beginPath();
		//this.graph.rect(x, y, width, height);
		//this.graph.stroke();
	}

    public fillRect(x: number, y: number, width: number, height: number): void
	{
		this.graph.fillRect(x, y, width, height);
	}

    public drawLine(x1: number, y1: number, x2: number, y2: number): void
	{
		this.graph.beginPath();
		this.graph.moveTo(x1, y1);
		this.graph.lineTo(x2, y2);
		//this.graph.closePath();
		this.graph.stroke();
	}
}

// C# の Rectangle
class CSRectangle
{
    private x: number;
    private y: number;
    private width: number;
    private height: number;

    public constructor(x: number, y: number, width: number, height: number) {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public contains(x: number, y: number): boolean
	{
		return x >= this.x && x < this.x + this.width && y >= this.y && y < this.y + this.height;
	}
}
