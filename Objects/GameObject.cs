using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class GameObject
{
	public GameObject() {}

	public GameObject(Vector2D position, ISprite sprite, Camera camera)
	{
		Position = position;
		Sprite = sprite;
		Camera = camera;
	}

	public bool Enabled { get; set; } = true;
	public Vector2D Position { get; set; }
	public ISprite? Sprite { get; set; }
	public Camera? Camera { get; set; }

	public virtual void Draw()
	{
		if (Enabled && Sprite != null && Camera != null)
			Camera.Draw(Sprite, Position);
	}

	public static GameObject FromDto(GameObjectDto dto, Camera camera, SpriteManager spriteManager)
	{
		Type type = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(a => a.GetTypes())
			.FirstOrDefault(t => t.FullName != null && t.FullName.Equals(dto.Class))!;
		var o = (GameObject)Activator.CreateInstance(type, new Vector2D(dto.X, dto.Y),
			spriteManager.CreateSprite<StaticSprite>(dto.Sprite), camera)!;
		o.Position = new(dto.X, dto.Y);
		return o;
	}
}
