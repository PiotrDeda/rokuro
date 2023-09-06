namespace Rokuro.Graphics;

public class StaticSprite : ISprite
{
	public StaticSprite(StaticSpriteTemplate template)
	{
		Template = template;
	}

	public int State { get; set; }

	StaticSpriteTemplate Template { get; }

	public int Width() => Template.Width;
	public int Height() => Template.Height;
	public IntPtr Texture() => Template.Texture;
	public IntPtr Clip() => Template.Clips[State];
}
