using Rokuro.MathUtils;

namespace Rokuro.Physics;

public class RectHitbox : IHitbox
{
	static readonly Vector2[] Sides = { new(1, 0), new(-1, 0), new(0, 1), new(0, -1) };

	public Vector2 HalfSize { get; set; } = Vector2.Zero;
	public Vector2 Offset { get; set; } = Vector2.Zero;
	public Vector2 Position { get; set; } = Vector2.Zero;

	public (bool isCollision, Vector2 normal, float penetration) Intersects(IHitbox other)
	{
		switch (other)
		{
			case RectHitbox r:
				return IntersectsWithRect(r);
			case CircleHitbox c:
				Vector2 closest = c.Position.Clamp(Position - HalfSize, Position + HalfSize);
				return (closest.DistanceSquared(c.Position) < c.Radius * c.Radius,
					(closest - c.Position).Normalize(), c.Radius - (closest - c.Position).Length);
			default:
				return (false, new(float.NaN, float.NaN), 0);
		}
	}

	public (bool isCollision, Vector2 normal, float penetration) IntersectsWithRect(RectHitbox r)
	{
		if (Position.X - HalfSize.X < r.Position.X + r.HalfSize.X &&
			Position.X + HalfSize.X > r.Position.X - r.HalfSize.X &&
			Position.Y - HalfSize.Y < r.Position.Y + r.HalfSize.Y &&
			Position.Y + HalfSize.Y > r.Position.Y - r.HalfSize.Y)
		{
			Vector2 maxA = Position + HalfSize;
			Vector2 minA = Position - HalfSize;
			Vector2 maxB = r.Position + r.HalfSize;
			Vector2 minB = r.Position - r.HalfSize;

			float[] distances = {
				maxB.X - minA.X,
				maxA.X - minB.X,
				maxB.Y - minA.Y,
				maxA.Y - minB.Y
			};

			float penetration = float.MaxValue;
			Vector2 normal = Vector2.Zero;
			for (int i = 0; i < Sides.Length; i++)
			{
				if (distances[i] < penetration)
				{
					penetration = distances[i];
					normal = Sides[i];
				}
			}

			return (true, normal, penetration);
		}
		return (false, new(float.NaN, float.NaN), 0);
	}
}
