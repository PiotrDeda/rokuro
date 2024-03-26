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
		GameObject? gameObject;
		if (objectType == typeof(TextObject) || spriteType == typeof(TextSprite))
			gameObject = new TextObject(new(dto.PositionX, dto.PositionY), camera, dto.Sprite, new(255, 255, 255),
				SpriteManager.DefaultFont);
		else
			gameObject = (GameObject)Activator.CreateInstance(objectType, new Vector2D(dto.PositionX, dto.PositionY),
				SpriteManager.CreateSprite(dto.Sprite, spriteType), camera)!;
		gameObject.Sprite!.ScaleX = dto.ScaleX;
		gameObject.Sprite.ScaleY = dto.ScaleY;
		gameObject.Sprite.Rotation = dto.Rotation;
		gameObject.Sprite.FlipX = dto.FlipX;
		gameObject.Sprite.FlipY = dto.FlipY;
		foreach (CustomPropertyDto propertyDto in dto.CustomProperties)
		{
			PropertyInfo? pi = objectType.GetProperty(propertyDto.Name);
			pi?.SetValue(gameObject, Convert.ChangeType(propertyDto.Value, pi.PropertyType));
		}
		return gameObject;
	}
}
