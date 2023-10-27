namespace Rokuro.Graphics;

public class PlayableSprite : ISprite
{
	int _state;

	public PlayableSprite(SpriteTemplate template)
	{
		Template = template;
	}

	public int State
	{
		get => _state;
		set
		{
			if (value < 0)
				_state = 0;
			else if (value >= Template.Clips.Length / Template.FrameCount)
				_state = Template.Clips.Length / Template.FrameCount - 1;
			else
				_state = value;
		}
	}

	SpriteTemplate Template { get; }
	int CurrentFrame { get; set; }
	bool IsPlaying { get; set; }
	Action? Callback { get; set; }

	public int GetWidth() => Template.Width;
	public int GetHeight() => Template.Height;

	public (IntPtr texture, IntPtr clip) GetRenderData()
	{
		if (IsPlaying)
		{
			if (CurrentFrame >= Template.Delay * Template.FrameCount)
			{
				IsPlaying = false;
				Callback?.Invoke();
				return (IntPtr.Zero, IntPtr.Zero);
			}

			return (Template.Texture,
				Template.Clips[CurrentFrame++ / Template.Delay % Template.FrameCount + State * Template.FrameCount]);
		}

		return (IntPtr.Zero, IntPtr.Zero);
	}

	public void Play(Action? callback = null)
	{
		CurrentFrame = 0;
		Callback = callback;
		IsPlaying = true;
	}
}
