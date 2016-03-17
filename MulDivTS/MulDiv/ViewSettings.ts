class ViewSettings {
    public BodyBackColor: WYColor;
    public BodyTextColor: WYColor;
    public ButtonBackColor: WYColor;
    public ButtonTextColor: WYColor;
    public TextBackColor: WYColor;
    public TextTextColor: WYColor;
    public CalcAreaBackColor: WYColor;
    //public CalcAreaFrameBackColor: WYColor;
    //public CalcAreaFrameSelectedBackColor: WYColor;
    //public CalcAreaFrameColor: WYColor;
    public CalcAreaTextColor: WYColor;
    public MainFont: GCDefaultFont;
    public CalcAreaFont: GCDefaultFont;
    public CalcAreaHorizontalMagnitude: number;
    public CalcAreaVerticalMagnitude: number;
    //public AsciiMode: boolean;
    public ImageSettings: ImageSettings;
    public UseImage: boolean;

    public constructor() {
        // 制御領域
        this.BodyBackColor = new WYColor(220, 240, 255);	// 背景色
        this.BodyTextColor = new WYColor(0, 0, 0);		// テキストの色
        this.ButtonBackColor = new WYColor(200, 220, 255);	// ボタンの色
        this.ButtonTextColor = new WYColor(0, 0, 0);		// ボタンのテキストの色
        this.TextBackColor = new WYColor(255, 245, 245);	// テキストボックスの色
        this.TextTextColor = new WYColor(0, 0, 0);		// テキストボックスのテキストの色

        // 計算領域
        this.CalcAreaBackColor = new WYColor(255, 245, 245);	// 式の領域の背景色(計算領域の背景色)
        this.CalcAreaTextColor = new WYColor(0, 0, 0);		// 式の文字の色(計算領域の文字の色)

        // フォント
        this.MainFont = new GCDefaultFont();			// フォーム(メインウィンドウのフォント)
        this.CalcAreaFont = new GCFont("32px 'Times New Roman'", 32);		// 計算領域のフォント

        this.CalcAreaHorizontalMagnitude = 1.0;			// 計算領域の文字の横の倍率
        this.CalcAreaVerticalMagnitude = 1.0;			// 計算領域の文字の縦の倍率

        this.ImageSettings = null;				// イメージの設定
        this.UseImage = false;					// イメージを使うかどうか
    }
}

class ImageSettings {
    public GetWidth(str: string): number {
        return 0; // 実装していません
    }
    public GetHeight(): number {
        return 0; // 実装していません
    }
    public DrawString(str: string, x: number, y: number, graph: CanvasRenderingContext2D): void {
        // 実装していません
    }
}
