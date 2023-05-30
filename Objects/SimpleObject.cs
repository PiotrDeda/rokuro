using Rokuro.Graphics;

namespace Rokuro.Objects;

public class SimpleObject : BaseObject, IDrawable
{
	public SimpleObject(Sprite sprite, Camera camera)
	{
		Sprite = sprite;
		Camera = camera;
	}

	public Sprite Sprite { get; set; }
	public Camera Camera { get; set; }

	public void Draw()
	{
		if (Enabled)
			App.Drawer.Draw(Sprite, Camera, Position);
	}
}
