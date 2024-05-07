using Rokuro.MathUtils;

namespace Rokuro.Inputs;

public class MouseMotionEventArgs(Vector2I relativeMotion, bool leftButton, bool rightButton) : EventArgs
{
	public Vector2I RelativeMotion { get; } = relativeMotion;
	public bool LeftButton { get; } = leftButton;
	public bool RightButton { get; } = rightButton;
}
