using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Rokuro.Core;
using SDL2;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Rokuro.Graphics;

class SpriteManageImpl
{
	public SpriteManageImpl()
	{
		DefaultFont = LoadDefaultFont();
		IntPtr rectObj = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SDL.SDL_Rect)));
		Marshal.StructureToPtr(new SDL.SDL_Rect { x = 0, y = 0, w = 0, h = 0 }, rectObj, false);
		BlankRect = rectObj;
	}

	public static SpriteManageImpl ActiveImpl { get; set; } = new();

	public Font DefaultFont { get; private set; }

	internal IntPtr BlankRect { get; }

	IntPtr Renderer { get; } = App.Renderer;
	Dictionary<string, Texture> Textures { get; } = new();

	public virtual T CreateSprite<T>(string name) where T : Sprite
	{
		if (Textures.TryGetValue(name, out Texture? texture))
			return (T)Activator.CreateInstance(typeof(T), texture)!;

		Logger.ThrowError($"Sprite {name} not found");
		throw new();
	}

	public virtual Sprite CreateSprite(string name, Type type)
	{
		if (Textures.TryGetValue(name, out Texture? texture))
			return (Sprite)Activator.CreateInstance(type, texture)!;

		Logger.ThrowError($"Sprite {name} not found");
		throw new();
	}

	internal virtual void LoadTextures()
	{
		if (Directory.Exists(Path.Combine("assets", "textures")))
		{
			string[] files = Directory.GetFiles(Path.Combine("assets", "textures"), "*.png", SearchOption.AllDirectories);
			foreach (string file in files)
				AddTexture(file.Split(Path.DirectorySeparatorChar).Skip(2).Aggregate((a, b) => Path.Combine(a, b)));
		}
	}

	void AddTexture(string filename)
	{
		TextureConfigModel textureConfig = new();
		string configFilename = Path.Combine("assets", "textures", filename.Replace(".png", ".yaml"));
		if (File.Exists(configFilename))
			try
			{
				textureConfig = new DeserializerBuilder()
					.WithNamingConvention(UnderscoredNamingConvention.Instance)
					.IgnoreUnmatchedProperties()
					.Build()
					.Deserialize<TextureConfigModel>(File.ReadAllText(configFilename));
			}
			catch (Exception e)
			{
				Logger.ThrowError($"Couldn't load texture: {e.Message}");
				return;
			}
		Textures.Add(filename.Replace(".png", "").Replace('\\', '/'),
			new(SDL_image.IMG_LoadTexture(Renderer, Path.Combine("assets", "textures", filename)),
				textureConfig.States, textureConfig.Frames, textureConfig.Delay));
	}

	Font LoadDefaultFont()
	{
		IntPtr font = SDL_ttf.TTF_OpenFont(Path.Combine("assets_engine", "CascadiaMono.ttf"), 20);
		if (font == IntPtr.Zero)
			Logger.ThrowSDLError("Failed to load font", ErrorSource.TTF);
		return new(font);
	}

	class TextureConfigModel
	{
		[UsedImplicitly] public int States { get; set; } = 1;
		[UsedImplicitly] public int Frames { get; set; } = 1;
		[UsedImplicitly] public int Delay { get; set; } = 30;
	}
}
