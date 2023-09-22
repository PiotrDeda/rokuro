using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class InteractableObject : SimpleObject, IMouseInteractable
{
	public InteractableObject(ISprite sprite, Camera camera) : base(sprite, camera) {}

	public bool WasMouseoverHandled { get; set; } = false;

	public bool IsMouseOver(Vector2D mousePosition)
	{
		Vector2D screenPosition = Camera.GetScreenPosition(Position);
		return Enabled &&
			   mousePosition.X >= screenPosition.X &&
			   mousePosition.X <= screenPosition.X + Sprite.Width() * Camera.Scale &&
			   mousePosition.Y >= screenPosition.Y &&
			   mousePosition.Y <= screenPosition.Y + Sprite.Height() * Camera.Scale;
	}

	public virtual void OnMouseover() {}

	public virtual void OnClick() {}
}
