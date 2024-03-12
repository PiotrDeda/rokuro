namespace Rokuro.Graphics;

public class StaticSprite : Sprite
{
	int _state;

	public StaticSprite(Texture texture) : base(texture) {}

	public int State
	{
		get => _state;
		set
		{
			if (value < 0)
				_state = 0;
			else if (value >= Texture.Clips.Length)
				_state = Texture.Clips.Length - 1;
			else
				_state = value;
		}
	}

	internal override IntPtr GetClip() => Texture.Clips[State];
}
