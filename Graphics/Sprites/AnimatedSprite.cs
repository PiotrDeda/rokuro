using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class AnimatedSprite : Sprite
{
	public AnimatedSprite(string filename, int frameCount, int delay, int stateCount = 1)
	{
		Texture = App.LoadTexture(filename);
		SDL.SDL_QueryTexture(Texture, out _, out _, out int width, out int height);
		Width = width / frameCount;
		Height = height / stateCount;

		FrameCount = (ulong)frameCount;
		Delay = (ulong)delay;

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

	internal override IntPtr Clip => Clips[SDL.SDL_GetTicks64() / Delay % FrameCount]; // TODO: + (ulong)State

	ulong FrameCount { get; }
	ulong Delay { get; }
}
