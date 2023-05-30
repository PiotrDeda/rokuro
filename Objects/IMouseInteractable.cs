using Rokuro.Math;

namespace Rokuro.Objects;

public interface IMouseInteractable
{
	bool WasMouseoverHandled { get; set; }

	bool IsMouseOver(Vector mousePosition);
	void OnMouseover();
	void OnClick();
}
