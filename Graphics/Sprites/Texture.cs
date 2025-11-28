using System.Runtime.InteropServices;
using SDL2;

namespace Rokuro.Graphics;

public class Texture
{
	internal Texture(IntPtr rawTexture, int stateCount, int frameCount, int delay)
	{
		FrameCount = frameCount;
		StateCount = stateCount;
		RawTexture = rawTexture;
		Delay = delay;

		Clips = new IntPtr[frameCount * stateCount];
		for (int i = 0; i < stateCount; i++)
		{
			for (int j = 0; j < frameCount; j++)
			{
				IntPtr obj = Marshal.AllocHGlobal(Marshal.SizeOf<SDL.SDL_Rect>());
				Marshal.StructureToPtr(new SDL.SDL_Rect {
					x = j * Width,
					y = i * Height,
					w = Width,
					h = Height
				}, obj, false);
				Clips[i * frameCount + j] = obj;
			}
		}
	}

	internal Texture() {}

	internal int Width { get; private set; }
	internal int Height { get; private set; }
	internal int FrameCount { get; } = 1;
	internal int StateCount { get; } = 1;
	internal int Delay { get; } = 30;

	internal IntPtr RawTexture
	{
		get;
		set
		{
			SDL.SDL_DestroyTexture(field);
			field = value;
			SDL.SDL_QueryTexture(field, out _, out _, out int width, out int height);
			Width = width / FrameCount;
			Height = height / StateCount;
		}
	}

	internal IntPtr[] Clips { get; } = [];
}
