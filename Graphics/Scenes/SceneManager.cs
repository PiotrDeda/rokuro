namespace Rokuro.Graphics;

public static class SceneManager
{
	internal static Scene CurrentScene => SceneManagerImpl.ActiveImpl.CurrentScene;

	public static void SetNextScene(string sceneName) => SceneManagerImpl.ActiveImpl.SetNextScene(sceneName);
	public static void LoadScenes(List<Scene> scenes) => SceneManagerImpl.ActiveImpl.LoadScenes(scenes);

	internal static void SwitchScenes() => SceneManagerImpl.ActiveImpl.SwitchScenes();
}
