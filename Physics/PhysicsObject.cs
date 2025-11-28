using Rokuro.Core;
using Rokuro.MathUtils;

namespace Rokuro.Physics;

public class PhysicsObject
{
	public Vector2 Position { get; set; } = Vector2.Zero;
	public Vector2 Velocity { get; set; } = Vector2.Zero;

	public float Mass
	{
		get;
		set
		{
			field = value;
			if (CacheGravity)
				GravityForce = new(0, App.Gravity * Mass);
			if (value == 0)
				InverseMass = 0;
			else
				InverseMass = 1.0f / value;
		}
	} = 1;

	public float Elasticity { get; set; } = 0.5f;
	public bool IsAffectedByGravity { get; set; } = true;
	public List<IHitbox> Hitboxes { get; } = [];
	public bool CacheGravity { get; set; } = true;

	Vector2 GravityForce { get; set; }
	float InverseMass { get; set; } = 1;

	public void DoPhysics()
	{
		Position += Velocity * App.PhysicsDeltaTime;
		if (IsAffectedByGravity)
			ApplyForce(CacheGravity ? GravityForce : new(0, App.Gravity * Mass));
	}

	public void ApplyForce(Vector2 force) => Velocity += force * InverseMass;

	public (bool isCollision, Vector2 Normal, float penetration) Intersects(PhysicsObject other)
	{
		foreach (IHitbox hitbox in Hitboxes)
		{
			foreach (IHitbox otherHitbox in other.Hitboxes)
			{
				if ((hitbox.Position.X - hitbox.Position.X) * (hitbox.Position.X - hitbox.Position.X) +
					(hitbox.Position.Y - hitbox.Position.Y) * (hitbox.Position.Y - hitbox.Position.Y) >
					hitbox.HalfSize + otherHitbox.HalfSize)
					continue;
				(bool isCollision, Vector2 normal, float penetration) = hitbox.Intersects(otherHitbox);
				if (isCollision)
					return (isCollision, normal, penetration);
			}
		}
		return (false, Vector2.NaN, 0);
	}

	public void DoCollision(PhysicsObject other, Vector2 normal, float penetration)
	{
		Vector2 relativeVelocity = other.Velocity - Velocity;
		if (float.IsNaN(normal.X) || float.IsNaN(normal.Y))
			return;
		float velocityAlongNormal = relativeVelocity.Dot(normal);
		float impulse = -(1 + (Elasticity + other.Elasticity) / 2) * velocityAlongNormal / (InverseMass + other.InverseMass);
		if (float.IsNaN(impulse))
			return;
		Vector2 impulseVector = impulse * normal;
		Velocity -= impulseVector * InverseMass;
		other.Velocity += impulseVector * other.InverseMass;
		if (InverseMass != 0)
			Position += normal * penetration;
		else if (other.InverseMass != 0)
			other.Position -= normal * penetration;
	}
}
