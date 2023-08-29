using Rokuro.Core;
using Rokuro.Graphics;
using Rokuro.Input;
using Rokuro.Math;

namespace Rokuro;

public static class App
{
	static AppInstance Instance { get; } = new();

	public static SceneManager SceneManager => Instance.SceneManager;
	public static InputManager InputManager => Instance.InputManager;
	public static Drawer Drawer => Instance.Drawer;
	public static Random Rand => Instance.Rand;

	internal static WindowData WindowData => Instance.WindowData;
	internal static IntPtr DefaultFont => Instance.DefaultFont;

	public static void Run(AppProperties properties, Func<Dictionary<string, Sprite>> sprites, Func<List<Scene>> scenes)
		=> Instance.Run(properties, sprites, scenes);

	public static void Quit() => Instance.Quit();

	public static Sprite GetSprite(string name) => Instance.GetSprite(name);

	public static Vector GetMousePosition() => Instance.GetMousePosition();

	internal static IntPtr LoadTexture(string filename) => Instance.LoadTexture(filename);
}
