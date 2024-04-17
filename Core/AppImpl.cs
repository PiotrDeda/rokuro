using Newtonsoft.Json;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.Inputs;
using Rokuro.MathUtils;
using Rokuro.Sound;
using SDL2;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_RendererFlags;
using static SDL2.SDL.SDL_WindowEventID;
using static SDL2.SDL.SDL_WindowFlags;

namespace Rokuro.Core;

class AppImpl
{
	IntPtr _renderer = IntPtr.Zero;

	public AppImpl()
	{
		InitSDL();
	}

	public static AppImpl ActiveImpl { get; set; } = new();

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

	internal bool WasSetup { get; private set; }

	IntPtr Window { get; set; }
	bool Running { get; set; } = true;

	public virtual void Setup(AppProperties properties)
	{
		Window = SDL.SDL_CreateWindow(properties.Name, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED,
			properties.WindowWidth, properties.WindowHeight, SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE);
		if (Window == IntPtr.Zero)
			Logger.ThrowSDLError("Window could not be created!", ErrorSource.SDL);

		// TODO: Animation speed is wrong without VSync – needs a deeper fix (delta time?)
		Renderer = SDL.SDL_CreateRenderer(Window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
		if (Renderer == IntPtr.Zero)
			Logger.ThrowSDLError("Renderer could not be created!", ErrorSource.SDL);
		SDL.SDL_SetRenderDrawColor(Renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderSetLogicalSize(Renderer, properties.WindowWidth, properties.WindowHeight);

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

		WasSetup = true;
	}

	public virtual void Run()
	{
		while (Running)
		{
			while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
			{
				Vector2D mousePosition = Input.GetMousePosition();
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
			Logger.ThrowSDLError("SDL could not initialize!", ErrorSource.SDL);

		if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
			Logger.ThrowSDLError("SDL_image could not initialize!", ErrorSource.IMG);

		if (SDL_ttf.TTF_Init() != 0)
			Logger.ThrowSDLError("SDL_ttf could not initialize!", ErrorSource.TTF);

		if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) != 0)
			Logger.ThrowSDLError("SDL_mixer could not initialize!", ErrorSource.Mix);

		Logger.LogInfo("SDL initialized");
	}

	void ShutdownSDL()
	{
		Logger.LogInfo("Shutting down SDL...");
		WasSetup = false;
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
}
