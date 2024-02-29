namespace Rokuro.Core;

public static class RNG
{
	public static Random Rand => RNGImpl.ActiveImpl.Rand;
	public static int NextStandardInt(int mean, int deviation) => RNGImpl.ActiveImpl.NextStandardInt(mean, deviation);
}
