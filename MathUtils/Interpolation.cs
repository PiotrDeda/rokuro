namespace Rokuro.MathUtils;

public static class Interpolation
{
	public static Func<float, float> Linear => t => t;
	public static Func<float, float> Sine => t => MathF.Sin((t - 0.5f) * MathF.PI) * 0.5f + 0.5f;
}
