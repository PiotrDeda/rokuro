using System.Reflection;
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
	protected List<IEventReceiver> EventReceivers { get; } = new();
	protected List<Camera> Cameras { get; } = new();

	public void RegisterGameObject(GameObject gameObject)
	{
		Drawables.Add(gameObject);
		if (gameObject is IMouseInteractable interactable)
			MouseInteractables.Add(interactable);
		if (gameObject is IEventReceiver receiver)
			EventReceivers.Add(receiver);
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

	internal void DoEvents(IInputEvent e)
	{
		HandleEvent(e);
		foreach (IEventReceiver receiver in EventReceivers)
			receiver.HandleEvent(e);
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
