using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class SpriteTemplate
{
	public SpriteTemplate(Texture texture, int stateCount, int frameCount, int delay)
	{
		Texture = texture.Get();
		SDL.SDL_QueryTexture(Texture, out _, out _, out int width, out int height);
		Width = width / frameCount;
		Height = height / stateCount;

		FrameCount = frameCount;
		Delay = delay;

		Clips = new IntPtr[frameCount * stateCount];
		for (int i = 0; i < stateCount; i++)
		{
			for (int j = 0; j < frameCount; j++)
			{
				IntPtr obj = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_Rect)));
				Marshal.StructureToPtr(SDLExt.Rect(j * Width, i * Height, Width, Height), obj, false);
				Clips[i * frameCount + j] = obj;
			}
		}
	}

	public SpriteTemplate(Texture texture) : this(texture, 1, 1, 30) {}

	internal SpriteTemplate() {}

	internal int Width { get; }
	internal int Height { get; }
	internal int FrameCount { get; }
	internal int Delay { get; }
	internal IntPtr Texture { get; }
	internal IntPtr[] Clips { get; } = new IntPtr[0];
}
