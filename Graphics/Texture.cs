namespace Rokuro.Graphics;

public struct Texture
{
	readonly IntPtr _texturePtr;

	internal Texture(IntPtr texturePtr)
	{
		_texturePtr = texturePtr;
	}

	internal IntPtr Get() => _texturePtr;
}
