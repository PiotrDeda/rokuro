using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

class SpriteManageImpl
{
	public SpriteManageImpl()
	{
		DefaultFont = LoadDefaultFont();
	}

	public static SpriteManageImpl ActiveImpl { get; set; } = new();

	public Font DefaultFont { get; private set; }

	IntPtr Renderer { get; } = App.Renderer;
	Dictionary<string, SpriteTemplate> SpriteTemplates { get; set; } = new();

	public virtual T CreateSprite<T>(string name) where T : ISprite
	{
		if (SpriteTemplates.TryGetValue(name, out SpriteTemplate? template))
			return (T)Activator.CreateInstance(typeof(T), template)!;

		Logger.ThrowError($"Sprite {name} not found!");
		throw new();
	}

	public virtual void LoadSpriteTemplates(Dictionary<string, SpriteTemplate> sprites)
	{
		SpriteTemplates = sprites;
	}

	public virtual Texture LoadTexture(string filename) =>
		new(SDL_image.IMG_LoadTexture(Renderer, $"assets/textures/{filename}.png"));

	Font LoadDefaultFont()
	{
		IntPtr font = SDL_ttf.TTF_OpenFont("assets_engine/CascadiaMono.ttf", 20);
		if (font == IntPtr.Zero)
			Logger.ThrowSDLError("Failed to load font", ErrorSource.TTF);
		return new(font);
	}
}
