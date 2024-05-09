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

	public virtual void DrawSprite(Sprite sprite, Vector2I position, float scale)
	{
		IntPtr rawTexture = sprite.Texture.RawTexture;
		if (rawTexture == IntPtr.Zero)
			return;
		SDL.SDL_Rect rect = new() {
			x = position.X, y = position.Y,
			w = (int)(sprite.Texture.Width * scale * sprite.ScaleX),
			h = (int)(sprite.Texture.Height * scale * sprite.ScaleY)
		};
		if (sprite.FlipX || sprite.FlipY || sprite.Rotation != 0)
		{
			var flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
			if (sprite.FlipX)
				flip |= SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
			if (sprite.FlipY)
				flip |= SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
			if (sprite.Origin is null)
			{
				SDL.SDL_RenderCopyEx(Renderer, rawTexture, sprite.GetClip(), ref rect, sprite.Rotation, IntPtr.Zero, flip);
			}
			else
			{
				SDL.SDL_Point origin = new() { x = sprite.Origin.X, y = sprite.Origin.Y };
				SDL.SDL_RenderCopyEx(Renderer, rawTexture, sprite.GetClip(), ref rect, sprite.Rotation, ref origin, flip);
			}
		}
		else
		{
			SDL.SDL_RenderCopy(Renderer, rawTexture, sprite.GetClip(), ref rect);
		}
	}

	public virtual void DrawLine(Vector2I start, Vector2I end, Color color, int thickness)
	{
		SDL.SDL_SetRenderDrawColor(Renderer, color.R, color.G, color.B, color.A);
		for (int i = -thickness / 2; i < thickness - thickness / 2; i++)
			SDL.SDL_RenderDrawLine(Renderer, start.X, start.Y + i, end.X, end.Y + i);
	}

	internal virtual IntPtr GetTextRawTexture(string text, Font font, Color color)
	{
		if (font.Get() == IntPtr.Zero)
			return IntPtr.Zero;
		IntPtr surface = SDL_ttf.TTF_RenderText_Blended_Wrapped(font.Get(), text, color, 2000);
		if (surface == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create surface from text: {text}", ErrorSource.TTF);
		IntPtr rawTexture = SDL.SDL_CreateTextureFromSurface(Renderer, surface);
		if (rawTexture == IntPtr.Zero)
			Logger.ThrowSDLError($"Failed to create texture from text surface: {text}", ErrorSource.SDL);
		return rawTexture;
	}

	internal virtual void RenderStart()
	{
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderClear(Renderer);

		SDL.SDL_SetRenderDrawColor(Renderer, BgColor.R, BgColor.G, BgColor.B, BgColor.A);
		SDL.SDL_Rect rect = new() { x = 0, y = 0, w = BaseWidth, h = BaseHeight };
		SDL.SDL_RenderFillRect(Renderer, ref rect);
	}

	internal virtual void RenderComplete()
	{
		SDL.SDL_RenderPresent(Renderer);
	}
}
