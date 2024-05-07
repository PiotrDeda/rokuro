using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public abstract class Sprite
{
	public Sprite(Texture texture)
	{
		Texture = texture;
	}

	public int Width => Texture.Width;
	public int Height => Texture.Height;
	public double Rotation { get; set; }
	public double ScaleX { get; set; } = 1;
	public double ScaleY { get; set; } = 1;
	public Vector2I? Origin { get; set; }
	public bool FlipX { get; set; }
	public bool FlipY { get; set; }

	internal Texture Texture { get; }

	internal abstract IntPtr GetClip();
}
