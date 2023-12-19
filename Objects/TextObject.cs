using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class TextObject : GameObject
{
	Color _color;
	Font _font;
	string _text;

	public TextObject(Vector2D position, Camera camera, string text, Color color, Font font)
		: base(position, new TextSprite(), camera)
	{
		_text = text;
		_color = color;
		_font = font;
		Sprite?.RefreshTexture(Text, Font, Color, Camera?.Drawer.Renderer);
	}

	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			Sprite?.RefreshTexture(_text, Font, Color, Camera?.Drawer.Renderer);
		}
	}

	public Color Color
	{
		get => _color;
		set
		{
			_color = value;
			Sprite?.RefreshTexture(Text, Font, _color, Camera?.Drawer.Renderer);
		}
	}

	public Font Font
	{
		get => _font;
		set
		{
			_font = value;
			Sprite?.RefreshTexture(Text, _font, Color, Camera?.Drawer.Renderer);
		}
	}

	public new TextSprite? Sprite => (TextSprite?)base.Sprite;
}
