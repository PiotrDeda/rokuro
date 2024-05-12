using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class LineObject : GameObject
{
	public Vector2I End { get; set; } = Vector2I.Zero;
	public Color Color { get; set; } = Color.White;
	public int Thickness { get; set; } = 1;

	public override void Draw()
	{
		if (Enabled && Camera != null)
			Camera.DrawLine(Position, End, Color, Thickness);
	}
}
