using Rokuro.Core;

namespace Rokuro.Graphics;

public class SceneManagerImpl
{
	public static SceneManagerImpl ActiveImpl { get; set; } = new();

	internal Scene CurrentScene { get; private set; } = new();

	List<Scene> Scenes { get; set; } = new();
	Scene NextScene { get; set; } = new();

	public virtual void SetNextScene(string sceneName)
	{
		Logger.LogInfo($"Switching scene from \"{CurrentScene.Name}\" to \"{sceneName}\"");
		try
		{
			NextScene = Scenes.First(scene => scene.Name == sceneName);
		}
		catch (InvalidOperationException)
		{
			Logger.ThrowError($"Scene \"{sceneName}\" not found");
		}
	}

	public virtual void LoadScenes(List<Scene> scenes)
	{
		Scenes = Scenes.Concat(scenes).ToList();
		CurrentScene = Scenes[0];
		NextScene = Scenes[0];
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
