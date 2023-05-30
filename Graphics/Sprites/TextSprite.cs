using Rokuro.Core;
using Rokuro.Math;
using SDL2;

namespace Rokuro.Graphics;

public class TextSprite : Sprite
{
	private string _text = "";
	private Color _color = new Color(255, 255, 255);

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

	protected void RefreshTexture()
	{
		IntPtr surface = SDL_ttf.TTF_RenderText_Blended_Wrapped(App.DefaultFont, Text, Color, 2000);
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
