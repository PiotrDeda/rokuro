namespace Rokuro.MathUtils;

public struct Vector2I
{
	public float Length => MathF.Sqrt(X * X + Y * Y);
	public float LengthSquared => X * X + Y * Y;

	public static readonly Vector2I Zero = new(0, 0);
	public static readonly Vector2I One = new(1, 1);
	public static readonly Vector2I Up = new(0, -1);
	public static readonly Vector2I Down = new(0, 1);
	public static readonly Vector2I Left = new(-1, 0);
	public static readonly Vector2I Right = new(1, 0);

	public int X;
	public int Y;

	public Vector2I(int x, int y)
	{
		X = x;
		Y = y;
	}

	public float Dot(Vector2I other) => X * other.X + Y * other.Y;
	public float PerpDot(Vector2I other) => X * other.Y - Y * other.X;
	public float Distance(Vector2I other) => (this - other).Length;
	public float DistanceSquared(Vector2I other) => (this - other).LengthSquared;

	public Vector2I Clamp(Vector2I min, Vector2I max) => new(
		X < min.X ? min.X : X > max.X ? max.X : X,
		Y < min.Y ? min.Y : Y > max.Y ? max.Y : Y
	);

	public static Vector2I Interpolate(Vector2I start, Vector2I end, float value, Func<float, float> function) => new(
		(int)(start.X + (end.X - start.X) * function(value)),
		(int)(start.Y + (end.Y - start.Y) * function(value))
	);

	public static Vector2I operator +(Vector2I left, Vector2I right) => new(left.X + right.X, left.Y + right.Y);
	public static Vector2I operator -(Vector2I left, Vector2I right) => new(left.X - right.X, left.Y - right.Y);
	public static Vector2I operator *(Vector2I left, Vector2I right) => new(left.X * right.X, left.Y * right.Y);
	public static Vector2I operator /(Vector2I left, Vector2I right) => new(left.X / right.X, left.Y / right.Y);

	public static Vector2I operator +(Vector2I left, int right) => new(left.X + right, left.Y + right);
	public static Vector2I operator -(Vector2I left, int right) => new(left.X - right, left.Y - right);
	public static Vector2I operator *(Vector2I left, int right) => new(left.X * right, left.Y * right);
	public static Vector2I operator /(Vector2I left, int right) => new(left.X / right, left.Y / right);

	public static Vector2I operator +(int left, Vector2I right) => new(left + right.X, left + right.Y);
	public static Vector2I operator -(int left, Vector2I right) => new(left - right.X, left - right.Y);
	public static Vector2I operator *(int left, Vector2I right) => new(left * right.X, left * right.Y);
	public static Vector2I operator /(int left, Vector2I right) => new(left / right.X, left / right.Y);

	public static Vector2I operator *(Vector2I left, float right) => new((int)(left.X * right), (int)(left.Y * right));
	public static Vector2I operator /(Vector2I left, float right) => new((int)(left.X / right), (int)(left.Y / right));

	public static Vector2I operator *(float left, Vector2I right) => new((int)(left * right.X), (int)(left * right.Y));
	public static Vector2I operator /(float left, Vector2I right) => new((int)(left / right.X), (int)(left / right.Y));

	public static Vector2I operator -(Vector2I value) => new(-value.X, -value.Y);

	public override string ToString() => $"[{X}, {Y}]";
}
