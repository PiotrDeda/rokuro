using System.Reflection;
using Rokuro.Dtos;
using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public class Camera
{
	Vector2I _position = new(0, 0);

	public string Name { get; set; } = "";
	public virtual float Scale => Scales[SelectedScale];

	public Vector2I Position
	{
		get => _position;
		set
		{
			_position = value;
			_position.Clamp(BoundaryMin, BoundaryMax);
		}
	}

	public Vector2I BoundaryMin { get; set; } = new(0, 0);
	public Vector2I BoundaryMax { get; set; } = new(0, 0);

	float[] Scales { get; } = { 0.5f, 0.75f, 1.0f, 1.25f, 1.5f };
	int SelectedScale { get; set; } = 2;

	public virtual Vector2I GetScreenPosition(Vector2I position) => (position - Position) * Scale;

	public void Draw(Sprite sprite, Vector2I position) => Drawer.Draw(sprite, GetScreenPosition(position), Scale);

	public void CenterOn(Vector2I position) =>
		Position = position - new Vector2I(Drawer.BaseWidth / 2, Drawer.BaseHeight / 2) / Scale;

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

	public void ResetZoom() => SelectedScale = 2;

	internal static Camera FromDto(CameraDto dto)
	{
		Type type = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(a => a.GetTypes())
			.FirstOrDefault(t => t.FullName != null && t.FullName.Equals(dto.Class))!;
		var camera = (Camera)Activator.CreateInstance(type)!;
		camera.Name = dto.Name;
		foreach (CustomPropertyDto property in dto.CustomProperties)
		{
			PropertyInfo? pi = type.GetProperty(property.Name);
			pi?.SetValue(camera, Convert.ChangeType(property.Value, pi.PropertyType));
		}
		return camera;
	}
}
