namespace Rokuro.Core;

public class WindowData
{
	public int BaseWidth { get; internal set; }
	public int BaseHeight { get; internal set; }
	public float WidthMultiplier { get; internal set; } = 1;
	public float HeightMultiplier { get; internal set; } = 1;
	public int WidthOffset { get; internal set; }
	public int HeightOffset { get; internal set; }
}
