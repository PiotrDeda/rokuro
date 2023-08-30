using Rokuro.Math;

namespace Rokuro.Graphics;

public class Camera
{
	Vector2D _position = new(0, 0);

	public virtual float Scale => Scales[SelectedScale];

	public Vector2D Position
	{
		get => _position;
		set
		{
			_position = value;
			_position.Clamp(BoundaryMin, BoundaryMax);
		}
	}

	public Vector2D BoundaryMin { get; set; } = new(0, 0);
	public Vector2D BoundaryMax { get; set; } = new(0, 0);

	float[] Scales { get; } = { 0.5f, 0.75f, 1.0f, 1.25f, 1.5f };
	int SelectedScale { get; set; } = 2;

	public virtual Vector2D GetScreenPosition(Vector2D position) => (position - Position) * Scale;

	public void CenterOn(Vector2D position)
	{
		Position = (position - new Vector2D(App.WindowData.BaseWidth / 2, App.WindowData.BaseHeight / 2)) / Scale;
	}

	public void ZoomIn()
	{
		if (SelectedScale < Scales.Length - 1)
			SelectedScale++;
	}

	public void ZoomOut()
	{
		if (SelectedScale > 0)
			SelectedScale--;
	}

	public void ResetZoom()
	{
		SelectedScale = 2;
	}
}
