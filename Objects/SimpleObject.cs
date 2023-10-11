using Rokuro.Graphics;

namespace Rokuro.Objects;

public class SimpleObject : BaseObject, IDrawable
{
	public SimpleObject(ISprite sprite, Camera camera)
	{
		Sprite = sprite;
		Camera = camera;
	}

	public ISprite Sprite { get; set; }
	public Camera Camera { get; set; }

	public void Draw()
	{
		if (Enabled)
			Camera.Draw(Sprite, Position);
	}
}
