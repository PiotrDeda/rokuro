using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class Sprite
{
	public Sprite(string filename, int stateCount = 1)
	{
		Texture = App.LoadTexture(filename);
		SDL.SDL_QueryTexture(Texture, out _, out _, out int width, out int height);
		Width = width;
		Height = height / stateCount;

		Clips = new IntPtr[stateCount];
		for (int i = 0; i < stateCount; i++)
		{
			IntPtr obj = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_Rect)));
			Marshal.StructureToPtr(SDLExt.Rect(0, i * Height, Width, Height), obj, false);
			Clips[i] = obj;
		}
	}

	internal Sprite() {}

	public int Width { get; protected set; }
	public int Height { get; protected set; }
	public int State { get; set; }

	internal IntPtr Texture { get; set; }
	internal virtual IntPtr Clip => Clips[State];

	protected IntPtr[] Clips { get; set; } = new IntPtr[0];
}
