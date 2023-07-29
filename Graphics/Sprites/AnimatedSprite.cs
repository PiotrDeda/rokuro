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
		AnimationClips = new IntPtr[frameCount];
		for (int i = 0; i < frameCount; i++)
		{
			IntPtr obj = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_Rect)));
			Marshal.StructureToPtr(SDLExt.Rect(i * Width, 0, Width, Height), obj, false);
			AnimationClips[i] = obj;
		}
	}

	internal override IntPtr Clip => AnimationClips[SDL.SDL_GetTicks64() / Delay % FrameCount];

	ulong FrameCount { get; set; }
	ulong Delay { get; set; }
	IntPtr[] AnimationClips { get; set; }
}
