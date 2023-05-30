using Rokuro.Core;

namespace Rokuro.Graphics;

public class SceneManager
{
	internal Scene CurrentScene { get; private set; } = new();

	List<Scene> Scenes { get; } = new();
	Scene NextScene { get; set; } = new();

	public void LoadScenes(List<Scene> loadedScenes)
	{
		Scenes.Clear();
		Scenes.AddRange(loadedScenes);
		CurrentScene = Scenes[0];
		NextScene = Scenes[0];
	}

	public void SetNextScene(Scene scene)
	{
		NextScene = scene;
		Logger.LogInfo($"Switching from {CurrentScene.Name} to {NextScene.Name}");
	}

	internal void SwitchScenes()
	{
		if (CurrentScene != NextScene)
		{
			Scene previousScene = CurrentScene;
			CurrentScene = NextScene;
			CurrentScene.Enter(previousScene);
		}
	}
}
