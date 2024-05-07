using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Inputs;

public static class Input
{
	public static event EventHandler<KeyDownEventArgs>? KeyDownEvent
	{
		add => InputImpl.ActiveImpl.KeyDownEvent += value;
		remove => InputImpl.ActiveImpl.KeyDownEvent -= value;
	}

	public static event EventHandler<MouseMotionEventArgs>? MouseMotionEvent
	{
		add => InputImpl.ActiveImpl.MouseMotionEvent += value;
		remove => InputImpl.ActiveImpl.MouseMotionEvent -= value;
	}

	public static event EventHandler<MouseWheelEventArgs>? MouseWheelEvent
	{
		add => InputImpl.ActiveImpl.MouseWheelEvent += value;
		remove => InputImpl.ActiveImpl.MouseWheelEvent -= value;
	}

	public static void SetKeyEvent(Keycode key, KeyEvent keyEvent) => InputImpl.ActiveImpl.SetKeyEvent(key, keyEvent);
	public static void RemoveKeyEvent(KeyEvent keyEvent) => InputImpl.ActiveImpl.RemoveKeyEvent(keyEvent);
	public static Vector2I GetMousePosition() => InputImpl.ActiveImpl.GetMousePosition();

	internal static void HandleEvent(SDL.SDL_Event e) => InputImpl.ActiveImpl.HandleEvent(e);
}
