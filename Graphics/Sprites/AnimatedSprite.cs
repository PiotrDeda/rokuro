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

	internal override IntPtr GetClip() =>
		Texture.Clips[CurrentFrame++ / Texture.Delay % Texture.FrameCount + State * Texture.FrameCount];
}
