using Rokuro.Graphics;
using Rokuro.Math;

namespace Rokuro.Objects;

public class InteractableObject : SimpleObject, IMouseInteractable
{
	public InteractableObject(Sprite sprite, Camera camera) : base(sprite, camera) {}

	public bool IsMouseOver(Vector mousePosition)
	{
		Vector screenPosition = Camera.GetScreenPosition(Position);
		return Enabled &&
			   mousePosition.X >= screenPosition.X &&
			   mousePosition.X <= screenPosition.X + Sprite.Width * Camera.Scale &&
			   mousePosition.Y >= screenPosition.Y &&
			   mousePosition.Y <= screenPosition.Y + Sprite.Height * Camera.Scale;
	}

	public virtual void OnMouseOver() {}

	public virtual void OnClick() {}

	public bool IsMousedOver { get; set; } = false;
}
