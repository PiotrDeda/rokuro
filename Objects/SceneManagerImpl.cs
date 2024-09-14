using Rokuro.Core;

namespace Rokuro.Objects;

class SceneManagerImpl
{
	public static SceneManagerImpl ActiveImpl { get; set; } = new();

	internal Scene CurrentScene { get; private set; } = new();

	List<Scene> Scenes { get; set; } = new();
	Scene NextScene { get; set; } = new();

	public virtual Scene GetScene(string name)
	{
		try
		{
			return Scenes.First(scene => scene.Name == name);
		}
		catch (InvalidOperationException)
		{
			Logger.ThrowError($"Scene \"{name}\" not found");
			return null!;
		}
	}

	public virtual void SetNextScene(string name)
	{
		Logger.LogInfo($"Switching scene from \"{CurrentScene.Name}\" to \"{name}\"");
		try
		{
			NextScene = Scenes.First(scene => scene.Name == name);
		}
		catch (InvalidOperationException)
		{
			Logger.ThrowError($"Scene \"{name}\" not found");
		}
	}

	public virtual void LoadScenes(List<Scene> scenes)
	{
		Scenes = Scenes.Concat(scenes).ToList();
		scenes.ForEach(scene => scene.OnLoaded());
	}

	internal virtual void SwitchScenes()
	{
		if (CurrentScene != NextScene)
		{
			CurrentScene = NextScene;
			CurrentScene.OnEnter();
		}
	}
}
