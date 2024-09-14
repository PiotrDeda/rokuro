namespace Rokuro.Objects;

public static class SceneManager
{
	internal static Scene CurrentScene => SceneManagerImpl.ActiveImpl.CurrentScene;

	public static Scene GetScene(string name) => SceneManagerImpl.ActiveImpl.GetScene(name);
	public static void SetNextScene(string name) => SceneManagerImpl.ActiveImpl.SetNextScene(name);
	public static void LoadScenes(List<Scene> scenes) => SceneManagerImpl.ActiveImpl.LoadScenes(scenes);

	internal static void SwitchScenes() => SceneManagerImpl.ActiveImpl.SwitchScenes();
}
