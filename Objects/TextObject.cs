using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class TextObject : GameObject
{
	Color _color;
	Font _font = SpriteManager.DefaultFont;
	string _text = "";

	public TextObject()
	{
		base.Sprite = new TextSprite();
	}

	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			Sprite?.RefreshRawTexture(_text, Font, Color);
		}
	}

	public Color Color
	{
		get => _color;
		set
		{
			_color = value;
			Sprite?.RefreshRawTexture(Text, Font, _color);
		}
	}

	public Font Font
	{
		get => _font;
		set
		{
			_font = value;
			Sprite?.RefreshRawTexture(Text, _font, Color);
		}
	}

	public new TextSprite? Sprite => (TextSprite?)base.Sprite;
}
