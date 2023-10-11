using Rokuro.MathUtils;

namespace Rokuro.Inputs;

public record MouseMotionEvent(Vector2D RelativeMotion, bool LeftButton, bool RightButton) : IInputEvent;
