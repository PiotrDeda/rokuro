using Rokuro.Math;
using SDL2;
using static SDL2.SDL.SDL_EventType;

namespace Rokuro.Input;

public class InputManager
{
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

	internal void HandleEvent(SDL.SDL_Event e)
	{
		switch (e.type)
		{
			case SDL_MOUSEMOTION:
				App.SceneManager.CurrentScene.HandleEvent(new MouseMotionEvent(
					new Vector2D(e.motion.xrel, e.motion.yrel),
					(e.motion.state & SDL.SDL_BUTTON_LMASK) != 0,
					(e.motion.state & SDL.SDL_BUTTON_RMASK) != 0
				));
				break;
			case SDL_MOUSEWHEEL:
				App.SceneManager.CurrentScene.HandleEvent(new MouseWheelEvent(
					new Vector2D(e.wheel.x, e.wheel.y)
				));
				break;
			case SDL_KEYDOWN:
				if (KeyEvents.TryGetValue((Keycode)e.key.keysym.sym, out KeyEvent? keyEvent))
					App.SceneManager.CurrentScene.HandleEvent(keyEvent);
				break;
		}
	}
}
