namespace Rokuro.Graphics;

public abstract class Sprite
{
	public Sprite(Texture texture)
	{
		Texture = texture;
	}

	public int Width => Texture.Width;
	public int Height => Texture.Height;

	internal Texture Texture { get; }

	internal abstract IntPtr GetClip();
}
