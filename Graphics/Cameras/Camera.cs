using Rokuro.Math;

namespace Rokuro.Graphics;

public class Camera
{
	public float Scale { get; set; } = 1.0f;

	public Vector GetScreenPosition(Vector position) => new(position.X, position.Y);
}
