using Rokuro.Core;
using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Graphics;

class DrawerImpl
{
	public static DrawerImpl ActiveImpl { get; set; } = new();

	public int BaseWidth { get; internal set; }
	public int BaseHeight { get; internal set; }
	public float WidthMultiplier { get; internal set; } = 1;
	public float HeightMultiplier { get; internal set; } = 1;
	public int WidthOffset { get; internal set; }
	public int HeightOffset { get; internal set; }
	public Color BgColor { get; internal set; } = new(0, 0, 0, 255);

	IntPtr Renderer { get; } = App.Renderer;

	public virtual void Draw(ISprite sprite, Vector2D position, float scale)
	{
		(IntPtr texture, IntPtr clip) = sprite.GetRenderData();
		if (texture != IntPtr.Zero)
		{
			SDL.SDL_Rect rect = SDLExt.Rect(position.X, position.Y,
				(int)(sprite.GetWidth() * scale), (int)(sprite.GetHeight() * scale));
			SDL.SDL_RenderCopy(Renderer, texture, clip, ref rect);
		}
	}

	internal virtual (IntPtr texture, int width, int height) GetTextTexture(string text, Font font, Color color)
	{
		if (font.Get() == IntPtr.Zero)
			return (IntPtr.Zero, 0, 0);
		IntPtr surface = SDL_ttf.TTF_RenderText_Blended_Wrapped(font.Get(), text, color, 2000);
		if (surface == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create surface from text: {text}", ErrorSource.TTF);
		IntPtr texture = SDL.SDL_CreateTextureFromSurface(Renderer, surface);
		if (texture == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create texture from text surface: {text}", ErrorSource.SDL);
		SDL.SDL_QueryTexture(texture, out _, out _, out int width, out int height);
		return (texture, width, height);
	}

	internal virtual void RenderStart()
	{
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderClear(Renderer);

		SDL.SDL_SetRenderDrawColor(Renderer, BgColor.R, BgColor.G, BgColor.B, BgColor.A);
		SDL.SDL_Rect rect = SDLExt.Rect(0, 0, BaseWidth, BaseHeight);
		SDL.SDL_RenderFillRect(Renderer, ref rect);
	}

	internal virtual void RenderComplete()
	{
		SDL.SDL_RenderPresent(Renderer);
	}
}
