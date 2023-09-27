using Rokuro.Core;
using SDL2;

namespace Rokuro.Graphics;

public class SpriteManager
{
	internal IntPtr DefaultFont { get; private set; }

	Dictionary<string, StaticSpriteTemplate> SpritesTemplates { get; set; } = new();

	public ISprite CreateSpriteFromTemplate(string name)
	{
		if (SpritesTemplates.TryGetValue(name, out StaticSpriteTemplate? template))
			if (template is AnimatedSpriteTemplate animatedTemplate)
				return new AnimatedSprite(animatedTemplate);
			else
				return new StaticSprite(template);

		Logger.ThrowError($"Sprite {name} not found!");
		return null!;
	}

	internal void LoadSpriteTemplates(Dictionary<string, StaticSpriteTemplate> sprites)
	{
		SpritesTemplates = sprites;
		DefaultFont = LoadDefaultFont();
	}

	internal IntPtr LoadTexture(string filename) =>
		SDL_image.IMG_LoadTexture(App.Drawer.Renderer, $"assets/textures/{filename}.png");

	IntPtr LoadDefaultFont()
	{
		IntPtr font = SDL_ttf.TTF_OpenFont("assets_engine/CascadiaMono.ttf", 20);
		if (font == IntPtr.Zero)
			Logger.ThrowSDLError("Failed to load font", ErrorSource.TTF);
		return font;
	}
}
