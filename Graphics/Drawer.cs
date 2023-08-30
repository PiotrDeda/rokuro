using Rokuro.Core;
using Rokuro.Math;
using SDL2;

namespace Rokuro.Graphics;

public class Drawer
{
	internal Drawer() {}

	internal Color BgColor { get; set; }

	internal IntPtr Renderer { get; set; }

	public void Draw(Sprite sprite, Camera camera, Vector2D position)
	{
		Vector2D screenPosition = camera.GetScreenPosition(position);
		SDL.SDL_Rect rect = SDLExt.Rect(screenPosition.X, screenPosition.Y,
			(int)(sprite.Width * camera.Scale), (int)(sprite.Height * camera.Scale));
		SDL.SDL_RenderCopy(Renderer, sprite.Texture, sprite.Clip, ref rect);
	}

	internal void RenderStart()
	{
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderClear(Renderer);

		SDL.SDL_SetRenderDrawColor(Renderer, BgColor.R, BgColor.G, BgColor.B, BgColor.A);
		SDL.SDL_Rect rect = SDLExt.Rect(0, 0, App.WindowData.BaseWidth, App.WindowData.BaseHeight);
		SDL.SDL_RenderFillRect(Renderer, ref rect);
	}

	internal void RenderComplete()
	{
		SDL.SDL_RenderPresent(Renderer);
	}
}
