using Rokuro.Math;

namespace Rokuro.Graphics;

public class Camera
{
	Vector _position = new(0, 0);

	public virtual float Scale => Scales[SelectedScale];

	public Vector Position
	{
		get => _position;
		set
		{
			_position = value;
			_position.Clamp(BoundaryMin, BoundaryMax);
		}
	}

	public Vector BoundaryMin { get; set; } = new(0, 0);
	public Vector BoundaryMax { get; set; } = new(0, 0);

	float[] Scales { get; } = { 0.5f, 0.75f, 1.0f, 1.25f, 1.5f };
	int SelectedScale { get; set; } = 2;

	public virtual Vector GetScreenPosition(Vector position) =>
		new((position.X - Position.X) * Scale, (position.Y - Position.Y) * Scale);

	public void CenterOn(Vector position)
	{
		Position = position - new Vector(App.WindowData.BaseWidth * 0.5f, App.WindowData.BaseHeight * 0.5f) / Scale;
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
