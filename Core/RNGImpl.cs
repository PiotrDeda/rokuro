namespace Rokuro.Core;

public class RNGImpl
{
	public static RNGImpl ActiveImpl { get; set; } = new();

	public Random Rand { get; } = new();

	public virtual int NextStandardInt(int mean, int deviation)
	{
		// Box-Muller transform
		double u1 = 1 - Rand.NextDouble();
		double u2 = 1 - Rand.NextDouble();
		return (int)Math.Round(mean + deviation * Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2));
	}
}
