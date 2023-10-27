namespace Rokuro.Graphics;

public class AnimatedSprite : ISprite
{
	int _state;
	
	public AnimatedSprite(SpriteTemplate template)
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

	public int Width() => Template.Width;
	public int Height() => Template.Height;
	public IntPtr Texture() => Template.Texture;

	public IntPtr Clip() => Template.Clips[CurrentFrame++ / Template.Delay % Template.FrameCount
										   + State * Template.FrameCount];
}
