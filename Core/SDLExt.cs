using SDL2;

namespace Rokuro.Core;

public class SDLExt
{
	public static SDL.SDL_Rect Rect(int x, int y, int w, int h)
	{
		SDL.SDL_Rect rect = new();
		rect.x = x;
		rect.y = y;
		rect.w = w;
		rect.h = h;
		return rect;
	}
}
