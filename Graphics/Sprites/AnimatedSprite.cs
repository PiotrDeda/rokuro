using Rokuro.Core;

namespace Rokuro.Graphics;

public class AnimatedSprite(Texture texture) : Sprite(texture)
{
	public int State
	{
		get;
		set
		{
			if (value < 0)
				field = 0;
			else if (value >= Texture.StateCount)
				field = Texture.StateCount - 1;
			else
				field = value;
		}
	}

	int CurrentFrame { get; set; }
	int CurrentTime { get; set; }

	internal override IntPtr GetClip()
	{
		CurrentTime += App.DeltaTime;
		if (CurrentTime >= Texture.Delay)
		{
			CurrentTime = 0;
			CurrentFrame = (CurrentFrame + 1) % Texture.FrameCount;
		}
		return Texture.Clips[CurrentFrame + State * Texture.FrameCount];
	}
}
