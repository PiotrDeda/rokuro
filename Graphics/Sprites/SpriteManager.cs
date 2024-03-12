namespace Rokuro.Graphics;

public static class SpriteManager
{
	public static Font DefaultFont => SpriteManageImpl.ActiveImpl.DefaultFont;

	internal static IntPtr BlankRect => SpriteManageImpl.ActiveImpl.BlankRect;

	public static T CreateSprite<T>(string name) where T : Sprite => SpriteManageImpl.ActiveImpl.CreateSprite<T>(name);
	public static Sprite CreateSprite(string name, Type type) => SpriteManageImpl.ActiveImpl.CreateSprite(name, type);

	internal static void LoadTextures() => SpriteManageImpl.ActiveImpl.LoadTextures();
}
