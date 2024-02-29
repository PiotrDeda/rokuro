using Rokuro.MathUtils;
using SDL2;

namespace Rokuro.Inputs;

public static class Input
{
	public static void SetKeyEvent(Keycode key, KeyEvent keyEvent) => InputImpl.ActiveImpl.SetKeyEvent(key, keyEvent);
	public static void RemoveKeyEvent(KeyEvent keyEvent) => InputImpl.ActiveImpl.RemoveKeyEvent(keyEvent);
	public static Vector2D GetMousePosition() => InputImpl.ActiveImpl.GetMousePosition();

	internal static IInputEvent GetInputEvent(SDL.SDL_Event e) => InputImpl.ActiveImpl.GetInputEvent(e);
}
