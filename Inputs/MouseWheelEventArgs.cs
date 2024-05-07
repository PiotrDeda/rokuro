using Rokuro.MathUtils;

namespace Rokuro.Inputs;

public class MouseWheelEventArgs(Vector2I scroll) : EventArgs
{
	public Vector2I Scroll { get; } = scroll;
}
