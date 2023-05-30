using Rokuro.Math;
using SDL2;

namespace Rokuro.Graphics;

public class Drawer
{
	internal Drawer() {}

	public Color BgColor { get; set; }

	internal IntPtr Renderer { get; set; }

	public void Draw(Sprite sprite, Camera camera, Vector position)
	{
		SDL.SDL_Rect rect = new();
		rect.x = (int)position.X;
		rect.y = (int)position.Y;
		rect.w = (int)(sprite.Width * camera.Scale);
		rect.h = (int)(sprite.Height * camera.Scale);
		SDL.SDL_RenderCopy(Renderer, sprite.Texture, IntPtr.Zero, ref rect);
	}

	internal void RenderStart()
	{
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderClear(Renderer);

		SDL.SDL_SetRenderDrawColor(Renderer, BgColor.R, BgColor.G, BgColor.B, BgColor.A);
		SDL.SDL_Rect rect = new();
		rect.x = 0;
		rect.y = 0;
		rect.w = App.WindowData.BaseWidth;
		rect.h = App.WindowData.BaseHeight;
		SDL.SDL_RenderFillRect(Renderer, ref rect);
	}

	internal void RenderEnd()
	{
		SDL.SDL_RenderPresent(Renderer);
	}
}
