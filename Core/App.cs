namespace Rokuro.Core;

public static class App
{
	internal static IntPtr Renderer => AppImpl.ActiveImpl.Renderer;
	internal static bool WasSetup => AppImpl.ActiveImpl.WasSetup;

	public static void Setup(AppProperties properties) => AppImpl.ActiveImpl.Setup(properties);
	public static void Run() => AppImpl.ActiveImpl.Run();
	public static void Quit() => AppImpl.ActiveImpl.Quit();
}
