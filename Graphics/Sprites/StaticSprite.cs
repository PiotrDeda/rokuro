namespace Rokuro.Graphics;

public class StaticSprite(Texture texture) : Sprite(texture)
{
	public int State
	{
		get;
		set
		{
			if (value < 0)
				field = 0;
			else if (value >= Texture.Clips.Length)
				field = Texture.Clips.Length - 1;
			else
				field = value;
		}
	}

	internal override IntPtr GetClip() => Texture.Clips[State];
}
