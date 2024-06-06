using Rokuro.MathUtils;

namespace Rokuro.Physics;

public class CircleHitbox : IHitbox
{
	public int Radius { get; set; }
	public Vector2 Offset { get; set; } = Vector2.Zero;
	public Vector2 Position { get; set; } = Vector2.Zero;

	public (bool isCollision, Vector2 normal, float penetration) Intersects(IHitbox other)
	{
		switch (other)
		{
			case RectHitbox r:
				Vector2 closest = Position.Clamp(r.Position - r.HalfSize, r.Position + r.HalfSize);
				return (closest.DistanceSquared(Position) < Radius * Radius,
					(Position - closest).Normalize(), Radius - (Position - closest).Length);
			case CircleHitbox c:
				float radii = Radius + c.Radius;
				Vector2 delta = Position - c.Position;
				return (Position.DistanceSquared(c.Position) < radii * radii, delta.Normalize(), radii - delta.Length);
			default:
				return (false, new(float.NaN, float.NaN), 0);
		}
	}
}
