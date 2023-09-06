using Rokuro.Graphics;
using Rokuro.Input;
using Rokuro.Math;
using SDL2;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_RendererFlags;
using static SDL2.SDL.SDL_WindowEventID;
using static SDL2.SDL.SDL_WindowFlags;

namespace Rokuro.Core;

class AppInstance
{
	public WindowData WindowData { get; } = new();
	public Drawer Drawer { get; } = new();
	public SpriteManager SpriteManager { get; } = new();
	public SceneManager SceneManager { get; } = new();
	public InputManager InputManager { get; } = new();
	public Random Rand { get; } = new();

	bool SDLInitialized { get; set; }
	bool Running { get; set; } = true;
	IntPtr Window { get; set; }

	public void Run(AppProperties properties, Func<Dictionary<string, StaticSpriteTemplate>> spriteTemplates,
		Func<List<Scene>> scenes)
	{
		if (!SDLInitialized)
			InitSDL();

		WindowData.BaseWidth = properties.WindowWidth;
		WindowData.BaseHeight = properties.WindowHeight;
		(Window, IntPtr renderer) = MakeWindowRenderer(properties);
		Drawer.Renderer = renderer;
		Drawer.BgColor = properties.BackgroundColor;
		SpriteManager.LoadSpriteTemplates(spriteTemplates());
		SceneManager.LoadScenes(scenes());

		while (Running)
		{
			while (SDL.SDL_PollEvent(out SDL.SDL_Event e) != 0)
			{
				Vector2D mousePosition = GetMousePosition();
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

				InputManager.HandleEvent(e);
			}

			Drawer.RenderStart();
			SceneManager.CurrentScene.DoRender();
			Drawer.RenderComplete();
			SceneManager.SwitchScenes();
		}

		ShutdownSDL();
	}

	public void Quit()
	{
		Logger.LogInfo("Quitting...");
		Running = false;
	}

	public Vector2D GetMousePosition()
	{
		SDL.SDL_GetMouseState(out int x, out int y);
		return new Vector2D((int)((x - App.WindowData.WidthOffset) * App.WindowData.WidthMultiplier),
			(int)((y - App.WindowData.HeightOffset) * App.WindowData.HeightMultiplier));
	}

	void InitSDL()
	{
		Logger.LogInfo("Initializing SDL...");
		if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_TIMER) < 0)
			Logger.ThrowSDLError("SDL could not initialize!", ErrorSource.SDL);

		if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
			Logger.ThrowSDLError("SDL_image could not initialize!", ErrorSource.IMG);

		if (SDL_ttf.TTF_Init() == -1)
			Logger.ThrowSDLError("SDL_ttf could not initialize!", ErrorSource.TTF);

		SDLInitialized = true;
		Logger.LogInfo("SDL initialized");
	}

	void ShutdownSDL()
	{
		Logger.LogInfo("Shutting down SDL...");
		SDL.SDL_DestroyWindow(Window);
		SDL_ttf.TTF_Quit();
		SDL_image.IMG_Quit();
		SDL.SDL_Quit();
	}

	(IntPtr, IntPtr) MakeWindowRenderer(AppProperties properties)
	{
		IntPtr window = SDL.SDL_CreateWindow(properties.Name, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED,
			WindowData.BaseWidth, WindowData.BaseHeight, SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE);
		if (window == IntPtr.Zero)
			Logger.ThrowSDLError("Window could not be created!", ErrorSource.SDL);

		// TODO: Animation speed is wrong without VSync – needs a deeper fix
		IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
		if (renderer == IntPtr.Zero)
			Logger.ThrowSDLError("Renderer could not be created!", ErrorSource.SDL);
		SDL.SDL_SetRenderDrawColor(renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderSetLogicalSize(renderer, WindowData.BaseWidth, WindowData.BaseHeight);

		return (window, renderer);
	}

	void UpdateWindowSize()
	{
		Logger.LogInfo("Updating window size...");

		SDL.SDL_GetWindowSize(Window, out int w, out int h);
		(float width, float height) = (w, h);
		(float baseWidth, float baseHeight) = (WindowData.BaseWidth, WindowData.BaseHeight);

		if (width / height > baseWidth / baseHeight)
		{
			float newW = height * baseWidth / baseHeight;
			WindowData.WidthOffset = (int)(width - newW) / 2;
			WindowData.HeightOffset = 0;
			width = newW;
		}
		else if (width / height < baseWidth / baseHeight)
		{
			float newH = width * baseHeight / baseWidth;
			WindowData.WidthOffset = 0;
			WindowData.HeightOffset = (int)(height - newH) / 2;
			height = newH;
		}
		else
		{
			WindowData.WidthOffset = 0;
			WindowData.HeightOffset = 0;
		}

		WindowData.WidthMultiplier = baseWidth / width;
		WindowData.HeightMultiplier = baseHeight / height;
	}
}
