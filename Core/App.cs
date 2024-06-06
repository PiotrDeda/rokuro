namespace Rokuro.Core;

public static class App
{
	public static bool DebugRenderHitboxes => AppImpl.DebugRenderHitboxes;
	public static float PhysicsDeltaTime => AppImpl.PhysicsDeltaTime;
	public static int DeltaTime => AppImpl.ActiveImpl.DeltaTime;

	public static float Gravity
	{
		get => AppImpl.ActiveImpl.Gravity;
		set => AppImpl.ActiveImpl.Gravity = value;
	}

	internal static IntPtr Renderer => AppImpl.ActiveImpl.Renderer;

	public static void Setup(AppProperties properties) => AppImpl.ActiveImpl.Setup(properties);
	public static void Run() => AppImpl.ActiveImpl.Run();
	public static void Quit() => AppImpl.ActiveImpl.Quit();
}
