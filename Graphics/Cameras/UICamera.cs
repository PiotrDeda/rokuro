using Rokuro.Math;

namespace Rokuro.Graphics;

public class UICamera : Camera
{
	public override float Scale => 1.0f;

	public override Vector2D GetScreenPosition(Vector2D position) => new(position.X, position.Y);
}
