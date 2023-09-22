using Rokuro.MathUtils;

namespace Rokuro.Objects;

public interface IMouseInteractable
{
	bool WasMouseoverHandled { get; set; }

	bool IsMouseOver(Vector2D mousePosition);
	void OnMouseover();
	void OnClick();
}
