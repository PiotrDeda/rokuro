namespace Rokuro.Graphics;

public class PlayableSprite : Sprite
{
	int _state;

	public PlayableSprite(Texture texture) : base(texture) {}

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
	bool IsPlaying { get; set; }
	Action? Callback { get; set; }

	internal override IntPtr GetClip()
	{
		if (IsPlaying)
		{
			if (CurrentFrame >= Texture.Delay * Texture.FrameCount)
			{
				IsPlaying = false;
				Callback?.Invoke();
				return SpriteManager.BlankRect;
			}
			return Texture.Clips[CurrentFrame++ / Texture.Delay % Texture.FrameCount + State * Texture.FrameCount];
		}
		return SpriteManager.BlankRect;
	}

	public void Play(Action? callback = null)
	{
		CurrentFrame = 0;
		Callback = callback;
		IsPlaying = true;
	}
}
