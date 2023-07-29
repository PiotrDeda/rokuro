namespace Rokuro.Math;

public struct Vector : IEquatable<Vector>
{
	const float Eps = 0.0001f;

	public Vector(float x, float y)
	{
		X = x;
		Y = y;
	}

	public float X { get; set; }
	public float Y { get; set; }

	public void Clamp(Vector min, Vector max)
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

	public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
	public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
	public static Vector operator *(Vector a, Vector b) => new(a.X * b.X, a.Y * b.Y);
	public static Vector operator /(Vector a, Vector b) => new(a.X / b.X, a.Y / b.Y);

	public static Vector operator +(Vector a, float b) => new(a.X + b, a.Y + b);
	public static Vector operator -(Vector a, float b) => new(a.X - b, a.Y - b);
	public static Vector operator *(Vector a, float b) => new(a.X * b, a.Y * b);
	public static Vector operator /(Vector a, float b) => new(a.X / b, a.Y / b);

	public static Vector operator +(float a, Vector b) => new(a + b.X, a + b.Y);
	public static Vector operator -(float a, Vector b) => new(a - b.X, a - b.Y);
	public static Vector operator *(float a, Vector b) => new(a * b.X, a * b.Y);
	public static Vector operator /(float a, Vector b) => new(a / b.X, a / b.Y);

	public static Vector operator -(Vector a) => new(-a.X, -a.Y);

	public static bool operator ==(Vector a, Vector b) =>
		System.Math.Abs(a.X - b.X) < Eps && System.Math.Abs(a.Y - b.Y) < Eps;

	public static bool operator !=(Vector a, Vector b) =>
		System.Math.Abs(a.X - b.X) > Eps || System.Math.Abs(a.Y - b.Y) > Eps;

	public bool Equals(Vector other) => this == other;
	public override bool Equals(object? obj) => obj is Vector other && Equals(other);
	public override int GetHashCode() => HashCode.Combine(X, Y);
}
