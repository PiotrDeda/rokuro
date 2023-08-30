using Rokuro.Math;

namespace Rokuro.Input;

public record MouseMotionEvent(Vector2D RelativeMotion, bool LeftButton, bool RightButton);
