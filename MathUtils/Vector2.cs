namespace Rokuro.MathUtils;

public record Vector2(float X, float Y)
{
	public static Vector2 Zero => new(0, 0);
	public static Vector2 One => new(1, 1);
	public static Vector2 Up => new(0, 1);
	public static Vector2 Down => new(0, -1);
	public static Vector2 Left => new(-1, 0);
	public static Vector2 Right => new(1, 0);

	public float Length => MathF.Sqrt(X * X + Y * Y);

	public float Dot(Vector2 other) => X * other.X + Y * other.Y;
	public float PerpDot(Vector2 other) => X * other.Y - Y * other.X;
	public float Distance(Vector2 other) => (this - other).Length;
	public Vector2 Normalize() => this / Length;

	public Vector2 Clamp(Vector2 min, Vector2 max) => new(
		X < min.X ? min.X : X > max.X ? max.X : X,
		Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y
	);

	public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);
	public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);
	public static Vector2 operator *(Vector2 left, Vector2 right) => new(left.X * right.X, left.Y * right.Y);
	public static Vector2 operator /(Vector2 left, Vector2 right) => new(left.X / right.X, left.Y / right.Y);

	public static Vector2 operator +(Vector2 left, float right) => new(left.X + right, left.Y + right);
	public static Vector2 operator -(Vector2 left, float right) => new(left.X - right, left.Y - right);
	public static Vector2 operator *(Vector2 left, float right) => new(left.X * right, left.Y * right);
	public static Vector2 operator /(Vector2 left, float right) => new(left.X / right, left.Y / right);

	public static Vector2 operator +(float left, Vector2 right) => new(left + right.X, left + right.Y);
	public static Vector2 operator -(float left, Vector2 right) => new(left - right.X, left - right.Y);
	public static Vector2 operator *(float left, Vector2 right) => new(left * right.X, left * right.Y);
	public static Vector2 operator /(float left, Vector2 right) => new(left / right.X, left / right.Y);

	public static Vector2 operator -(Vector2 value) => new(-value.X, -value.Y);

	public override string ToString() => $"[{X}, {Y}]";
}
