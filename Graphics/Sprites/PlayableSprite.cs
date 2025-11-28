using Rokuro.Core;

namespace Rokuro.Graphics;

public class PlayableSprite(Texture texture) : Sprite(texture)
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
	bool IsPlaying { get; set; }
	Action? Callback { get; set; }

	internal override IntPtr GetClip()
	{
		if (!IsPlaying)
			return SpriteManager.BlankRect;
		CurrentTime += App.DeltaTime;
		if (CurrentTime >= Texture.Delay)
		{
			CurrentTime = 0;
			CurrentFrame++;
			if (CurrentFrame >= Texture.FrameCount)
			{
				IsPlaying = false;
				Callback?.Invoke();
				return SpriteManager.BlankRect;
			}
		}
		return Texture.Clips[CurrentFrame + State * Texture.FrameCount];
	}

	public void Play(Action? callback = null)
	{
		CurrentFrame = 0;
		Callback = callback;
		IsPlaying = true;
	}
}
