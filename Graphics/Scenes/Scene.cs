using Rokuro.Core;
using Rokuro.Dtos;
using Rokuro.Inputs;
using Rokuro.MathUtils;
using Rokuro.Objects;

namespace Rokuro.Graphics;

public class Scene
{
	public string Name { get; protected set; } = "Scene";

	protected List<GameObject> Drawables { get; } = new();
	protected List<IMouseInteractable> MouseInteractables { get; } = new();
	protected List<Camera> Cameras { get; } = new();

	public void RegisterGameObject(GameObject gameObject)
	{
		Drawables.Add(gameObject);
		if (gameObject is IMouseInteractable interactable)
			MouseInteractables.Add(interactable);
	}

	public void RegisterCamera(Camera camera)
	{
		Cameras.Add(camera);
	}

	public Camera GetCamera(string name)
	{
		try
		{
			return Cameras.First(camera => camera.Name.Equals(name));
		}
		catch (InvalidOperationException)
		{
			Logger.ThrowError($"Camera \"{name}\" not found");
			return null!;
		}
	}

	public virtual void OnEnter() {}
	public virtual void HandleEvent(IInputEvent e) {}

	public virtual void DoRender()
	{
		foreach (GameObject drawable in Drawables)
			drawable.Draw();
	}

	internal void DoMouseOvers(Vector2D mousePosition)
	{
		foreach (IMouseInteractable interactable in MouseInteractables)
			if (interactable.IsMouseOver(mousePosition))
			{
				if (!interactable.WasMouseoverHandled)
				{
					interactable.OnMouseover();
					interactable.WasMouseoverHandled = true;
				}
			}
			else
			{
				interactable.WasMouseoverHandled = false;
			}
	}

	internal void DoClicks(Vector2D mousePosition)
	{
		foreach (IMouseInteractable interactable in MouseInteractables)
			if (interactable.IsMouseOver(mousePosition))
				interactable.OnClick();
	}

	internal static Scene FromDto(SceneDto dto)
	{
		Scene scene = new();
		scene.Name = dto.Name;
		dto.Cameras.ForEach(cameraDto => scene.RegisterCamera(Camera.FromDto(cameraDto)));
		dto.GameObjects.ForEach(objectDto =>
			scene.RegisterGameObject(GameObject.FromDto(objectDto, scene.GetCamera(objectDto.Camera))));
		return scene;
	}
}
