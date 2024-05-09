using JetBrains.Annotations;
using Newtonsoft.Json;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.Inputs;
using Rokuro.MathUtils;
using Rokuro.Objects;
using Rokuro.Sound;
using SDL2;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_RendererFlags;
using static SDL2.SDL.SDL_WindowEventID;
using static SDL2.SDL.SDL_WindowFlags;

namespace Rokuro.Core;

class AppImpl
{
	int _fpsLimit = 60;
	IntPtr _renderer = IntPtr.Zero;

	public AppImpl()
	{
		InitSDL();
		FrameDelay = (uint)(1000 / _fpsLimit);
	}

	public static AppImpl ActiveImpl { get; set; } = new();

	public int FPSLimit
	{
		get => _fpsLimit;
		set
		{
			_fpsLimit = value;
			FrameDelay = (uint)(1000 / value);
		}
	}

	public int DeltaTime { get; set; }

	internal IntPtr Renderer
	{
		get
		{
			if (_renderer == IntPtr.Zero)
				Logger.ThrowError("App was not setup. Make sure to call App.Setup(AppProperties) before running!");
			return _renderer;
		}
		private set => _renderer = value;
	}

	IntPtr Window { get; set; }
	bool Running { get; set; } = true;
	uint FrameDelay { get; set; }

	public virtual void Setup(AppProperties properties)
	{
		SettingsModel settings = new();
		if (File.Exists("settings.yaml"))
		{
			try
			{
				settings = new DeserializerBuilder()
					.WithNamingConvention(UnderscoredNamingConvention.Instance)
					.Build()
					.Deserialize<SettingsModel>(File.ReadAllText("settings.yaml"));
			}
			catch (Exception e)
			{
				Logger.ThrowError($"Couldn't load settings: {e.Message}");
				return;
			}
		}
		File.WriteAllText("settings.yaml", new SerializerBuilder()
			.WithNamingConvention(UnderscoredNamingConvention.Instance)
			.Build()
			.Serialize(settings));

		SDL.SDL_WindowFlags windowFlags = 0;
		if (settings.Fullscreen == 0)
			windowFlags = SDL_WINDOW_RESIZABLE;
		else if (settings.Fullscreen == 1)
			windowFlags = SDL_WINDOW_FULLSCREEN;
		else if (settings.Fullscreen == 2)
			windowFlags = SDL_WINDOW_FULLSCREEN_DESKTOP;
		else
			Logger.ThrowError("Invalid fullscreen setting in settings.yaml");
		Window = SDL.SDL_CreateWindow(properties.Name, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED,
			properties.WindowWidth, properties.WindowHeight, windowFlags);
		if (Window == IntPtr.Zero)
			Logger.ThrowSDLError("Window could not be created", ErrorSource.SDL);

		SDL.SDL_RendererFlags rendererFlags = 0;
		if (settings.HardwareAcceleration)
			rendererFlags |= SDL_RENDERER_ACCELERATED;
		if (settings.VSync)
			rendererFlags |= SDL_RENDERER_PRESENTVSYNC;
		Renderer = SDL.SDL_CreateRenderer(Window, -1, rendererFlags);
		if (Renderer == IntPtr.Zero)
			Logger.ThrowSDLError("Renderer could not be created", ErrorSource.SDL);
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderSetLogicalSize(Renderer, properties.WindowWidth, properties.WindowHeight);
		if (settings.RenderQuality > 2)
			Logger.ThrowError("Invalid render quality setting in settings.yaml");
		SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, settings.RenderQuality.ToString());

		Drawer.BaseWidth = properties.WindowWidth;
		Drawer.BaseHeight = properties.WindowHeight;
		Drawer.BgColor = properties.BackgroundColor;

		SpriteManager.LoadTextures();
		SoundManager.LoadSoundsAndMusic();

