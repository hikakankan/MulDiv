class MDCalc
{
    private settings: ViewSettings;
    private x_margin: number;
    private y_margin: number;
    private hmag: number;
    private vmag: number;

    public constructor(view_settings: ViewSettings) {
        this.settings = view_settings;

        this.x_margin = 10;
        this.y_margin = 10;
        this.hmag = 1;
        this.vmag = 1;
    }

	public paint = function(gc: CanvasRenderingContext2D, str: string, col: number, row: number): void
	{
		if ( str != "" )
		{
			if ( this.settings.UseImage )
			{
				//int char_width = settings.ImageSettings.GetWidth("0.");
				//int char_height = settings.ImageSettings.GetHeight();
				//if ( str == "." )
				//{
				//	int width = settings.ImageSettings.GetWidth(".");
				//	settings.ImageSettings.DrawString(str, col * char_width - width + x_margin, row * char_height + y_margin, g);
				//}
				//else
				//{
				//	settings.ImageSettings.DrawString(str, col * char_width + x_margin, row * char_height + y_margin, g);
				//}
			}
			else
			{
				var font = new WYFont(this.settings.CalcAreaFont, gc, false, null);
				var w = font.stringWidth("0");
				var h = font.getHeight();
				var char_width = Math.floor(w * this.settings.CalcAreaHorizontalMagnitude * this.hmag);
				var char_height = Math.floor(h * this.settings.CalcAreaVerticalMagnitude * this.vmag);
				var g = new WYGraphics(gc, this.settings.CalcAreaFont, false, null);
				g.setColor(this.settings.CalcAreaTextColor);
				if ( str == "." )
				{
					g.drawString(str, (col - 0.4) * char_width + this.x_margin, row * char_height + this.y_margin);
				}
				else
				{
					g.drawString(str, col * char_width + this.x_margin, row * char_height + this.y_margin);
				}
			}
		}
	}
}
