namespace Rokuro.Graphics;

public class StaticSprite : ISprite
{
	int _state;

	public StaticSprite(SpriteTemplate template)
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

	SpriteTemplate Template { get; }

	public int GetWidth() => Template.Width;
	public int GetHeight() => Template.Height;

	public (IntPtr texture, IntPtr clip) GetRenderData() => (Template.Texture, Template.Clips[State]);
}
