using SDL2;

namespace Rokuro.MathUtils;

public struct Color
{
	public Color(byte r, byte g, byte b, byte a)
	{
		R = r;
		G = g;
		B = b;
		A = a;
	}

	public Color(byte r, byte g, byte b)
	{
		R = r;
		G = g;
		B = b;
		A = 255;
	}

	public byte R { get; set; }
	public byte G { get; set; }
	public byte B { get; set; }
	public byte A { get; set; }

	public static implicit operator SDL.SDL_Color(Color color)
	{
		SDL.SDL_Color sdlColor = new();
		sdlColor.r = color.R;
		sdlColor.g = color.G;
		sdlColor.b = color.B;
		sdlColor.a = color.A;
		return sdlColor;
	}
}
