using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class TextObject : GameObject
{
	public TextObject()
	{
		base.Sprite = new TextSprite();
	}

	public string Text
	{
		get;
		set
		{
			field = value;
			Sprite?.RefreshRawTexture(field, Font, FontSize, Color);
		}
	} = "";

	public Color Color
	{
		get;
		set
		{
			field = value;
			Sprite?.RefreshRawTexture(Text, Font, FontSize, field);
		}
	}

	public Font Font
	{
		get;
		set
		{
			field = value;
			Sprite?.RefreshRawTexture(Text, field, FontSize, Color);
		}
	} = SpriteManager.DefaultFont;

	public int FontSize
	{
		get;
		set
		{
			field = value;
			Sprite?.RefreshRawTexture(Text, Font, field, Color);
		}
	} = 20;

	public new TextSprite? Sprite => (TextSprite?)base.Sprite;
}
