using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class StaticSpriteTemplate
{
	public StaticSpriteTemplate(string filename, int stateCount = 1)
	{
		Texture = App.SpriteManager.LoadTexture(filename);
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

	internal StaticSpriteTemplate() {}

	protected internal int Width { get; protected set; }
	protected internal int Height { get; protected set; }

	protected internal IntPtr Texture { get; protected set; }
	protected internal IntPtr[] Clips { get; protected set; } = new IntPtr[0];
}
