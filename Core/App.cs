namespace Rokuro.Core;

public static class App
{
	public static int FPSLimit => AppImpl.ActiveImpl.FPSLimit;
	public static int DeltaTime => AppImpl.ActiveImpl.DeltaTime;

	internal static IntPtr Renderer => AppImpl.ActiveImpl.Renderer;

	public static void Setup(AppProperties properties) => AppImpl.ActiveImpl.Setup(properties);
	public static void Run() => AppImpl.ActiveImpl.Run();
	public static void Quit() => AppImpl.ActiveImpl.Quit();
}
