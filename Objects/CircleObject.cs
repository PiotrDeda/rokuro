using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class CircleObject : GameObject
{
	public int Radius { get; set; } = 1;
	public Color Color { get; set; } = Color.White;

	public override void Draw()
	{
		if (Enabled && Camera != null)
			Camera.DrawCircle(Position, Radius, Color);
	}
}
