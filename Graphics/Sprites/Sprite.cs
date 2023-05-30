using SDL2;

namespace Rokuro.Graphics;

public class Sprite
{
	public Sprite(string filename)
	{
		Texture = App.LoadTexture(filename);
		SDL.SDL_QueryTexture(Texture, out _, out _, out int width, out int height);
		Width = width;
		Height = height;
	}

	internal Sprite() {}

	public int Width { get; protected set; }
	public int Height { get; protected set; }

	internal IntPtr Texture { get; set; }
}
