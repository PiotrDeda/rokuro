using System.Runtime.InteropServices;
using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class AnimatedSpriteTemplate : StaticSpriteTemplate
{
	public AnimatedSpriteTemplate(Texture texture, int frameCount, int delay, int stateCount = 1)
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

	internal int FrameCount { get; }
	internal int Delay { get; }
}
