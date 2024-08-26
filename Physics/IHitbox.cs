using Rokuro.MathUtils;

namespace Rokuro.Physics;

public interface IHitbox
{
	public Vector2 Offset { get; set; }
	public Vector2 Position { get; set; }
	public float HalfSize { get; }

	public (bool isCollision, Vector2 normal, float penetration) Intersects(IHitbox other);
}
