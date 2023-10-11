using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class SpriteManager
{
	public SpriteManager(IntPtr renderer)
	{
		DefaultFont = LoadDefaultFont();
		Renderer = renderer;
	}

	public Font DefaultFont { get; private set; }

	IntPtr Renderer { get; }
	Dictionary<string, StaticSpriteTemplate> SpriteTemplates { get; set; } = new();

	public ISprite CreateSpriteFromTemplate(string name)
	{
		if (SpriteTemplates.TryGetValue(name, out StaticSpriteTemplate? template))
			if (template is AnimatedSpriteTemplate animatedTemplate)
				return new AnimatedSprite(animatedTemplate);
			else
				return new StaticSprite(template);

		Logger.ThrowError($"Sprite {name} not found!");
		return null!;
	}

	public void LoadSpriteTemplates(Dictionary<string, StaticSpriteTemplate> sprites)
	{
		SpriteTemplates = sprites;
	}

	public Texture LoadTexture(string filename) =>
		new(SDL_image.IMG_LoadTexture(Renderer, $"assets/textures/{filename}.png"));

	Font LoadDefaultFont()
	{
		IntPtr font = SDL_ttf.TTF_OpenFont("assets_engine/CascadiaMono.ttf", 20);
		if (font == IntPtr.Zero)
			Logger.ThrowSDLError("Failed to load font", ErrorSource.TTF);
		return new(font);
	}
}
