using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Graphics;

public class TextSprite() : Sprite(new())
{
	internal override IntPtr GetClip() => IntPtr.Zero;

	internal void RefreshRawTexture(string text, Font font, int fontSize, Color color)
	{
		if (font.Get() == IntPtr.Zero)
			return;
		SDL_ttf.TTF_SetFontSize(font.Get(), fontSize);
		Texture.RawTexture = Drawer.GetTextRawTexture(text, font, color);
	}
}
