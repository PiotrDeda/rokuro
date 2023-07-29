using Rokuro.Core;
using Rokuro.Graphics;
using Rokuro.Input;

namespace Rokuro;

public static class App
{
	static AppInstance Instance { get; } = new();

	internal static WindowData WindowData => Instance.WindowData;
	internal static Drawer Drawer => Instance.Drawer;
	internal static IntPtr DefaultFont => Instance.DefaultFont;
	internal static SceneManager SceneManager => Instance.SceneManager;
	internal static InputManager InputManager => Instance.InputManager;

	public static void Run(AppProperties properties, Dictionary<string, Sprite> sprites, List<Scene> scenes) =>
		Instance.Run(properties, sprites, scenes);

	public static void Quit() => Instance.Quit();

	internal static IntPtr LoadTexture(string filename) => Instance.LoadTexture(filename);
}
