using Rokuro.Core;
using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Graphics;

public class TextSprite : ISprite
{
	Color _color = new(255, 255, 255);
	string _text = "";

	public TextSprite(string initialText)
	{
		Text = initialText;
	}

	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			RefreshTexture();
		}
	}

	public Color Color
	{
		get => _color;
		set
		{
			_color = value;
			RefreshTexture();
		}
	}

	IntPtr Texture { get; set; }
	int Width { get; set; }
	int Height { get; set; }

	int ISprite.Width() => Width;
	int ISprite.Height() => Height;
	IntPtr ISprite.Texture() => Texture;
	public IntPtr Clip() => IntPtr.Zero;

	void RefreshTexture()
	{
		IntPtr surface = SDL_ttf.TTF_RenderText_Blended_Wrapped(App.SpriteManager.DefaultFont, Text, Color, 2000);
		if (surface == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create surface from text: {Text}", ErrorSource.TTF);
		Texture = SDL.SDL_CreateTextureFromSurface(App.Drawer.Renderer, surface);
		if (Texture == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create texture from text surface: {Text}", ErrorSource.SDL);
		SDL.SDL_QueryTexture(Texture, out _, out _, out int width, out int height);
		Width = width;
		Height = height;
	}
}
