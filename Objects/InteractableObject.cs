using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class InteractableObject : GameObject, IMouseInteractable
{
	public InteractableObject(Vector2D position, ISprite sprite, Camera camera) : base(position, sprite, camera) {}

	public bool WasMouseoverHandled { get; set; } = false;

	public bool IsMouseOver(Vector2D mousePosition)
	{
		if (!Enabled || Sprite == null || Camera == null)
			return false;
		Vector2D screenPosition = Camera.GetScreenPosition(Position);
		return mousePosition.X >= screenPosition.X &&
			   mousePosition.X <= screenPosition.X + Sprite.GetWidth() * Camera.Scale &&
			   mousePosition.Y >= screenPosition.Y &&
			   mousePosition.Y <= screenPosition.Y + Sprite.GetHeight() * Camera.Scale;
	}

	public virtual void OnMouseover() {}

	public virtual void OnClick() {}
}
