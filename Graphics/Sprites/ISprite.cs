namespace Rokuro.Graphics;

public interface ISprite
{
	int GetWidth();
	int GetHeight();
	internal (IntPtr texture, IntPtr clip) GetRenderData();
}
