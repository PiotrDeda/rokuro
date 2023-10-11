using Rokuro.Core;
using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public class UICamera : Camera
{
	public UICamera(Drawer drawer, WindowData windowData) : base(drawer, windowData) {}

	public override float Scale => 1.0f;

	public override Vector2D GetScreenPosition(Vector2D position) => new(position.X, position.Y);
}
