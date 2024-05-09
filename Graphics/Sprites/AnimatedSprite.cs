using Rokuro.Core;

namespace Rokuro.Graphics;

public class AnimatedSprite : Sprite
{
	int _state;

	public AnimatedSprite(Texture texture) : base(texture) {}

	public int State
	{
		get => _state;
		set
		{
			if (value < 0)
				_state = 0;
			else if (value >= Texture.StateCount)
				_state = Texture.StateCount - 1;
			else
				_state = value;
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
