using Rokuro.Core;

namespace Rokuro.MathUtils;

public static class RandomUtils
{
	public static int NextStandardInt(int mean, int deviation)
	{
		// Box-Muller transform
		double u1 = 1 - App.Rand.NextDouble();
		double u2 = 1 - App.Rand.NextDouble();
		return (int)Math.Round(mean + deviation * Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2));
	}
}
