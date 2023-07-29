using Rokuro.Math;

namespace Rokuro.Graphics;

public class UICamera : Camera
{
	public override float Scale => 1.0f;

	public override Vector GetScreenPosition(Vector position) => new(position.X, position.Y);
}
