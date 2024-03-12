using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public class TextSprite : Sprite
{
	public TextSprite() : base(new()) {}

	internal override IntPtr GetClip() => IntPtr.Zero;

	internal void RefreshRawTexture(string text, Font font, Color color)
	{
		if (font.Get() != IntPtr.Zero)
			Texture.RawTexture = Drawer.GetTextRawTexture(text, font, color);
	}
}
