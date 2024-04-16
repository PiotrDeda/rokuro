using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class InteractableObject : GameObject, IMouseInteractable
{
	public bool WasMouseoverHandled { get; set; } = false;

	public bool IsMouseOver(Vector2D mousePosition)
	{
		if (!Enabled || Sprite == null || Camera == null)
			return false;
		Vector2D screenPosition = Camera.GetScreenPosition(Position);
		return mousePosition.X >= screenPosition.X &&
			   mousePosition.X <= screenPosition.X + Sprite.Width * Camera.Scale &&
			   mousePosition.Y >= screenPosition.Y &&
			   mousePosition.Y <= screenPosition.Y + Sprite.Height * Camera.Scale;
	}

	public virtual void OnMouseover() {}

	public virtual void OnClick() {}
}
