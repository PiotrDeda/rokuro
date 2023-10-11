namespace Rokuro.Graphics;

public class Font
{
	readonly IntPtr _fontPtr;

	internal Font(IntPtr fontPtr)
	{
		_fontPtr = fontPtr;
	}

	internal IntPtr Get() => _fontPtr;
}
