using Rokuro.Core;
using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Graphics;

public class TextSprite : ISprite
{
	IntPtr Texture { get; set; }
	int Width { get; set; }
	int Height { get; set; }

	int ISprite.GetWidth() => Width;
	int ISprite.GetHeight() => Height;
	public (IntPtr texture, IntPtr clip) GetRenderData() => (Texture, IntPtr.Zero);

	internal void RefreshTexture(string text, Font font, Color color, IntPtr renderer)
	{
		IntPtr surface = SDL_ttf.TTF_RenderText_Blended_Wrapped(font.Get(), text, color, 2000);
		if (surface == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create surface from text: {text}", ErrorSource.TTF);
		Texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
		if (Texture == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create texture from text surface: {text}", ErrorSource.SDL);
		SDL.SDL_QueryTexture(Texture, out _, out _, out int width, out int height);
		Width = width;
		Height = height;
	}
}
