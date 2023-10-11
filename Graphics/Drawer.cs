using Rokuro.Core;
using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Graphics;

public class Drawer
{
	internal Drawer(IntPtr renderer, WindowData windowData, Color bgColor)
	{
		Renderer = renderer;
		WindowData = windowData;
		BgColor = bgColor;
	}

	internal IntPtr Renderer { get; }

	WindowData WindowData { get; }
	Color BgColor { get; }

	public void Draw(ISprite sprite, Vector2D position, float scale)
	{
		SDL.SDL_Rect rect = SDLExt.Rect(position.X, position.Y,
			(int)(sprite.Width() * scale), (int)(sprite.Height() * scale));
		SDL.SDL_RenderCopy(Renderer, sprite.Texture(), sprite.Clip(), ref rect);
	}

	internal void RenderStart()
	{
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderClear(Renderer);

		SDL.SDL_SetRenderDrawColor(Renderer, BgColor.R, BgColor.G, BgColor.B, BgColor.A);
		SDL.SDL_Rect rect = SDLExt.Rect(0, 0, WindowData.BaseWidth, WindowData.BaseHeight);
		SDL.SDL_RenderFillRect(Renderer, ref rect);
	}

	internal void RenderComplete()
	{
		SDL.SDL_RenderPresent(Renderer);
	}
}
