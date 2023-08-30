namespace Rokuro.Math;

public struct Vector2D : IEquatable<Vector2D>
{
	public Vector2D(int x, int y)
	{
		X = x;
		Y = y;
	}

	public int X { get; set; }
	public int Y { get; set; }

	public void Clamp(Vector2D min, Vector2D max)
	{
		if (X < min.X)
			X = min.X;
		else if (X > max.X)
			X = max.X;
		if (Y < min.Y)
			Y = min.Y;
		else if (Y > max.Y)
			Y = max.Y;
	}

	public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
	public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
	public static Vector2D operator *(Vector2D a, Vector2D b) => new(a.X * b.X, a.Y * b.Y);
	public static Vector2D operator /(Vector2D a, Vector2D b) => new(a.X / b.X, a.Y / b.Y);

	public static Vector2D operator +(Vector2D a, int b) => new(a.X + b, a.Y + b);
	public static Vector2D operator -(Vector2D a, int b) => new(a.X - b, a.Y - b);
	public static Vector2D operator *(Vector2D a, int b) => new(a.X * b, a.Y * b);
	public static Vector2D operator /(Vector2D a, int b) => new(a.X / b, a.Y / b);

	public static Vector2D operator +(int a, Vector2D b) => new(a + b.X, a + b.Y);
	public static Vector2D operator -(int a, Vector2D b) => new(a - b.X, a - b.Y);
	public static Vector2D operator *(int a, Vector2D b) => new(a * b.X, a * b.Y);
	public static Vector2D operator /(int a, Vector2D b) => new(a / b.X, a / b.Y);
	
	
	public static Vector2D operator *(Vector2D a, float b) => new((int)(a.X * b), (int)(a.Y * b));
	public static Vector2D operator /(Vector2D a, float b) => new((int)(a.X / b), (int)(a.Y / b));
	
	public static Vector2D operator *(float a, Vector2D b) => new((int)(a * b.X), (int)(a * b.Y));
	public static Vector2D operator /(float a, Vector2D b) => new((int)(a / b.X), (int)(a / b.Y));

	public static Vector2D operator -(Vector2D a) => new(-a.X, -a.Y);

	public static bool operator ==(Vector2D a, Vector2D b) => a.X == b.X && a.Y == b.Y;

	public static bool operator !=(Vector2D a, Vector2D b) => a.X != b.X || a.Y != b.Y;

	public bool Equals(Vector2D other) => this == other;
	public override bool Equals(object? obj) => obj is Vector2D other && Equals(other);
	public override int GetHashCode() => HashCode.Combine(X, Y);

	public override string ToString() => $"[{X}, {Y}]";
}
