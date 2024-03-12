using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class Texture
{
	IntPtr _rawTexture;

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
				IntPtr obj = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_Rect)));
				Marshal.StructureToPtr(SDLExt.Rect(j * Width, i * Height, Width, Height), obj, false);
				Clips[i * frameCount + j] = obj;
			}
		}
	}

	internal Texture(IntPtr rawTexture) : this(rawTexture, 1, 1, 30) {}

	internal Texture() {}

	internal int Width { get; private set; }
	internal int Height { get; private set; }
	internal int FrameCount { get; } = 1;
	internal int StateCount { get; } = 1;
	internal int Delay { get; } = 30;

	internal IntPtr RawTexture
	{
		get => _rawTexture;
		set
		{
			SDL.SDL_DestroyTexture(_rawTexture);
			_rawTexture = value;
			SDL.SDL_QueryTexture(_rawTexture, out _, out _, out int width, out int height);
			Width = width / FrameCount;
			Height = height / StateCount;
		}
	}

	internal IntPtr[] Clips { get; } = new IntPtr[0];
}
