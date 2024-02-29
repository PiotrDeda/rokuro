namespace Rokuro.Graphics;

public static class SpriteManager
{
	public static Font DefaultFont => SpriteManageImpl.ActiveImpl.DefaultFont;

	public static T CreateSprite<T>(string name) where T : ISprite => SpriteManageImpl.ActiveImpl.CreateSprite<T>(name);

	public static void LoadSpriteTemplates(Dictionary<string, SpriteTemplate> sprites) =>
		SpriteManageImpl.ActiveImpl.LoadSpriteTemplates(sprites);

	public static Texture LoadTexture(string filename) => SpriteManageImpl.ActiveImpl.LoadTexture(filename);
}