		if (Directory.Exists(Path.Combine("assets", "autogen", "scenes")))
			SceneManager.LoadScenes(Directory.GetFiles(Path.Combine("assets", "autogen", "scenes"), "*.json")
				.Select(path => JsonConvert.DeserializeObject<SceneDto>(File.ReadAllText(path))!)
				.Select(sceneDto => Scene.FromDto(sceneDto))
				.ToList());
	}

	public virtual void Run()
	{
		uint startTime, frameTime;
		while (Running)
		{
			startTime = SDL.SDL_GetTicks();

			while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
			{
				Vector2I mousePosition = Input.GetMousePosition();
				SceneManager.CurrentScene.DoMouseOvers(mousePosition);

				switch (e.type)
				{
					case SDL_MOUSEBUTTONDOWN:
						if (e.button.button == SDL.SDL_BUTTON_LEFT)
							SceneManager.CurrentScene.DoClicks(mousePosition);
						break;
					case SDL_WINDOWEVENT:
						if (e.window.windowEvent == SDL_WINDOWEVENT_SIZE_CHANGED)
							UpdateWindowSize();
						break;
					case SDL_QUIT:
						Quit();
						break;
				}

				Input.HandleEvent(e);
			}

			Drawer.RenderStart();
			SceneManager.CurrentScene.DoRender();
			Drawer.RenderComplete();
			SceneManager.SwitchScenes();

			frameTime = SDL.SDL_GetTicks() - startTime;
			if (frameTime < FrameDelay)
				SDL.SDL_Delay(FrameDelay - frameTime);
			frameTime = SDL.SDL_GetTicks() - startTime;
			DeltaTime = (int)frameTime;
			Console.WriteLine(DeltaTime);
		}

		ShutdownSDL();
	}

	public virtual void Quit()
	{
		Logger.LogInfo("Quitting...");
		Running = false;
	}

	void InitSDL()
	{
		Logger.LogInfo("Initializing SDL...");

		if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO | SDL.SDL_INIT_TIMER) != 0)
			Logger.ThrowSDLError("SDL could not initialize", ErrorSource.SDL);

		if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
			Logger.ThrowSDLError("SDL_image could not initialize", ErrorSource.IMG);

		if (SDL_ttf.TTF_Init() != 0)
			Logger.ThrowSDLError("SDL_ttf could not initialize", ErrorSource.TTF);

		if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) != 0)
			Logger.ThrowSDLError("SDL_mixer could not initialize", ErrorSource.Mix);

		Logger.LogInfo("SDL initialized");
	}

	void ShutdownSDL()
	{
		Logger.LogInfo("Shutting down SDL...");
		SDL.SDL_DestroyWindow(Window);
		SDL_mixer.Mix_Quit();
		SDL_ttf.TTF_Quit();
		SDL_image.IMG_Quit();
		SDL.SDL_Quit();
	}

	void UpdateWindowSize()
	{
		Logger.LogInfo("Updating window size...");

		SDL.SDL_GetWindowSize(Window, out int w, out int h);
		(float width, float height) = (w, h);
		(float baseWidth, float baseHeight) = (Drawer.BaseWidth, Drawer.BaseHeight);

		if (width / height > baseWidth / baseHeight)
		{
			float newW = height * baseWidth / baseHeight;
			Drawer.WidthOffset = (int)(width - newW) / 2;
			Drawer.HeightOffset = 0;
			width = newW;
		}
		else if (width / height < baseWidth / baseHeight)
		{
			float newH = width * baseHeight / baseWidth;
			Drawer.WidthOffset = 0;
			Drawer.HeightOffset = (int)(height - newH) / 2;
			height = newH;
		}
		else
		{
			Drawer.WidthOffset = 0;
			Drawer.HeightOffset = 0;
		}

		Drawer.WidthMultiplier = baseWidth / width;
		Drawer.HeightMultiplier = baseHeight / height;
	}

	class SettingsModel
	{
		[YamlMember(Description =
			"Whether application should be launched windowed or in fullscreen. 0 - Windowed, 1 - Native fullscreen, 2 - Borderless [Default: 0]")]
		[UsedImplicitly]
		public int Fullscreen { get; set; }

		[YamlMember(Description =
			"Scaling method used. 0 - Nearest Neighbor, 1 - Linear, 2 - 'Best' (currently the same as linear) [Default: 2]")]
		[UsedImplicitly]
		public int RenderQuality { get; set; } = 2;

		[YamlMember(Description = "Whether to use hardware acceleration. [Default: true]")] [UsedImplicitly]
		public bool HardwareAcceleration { get; set; } = true;

		[YamlMember(Description =
			"Whether to use vertical synchronization (synchronizing to display refresh rate). [Default: true]")]
		[UsedImplicitly]
		public bool VSync { get; set; } = true;
	}
}
