function WYCheckBox(canvas, settings, text)
{
	this.canvas = canvas;
	this.settings = settings;
	this.foreShadowWidth = 2;
	this.backShadowWidth = 3;
	this.shadowRate = 0.6;
	this.isDown = false;
	this.cornerRadius = 12; // 角の半径
	this.gradientRate = 0.4;
	this.text = text;
	this.value = false;
	this.rect = new WYRect(0, 0, this.canvas.width, this.canvas.height);

	this.setText = function(text)
	{
		this.text = text;
	}

	this.getValue = function()
	{
		return this.value;
	}

	this.setValue = function(value)
	{
		this.value = value;
		this.isDown = value;
		this.draw();
	}

	this.setDown = function(value)
	{
		this.setValue(value);
	}

	this.setRect = function(left, top, width, height)
	{
		this.rect = new WYRect(left, top, width, height);
	}

	this.contains = function(x, y)
	{
		return this.rect.contains(x, y);
	}

	this.draw = function()
	{
		var w1 = this.backShadowWidth;
		var w2 = this.foreShadowWidth;
		var gc = this.canvas.getContext("2d");
		var g = new WYGraphics(gc, this.settings.MainFont, this.settings.UseImage, this.settings.ImageSettings);
		if ( this.isDown ) {
			g.drawRoundBox(this.rect, w1, w2, this.settings.ButtonBackColor, this.isDown, this.shadowRate, this.gradientRate, this.cornerRadius);
			g.drawButtonText(this.rect, w1, w2, this.settings.ButtonTextColor, this.text);
		} else {
			g.drawRoundBox(this.rect, w2, w1, this.settings.ButtonBackColor, this.isDown, this.shadowRate, this.gradientRate, this.cornerRadius);
			g.drawButtonText(this.rect, w2, w1, this.settings.ButtonTextColor, this.text);
		}
	}

	this.onclick = function()
	{
		this.setValue(!this.value);
		if ( this.oncheck != null ) {
			this.oncheck();
		}
	}

	this.mousePressed = function(x, y)
	{
		if ( this.contains(x, y) ) {
			//this.setDown(true);
			if ( this.onclick != null ) {
				this.onclick();
			}
		}
	}

	this.mouseReleased = function(x, y)
	{
		if ( this.isDown ) {
			//this.setDown(false);
		}
	}

	this.mouseMoved = function(x, y)
	{
	}

	this.touchStart = function(x, y)
	{
		if ( this.contains(x, y) ) {
			this.setDown(true);
			if ( this.onclick != null ) {
				this.onclick();
			}
		}
	}

	this.touchEnd = function()
	{
		if ( this.isDown ) {
			this.setDown(false);
		}
	}

	this.touchMove = function(x, y)
	{
	}
}
