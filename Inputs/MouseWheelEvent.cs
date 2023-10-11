using Rokuro.MathUtils;

namespace Rokuro.Inputs;

public record MouseWheelEvent(Vector2D Scroll) : IInputEvent;
