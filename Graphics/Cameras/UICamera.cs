using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public class UICamera : Camera
{
	public override float Scale => 1.0f;

	public override Vector2I GetScreenPosition(Vector2I position) => new(position.X, position.Y);
}
