using Rokuro.Inputs;

namespace Rokuro.Objects;

public interface IEventReceiver
{
	void HandleEvent(IInputEvent e);
}
