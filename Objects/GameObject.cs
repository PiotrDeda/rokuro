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
}
