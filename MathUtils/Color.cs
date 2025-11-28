using SDL2;

namespace Rokuro.MathUtils;

public struct Color(byte r, byte g, byte b, byte a = 255)
{
	public static Color White => new(255, 255, 255);
	public static Color Black => new(0, 0, 0);
	public static Color Red => new(255, 0, 0);
	public static Color Green => new(0, 255, 0);
	public static Color Blue => new(0, 0, 255);
	public static Color Yellow => new(255, 255, 0);
	public static Color Cyan => new(0, 255, 255);
	public static Color Magenta => new(255, 0, 255);
	public static Color Transparent => new(0, 0, 0, 0);

	public byte R { get; set; } = r;
	public byte G { get; set; } = g;
	public byte B { get; set; } = b;
	public byte A { get; set; } = a;

	public static implicit operator SDL.SDL_Color(Color color) => new() {
		r = color.R,
		g = color.G,
		b = color.B,
		a = color.A
	};
}
