using Rokuro.MathUtils;

namespace Rokuro.Physics;

public class CircleHitbox : IHitbox
{
	public int Radius { get; set; }
	public Vector2 Offset { get; set; } = Vector2.Zero;
	public Vector2 Position { get; set; } = Vector2.Zero;
	public float HalfSize => Radius;

	public (bool isCollision, Vector2 normal, float penetration) Intersects(IHitbox other)
	{
		switch (other)
		{
			case RectHitbox r:
				Vector2 closest = Position.Clamp(r.Position - r.HalfSizeV, r.Position + r.HalfSizeV);
				return (closest.DistanceSquared(Position) < Radius * Radius,
					(Position - closest).Normalize(), Radius - (Position - closest).Length);
			case CircleHitbox c:
				float radii = Radius + c.Radius;
				Vector2 delta = Position - c.Position;
				float deltaLength = delta.Length;
				bool isCollision = deltaLength < radii;
				return (isCollision, isCollision ? delta.Normalize() : Vector2.NaN, radii - deltaLength);
			default:
				return (false, Vector2.NaN, 0);
		}
	}
}
