using System.Reflection;
using Rokuro.Core;
using Rokuro.Dtos;
using Rokuro.Graphics;
using Rokuro.MathUtils;

namespace Rokuro.Objects;

public class Scene
{
	public string Name { get; protected set; } = "Scene";
	public Coroutines Coroutines { get; } = new();

	protected List<GameObject> GameObjects { get; } = new();
	protected List<IMouseInteractable> MouseInteractables { get; } = new();
	protected List<Camera> Cameras { get; } = new();

	public void RegisterGameObject(GameObject gameObject)
	{
		GameObjects.Add(gameObject);
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

	internal void DoCoroutines()
	{
		Coroutines.Execute();
		foreach (GameObject gameObject in GameObjects)
			gameObject.Coroutines.Execute();
	}

	internal void DoRender()
	{
		foreach (GameObject gameObject in GameObjects)
			gameObject.Draw();
	}

	internal void DoMouseOvers(Vector2I mousePosition)
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

	internal void DoClicks(Vector2I mousePosition)
	{
		foreach (IMouseInteractable interactable in MouseInteractables)
			if (interactable.IsMouseOver(mousePosition))
				interactable.OnClick();
	}

	internal static Scene FromDto(SceneDto dto)
	{
		Type type = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).SelectMany(a => a.GetTypes())
			.FirstOrDefault(t => t.FullName != null && t.FullName.Equals(dto.Class))!;
		var scene = (Scene)Activator.CreateInstance(type)!;
		scene.Name = dto.Name;
		foreach (CustomPropertyDto property in dto.CustomProperties)
		{
			PropertyInfo? pi = type.GetProperty(property.Name);
			pi?.SetValue(scene, Convert.ChangeType(property.Value, pi.PropertyType));
		}
		dto.Cameras.ForEach(cameraDto => scene.RegisterCamera(Camera.FromDto(cameraDto)));
		dto.GameObjects.ForEach(objectDto =>
			scene.RegisterGameObject(GameObject.FromDto(objectDto, scene.GetCamera(objectDto.Camera))));
		return scene;
	}
}
