namespace Rokuro.Graphics;

public class StaticSprite : ISprite
{
	int _state;
	
	public StaticSprite(StaticSpriteTemplate template)
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
			else if (value >= Template.Clips.Length)
				_state = Template.Clips.Length - 1;
			else
				_state = value;
		}
	}

	StaticSpriteTemplate Template { get; }

	public int Width() => Template.Width;
	public int Height() => Template.Height;
	public IntPtr Texture() => Template.Texture;
	public IntPtr Clip() => Template.Clips[State];
}
