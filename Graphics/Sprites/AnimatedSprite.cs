using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class AnimatedSprite : Sprite
{
	public AnimatedSprite(string filename, int frameCount, int delay) : base(filename)
	{
		FrameCount = (ulong)frameCount;
		Delay = (ulong)delay;
		Width /= frameCount;
		Clips = new IntPtr[frameCount];
		for (int i = 0; i < frameCount; i++)
			{
				IntPtr obj = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_Rect)));
				Marshal.StructureToPtr(SDLExt.Rect(i * Width, 0, Width, Height), obj, false);
				Clips[i] = obj;
			}
	}

	internal override IntPtr Clip => Clips[SDL.SDL_GetTicks64() / Delay % FrameCount];

	protected ulong FrameCount { get; set; }
	protected ulong Delay { get; set; }
	protected IntPtr[] Clips { get; set; }
}
