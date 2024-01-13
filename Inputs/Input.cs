using Rokuro.Graphics;
using Rokuro.MathUtils;
using SDL2;
using static SDL2.SDL.SDL_EventType;

namespace Rokuro.Inputs;

public class Input
{
	public Input(Drawer drawer)
	{
		Drawer = drawer;
	}

	Drawer Drawer { get; }
	Dictionary<Keycode, KeyEvent> KeyEvents { get; } = new();

	public void SetKeyEvent(Keycode key, KeyEvent keyEvent)
	{
		foreach ((Keycode k, _) in KeyEvents.Where(x => x.Value == keyEvent).ToList())
			KeyEvents.Remove(k);
		KeyEvents[key] = keyEvent;
	}

	public void RemoveKeyEvent(KeyEvent keyEvent)
	{
		KeyEvents.Remove(KeyEvents.First(x => x.Value == keyEvent).Key);
	}

	public Vector2D GetMousePosition()
	{
		SDL.SDL_GetMouseState(out int x, out int y);
		return new((int)((x - Drawer.WidthOffset) * Drawer.WidthMultiplier),
			(int)((y - Drawer.HeightOffset) * Drawer.HeightMultiplier));
	}

	internal IInputEvent GetInputEvent(SDL.SDL_Event e) => e.type switch {
		SDL_MOUSEMOTION => new MouseMotionEvent(
			new(e.motion.xrel, e.motion.yrel),
			(e.motion.state & SDL.SDL_BUTTON_LMASK) != 0,
			(e.motion.state & SDL.SDL_BUTTON_RMASK) != 0
		),
		SDL_MOUSEWHEEL => new MouseWheelEvent(
			new(e.wheel.x, e.wheel.y)
		),
		SDL_KEYDOWN => KeyEvents.TryGetValue((Keycode)e.key.keysym.sym, out KeyEvent? keyEvent)
			? keyEvent
			: new NullEvent(),
		_ => new NullEvent()
	};
}
