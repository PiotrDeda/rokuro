using Rokuro.Core;
using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Graphics;

public class Drawer
{
	internal Drawer(IntPtr renderer, int baseWidth, int baseHeight, Color bgColor)
	{
		Renderer = renderer;
		BaseWidth = baseWidth;
		BaseHeight = baseHeight;
		BgColor = bgColor;
	}

	public int BaseWidth { get; internal set; }
	public int BaseHeight { get; internal set; }
	public float WidthMultiplier { get; internal set; } = 1;
	public float HeightMultiplier { get; internal set; } = 1;
	public int WidthOffset { get; internal set; }
	public int HeightOffset { get; internal set; }

	internal IntPtr Renderer { get; }

	Color BgColor { get; }

	public void Draw(ISprite sprite, Vector2D position, float scale)
	{
		(IntPtr texture, IntPtr clip) = sprite.GetRenderData();
		if (texture != IntPtr.Zero)
		{
			SDL.SDL_Rect rect = SDLExt.Rect(position.X, position.Y,
				(int)(sprite.GetWidth() * scale), (int)(sprite.GetHeight() * scale));
			SDL.SDL_RenderCopy(Renderer, texture, clip, ref rect);
		}
	}

	internal void RenderStart()
	{
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderClear(Renderer);

		SDL.SDL_SetRenderDrawColor(Renderer, BgColor.R, BgColor.G, BgColor.B, BgColor.A);
		SDL.SDL_Rect rect = SDLExt.Rect(0, 0, BaseWidth, BaseHeight);
		SDL.SDL_RenderFillRect(Renderer, ref rect);
	}

	internal void RenderComplete()
	{
		SDL.SDL_RenderPresent(Renderer);
	}
}
