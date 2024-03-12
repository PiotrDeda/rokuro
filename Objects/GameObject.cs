using System.Reflection;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class GameObject
{
	public GameObject() {}

	public GameObject(Vector2D position, Sprite sprite, Camera camera)
	{
		Position = position;
		Sprite = sprite;
		Camera = camera;
	}

	public bool Enabled { get; set; } = true;
	public Vector2D Position { get; set; }
	public Sprite? Sprite { get; set; }
	public Camera? Camera { get; set; }

	public virtual void Draw()
	{
		if (Enabled && Sprite != null && Camera != null)
			Camera.Draw(Sprite, Position);
	}

	public static GameObject FromDto(GameObjectDto dto, Camera camera)
	{
		Type objectType = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(a => a.GetTypes())
			.FirstOrDefault(t => t.FullName != null && t.FullName.Equals(dto.Class))!;
		Type spriteType = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(a => a.GetTypes())
			.FirstOrDefault(t => t.FullName != null && t.FullName.Equals(dto.SpriteType))!;
		GameObject? o;
		if (objectType == typeof(TextObject) || spriteType == typeof(TextSprite))
			o = new TextObject(new(dto.X, dto.Y), camera, dto.Sprite, new(255, 255, 255), SpriteManager.DefaultFont);
		else
			o = (GameObject)Activator.CreateInstance(objectType, new Vector2D(dto.X, dto.Y),
				SpriteManager.CreateSprite(dto.Sprite, spriteType), camera)!;
		foreach (CustomPropertyDto property in dto.CustomProperties)
		{
			PropertyInfo? pi = objectType.GetProperty(property.Name);
			pi?.SetValue(o, Convert.ChangeType(property.Value, pi.PropertyType));
		}
		return o;
	}
}
