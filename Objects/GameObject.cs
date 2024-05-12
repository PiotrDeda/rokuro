using System.Collections;
using System.Reflection;
using Rokuro.Core;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class GameObject
{
	public bool Enabled { get; set; } = true;
	public Vector2I Position { get; set; } = Vector2I.Zero;
	public Sprite? Sprite { get; set; }
	public Camera? Camera { get; set; }
	public Coroutines Coroutines { get; } = new();

	public IEnumerator MoveTo(Vector2I end, int durationMs, Func<float, float> interpolationFunction)
	{
		Vector2I start = Position;
		int elapsedTime = 0;
		float progress = 0;
		while (progress < 1)
		{
			progress = (float)elapsedTime / durationMs;
			Position = Vector2I.Interpolate(start, end, (float)elapsedTime / durationMs, interpolationFunction);
			elapsedTime += App.DeltaTime;
			yield return null;
		}
		Position = end;
	}

	public virtual void Draw()
	{
		if (Enabled && Sprite != null && Camera != null)
			Camera.DrawSprite(Sprite, Position);
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
