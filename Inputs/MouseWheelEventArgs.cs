using Rokuro.MathUtils;

namespace Rokuro.Inputs;

public class MouseWheelEventArgs(Vector2D scroll) : EventArgs
{
	public Vector2D Scroll { get; } = scroll;
}
