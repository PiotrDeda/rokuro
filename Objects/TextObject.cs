using Rokuro.Graphics;
using Rokuro.Math;

namespace Rokuro.Objects;

public class TextObject : SimpleObject, IDrawable
{
	public TextObject(string initialText, Camera camera) : base(new TextSprite(initialText), camera)
	{
		Text = initialText;
	}

	public string Text
	{
		get => ((TextSprite) Sprite).Text;
		set => ((TextSprite) Sprite).Text = value;
	}
	
	public Color Color
	{
		get => ((TextSprite) Sprite).Color;
		set => ((TextSprite) Sprite).Color = value;
	}
}
