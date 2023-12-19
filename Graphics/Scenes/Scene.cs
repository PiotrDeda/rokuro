using Rokuro.Inputs;
using Rokuro.MathUtils;
using Rokuro.Objects;

namespace Rokuro.Graphics;

public class Scene
{
	public string Name { get; protected set; } = "Scene";

	protected List<GameObject> Drawables { get; } = new();
	protected List<IMouseInteractable> MouseInteractables { get; } = new();

	public void RegisterGameObject(GameObject gameObject)
	{
		Drawables.Add(gameObject);
		if (gameObject is IMouseInteractable interactable)
			MouseInteractables.Add(interactable);
	}

	public virtual void OnEnter() {}
	public virtual void HandleEvent(IInputEvent e) {}

	public virtual void DoRender()
	{
		foreach (GameObject drawable in Drawables)
			drawable.Draw();
	}

	internal void DoMouseOvers(Vector2D mousePosition)
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

	internal void DoClicks(Vector2D mousePosition)
	{
		foreach (IMouseInteractable interactable in MouseInteractables)
			if (interactable.IsMouseOver(mousePosition))
				interactable.OnClick();
	}
}
