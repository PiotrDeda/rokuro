using Rokuro.MathUtils;

namespace Rokuro.Input;

public record MouseWheelEvent(Vector2D Scroll) : IInputEvent;
