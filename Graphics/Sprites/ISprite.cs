namespace Rokuro.Graphics;

public interface ISprite
{
	int Width();
	int Height();
	internal IntPtr Texture();
	internal IntPtr Clip();
}
