using Rokuro.MathUtils;

namespace Rokuro.Inputs;

public class MouseMotionEventArgs(Vector2D relativeMotion, bool leftButton, bool rightButton) : EventArgs
{
	public Vector2D RelativeMotion { get; } = relativeMotion;
	public bool LeftButton { get; } = leftButton;
	public bool RightButton { get; } = rightButton;
}
