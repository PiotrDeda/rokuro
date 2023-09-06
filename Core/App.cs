using Rokuro.Graphics;
using Rokuro.Input;
using Rokuro.Math;

namespace Rokuro.Core;

public static class App
{
	static AppInstance Instance { get; } = new();

	public static SpriteManager SpriteManager => Instance.SpriteManager;
	public static SceneManager SceneManager => Instance.SceneManager;
	public static InputManager InputManager => Instance.InputManager;
	public static Drawer Drawer => Instance.Drawer;
	public static Random Rand => Instance.Rand;

	internal static WindowData WindowData => Instance.WindowData;

	public static void Run(AppProperties properties, Func<Dictionary<string, StaticSpriteTemplate>> spriteTemplates,
		Func<List<Scene>> scenes) => Instance.Run(properties, spriteTemplates, scenes);

	public static void Quit() => Instance.Quit();

	public static Vector2D GetMousePosition() => Instance.GetMousePosition();
}
