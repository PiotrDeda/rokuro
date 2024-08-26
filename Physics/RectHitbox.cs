using Rokuro.MathUtils;

namespace Rokuro.Physics;

public class RectHitbox : IHitbox
{
	static readonly Vector2[] Sides = { new(1, 0), new(-1, 0), new(0, 1), new(0, -1) };

	public Vector2 HalfSizeV { get; set; } = Vector2.Zero;
	public Vector2 Offset { get; set; } = Vector2.Zero;
	public Vector2 Position { get; set; } = Vector2.Zero;
	public float HalfSize => HalfSizeV.X > HalfSizeV.Y ? HalfSizeV.X : HalfSizeV.Y;

	public (bool isCollision, Vector2 normal, float penetration) Intersects(IHitbox other)
	{
		switch (other)
		{
			case RectHitbox r:
				return IntersectsWithRect(r);
			case CircleHitbox c:
				Vector2 closest = c.Position.Clamp(Position - HalfSizeV, Position + HalfSizeV);
				return (closest.DistanceSquared(c.Position) < c.Radius * c.Radius,
					(closest - c.Position).Normalize(), c.Radius - (closest - c.Position).Length);
			default:
				return (false, new(float.NaN, float.NaN), 0);
		}
	}

	public (bool isCollision, Vector2 normal, float penetration) IntersectsWithRect(RectHitbox r)
	{
		if (Position.X - HalfSizeV.X < r.Position.X + r.HalfSizeV.X &&
			Position.X + HalfSizeV.X > r.Position.X - r.HalfSizeV.X &&
			Position.Y - HalfSizeV.Y < r.Position.Y + r.HalfSizeV.Y &&
			Position.Y + HalfSizeV.Y > r.Position.Y - r.HalfSizeV.Y)
		{
			Vector2 maxA = Position + HalfSizeV;
			Vector2 minA = Position - HalfSizeV;
			Vector2 maxB = r.Position + r.HalfSizeV;
			Vector2 minB = r.Position - r.HalfSizeV;

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
		return (false, Vector2.NaN, 0);
	}
}
