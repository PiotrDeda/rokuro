using Rokuro.Core;

namespace Rokuro.Graphics;

public class SceneManager
{
	internal Scene CurrentScene { get; private set; } = new();

	List<Scene> Scenes { get; set; } = new();
	Scene NextScene { get; set; } = new();

	public void SetNextScene(int sceneId)
	{
		NextScene = Scenes[sceneId];
		Logger.LogInfo($"Switching from {CurrentScene.Name} to {NextScene.Name}");
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
			Scene previousScene = CurrentScene;
			CurrentScene = NextScene;
			CurrentScene.OnEnter(previousScene);
		}
	}
}
