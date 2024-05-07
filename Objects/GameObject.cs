using System.Reflection;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class GameObject
{
	public bool Enabled { get; set; } = true;
	public Vector2I Position { get; set; } = new(0, 0);
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

		var gameObject = (GameObject)Activator.CreateInstance(objectType)!;

		gameObject.Position = new(dto.PositionX, dto.PositionY);
		gameObject.Camera = camera;

		if (objectType == typeof(TextObject) || spriteType == typeof(TextSprite))
		{
			var textObject = (TextObject)gameObject;
			textObject.Text = dto.Sprite;
			textObject.Color = new(255, 255, 255);
		}
		else
			gameObject.Sprite = SpriteManager.CreateSprite(dto.Sprite, spriteType);

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
