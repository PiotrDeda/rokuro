using Rokuro.Input;
using Rokuro.Math;
using Rokuro.Objects;

namespace Rokuro.Graphics;

public class Scene
{
	public string Name { get; protected set; } = "Scene";

	protected List<IDrawable> Drawables { get; } = new();
	protected List<IMouseInteractable> MouseInteractables { get; } = new();

	public void RegisterGameObject(BaseObject gameObject)
	{
		if (gameObject is IDrawable drawable)
			Drawables.Add(drawable);
		if (gameObject is IMouseInteractable interactable)
			MouseInteractables.Add(interactable);
	}

	public virtual void OnEnter(Scene previousScene) {}
	public virtual void HandleEvent(MouseWheelEvent e) {}
	public virtual void HandleEvent(MouseMotionEvent e) {}
	public virtual void HandleEvent(KeyEvent e) {}

	public virtual void DoRender()
	{
		foreach (IDrawable drawable in Drawables)
			drawable.Draw();
	}

	internal void DoMouseOvers(Vector mousePosition)
	{
		foreach (IMouseInteractable interactable in MouseInteractables)
			if (interactable.IsMouseOver(mousePosition))
			{
				if (!interactable.WasMouseoverHandled)
				{
					interactable.OnMouseover();
					interactable.WasMouseoverHandled = true;
				}
			}
			else
			{
				interactable.WasMouseoverHandled = false;
			}
	}

	internal void DoClicks(Vector mousePosition)
	{
		foreach (IMouseInteractable interactable in MouseInteractables)
			if (interactable.IsMouseOver(mousePosition))
				interactable.OnClick();
	}
}
