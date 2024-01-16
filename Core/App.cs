using Newtonsoft.Json;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.Inputs;
using Rokuro.MathUtils;
using SDL2;
using static SDL2.SDL.SDL_EventType;
using static SDL2.SDL.SDL_RendererFlags;
using static SDL2.SDL.SDL_WindowEventID;
using static SDL2.SDL.SDL_WindowFlags;

namespace Rokuro.Core;

public abstract class App : IQuittable
{
	public App(AppProperties properties)
	{
		InitSDL();
		(Window, IntPtr renderer) = MakeWindowRenderer(properties);
		SpriteManager = new(renderer);
		Drawer = new(renderer, properties.WindowWidth, properties.WindowHeight, properties.BackgroundColor);
		Input = new(Drawer);
	}

	protected SpriteManager SpriteManager { get; }
	protected SceneManager SceneManager { get; } = new();
	protected Input Input { get; }
	protected RNG RNG { get; } = new();
	protected Drawer Drawer { get; }

	bool Running { get; set; } = true;
	IntPtr Window { get; }

	public void Quit()
	{
		Logger.LogInfo("Quitting...");
		Running = false;
	}

	public abstract void Init();

	public void Run()
	{
		Init();

		if (Directory.Exists("assets/autogen/data/scenes"))
			SceneManager.LoadScenes(Directory.GetFiles("assets/autogen/data/scenes", "*.json")
				.Select(path => JsonConvert.DeserializeObject<SceneDto>(File.ReadAllText(path))!)
				.Select(sceneDto => Scene.FromDto(sceneDto, SpriteManager, Drawer))
				.ToList());

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

				SceneManager.CurrentScene.HandleEvent(Input.GetInputEvent(e));
			}

			Drawer.RenderStart();
			SceneManager.CurrentScene.DoRender();
			Drawer.RenderComplete();
			SceneManager.SwitchScenes();
		}

		ShutdownSDL();
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
			properties.WindowWidth, properties.WindowHeight, SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE);
		if (window == IntPtr.Zero)
			Logger.ThrowSDLError("Window could not be created!", ErrorSource.SDL);

		// TODO: Animation speed is wrong without VSync – needs a deeper fix (delta time?)
		IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
		if (renderer == IntPtr.Zero)
			Logger.ThrowSDLError("Renderer could not be created!", ErrorSource.SDL);
		SDL.SDL_SetRenderDrawColor(renderer, 0x00, 0x00, 0x00, 0xFF);
		SDL.SDL_RenderSetLogicalSize(renderer, properties.WindowWidth, properties.WindowHeight);

		return (window, renderer);
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
