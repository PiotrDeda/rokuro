using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public class TextSprite : ISprite
{
	IntPtr Texture { get; set; }
	int Width { get; set; }
	int Height { get; set; }

	int ISprite.GetWidth() => Width;
	int ISprite.GetHeight() => Height;
	public (IntPtr texture, IntPtr clip) GetRenderData() => (Texture, IntPtr.Zero);

	internal void RefreshTexture(string text, Font font, Color color)
	{
		if (font.Get() != IntPtr.Zero)
			(Texture, Width, Height) = Drawer.GetTextTexture(text, font, color);
	}
}
