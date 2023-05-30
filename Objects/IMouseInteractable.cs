using Rokuro.Math;

namespace Rokuro.Objects;

public interface IMouseInteractable
{
	bool IsMouseOver(Vector mousePosition);
	void OnMouseOver();
	void OnClick();

	public bool IsMousedOver { get; set; }
}
