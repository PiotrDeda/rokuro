using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class InteractableObject : GameObject
{
	public bool WasMouseoverHandled { get; set; }

	public virtual bool IsMouseOver(Vector2I mousePosition)
	{
		if (!Enabled || Sprite == null || Camera == null)
			return false;
		Vector2I screenPosition = Camera.GetScreenPosition(Position);
		return mousePosition.X >= screenPosition.X &&
			   mousePosition.X <= screenPosition.X + Sprite.Width * Camera.Scale &&
			   mousePosition.Y >= screenPosition.Y &&
			   mousePosition.Y <= screenPosition.Y + Sprite.Height * Camera.Scale;
	}

	public virtual void OnMouseover() {}

	public virtual void OnClick() {}
}
