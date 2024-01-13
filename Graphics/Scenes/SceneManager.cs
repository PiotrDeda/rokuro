using Rokuro.Core;

namespace Rokuro.Graphics;

public class SceneManager
{
	internal Scene CurrentScene { get; private set; } = new();

	List<Scene> Scenes { get; set; } = new();
	Scene NextScene { get; set; } = new();

	public void SetNextScene(string sceneName)
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

	public void LoadScenes(List<Scene> scenes)
	{
		Scenes = scenes;
		CurrentScene = Scenes[0];
		NextScene = Scenes[0];
	}

	internal void SwitchScenes()
	{
		if (CurrentScene != NextScene)
		{
			CurrentScene = NextScene;
			CurrentScene.OnEnter();
		}
	}
}
