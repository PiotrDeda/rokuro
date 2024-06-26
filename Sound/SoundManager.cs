namespace Rokuro.Sound;

public static class SoundManager
{
	public static void PlaySound(string name, int loops = 0) => SoundManagerImpl.ActiveImpl.PlaySound(name, loops);
	public static void PlayMusic(string name) => SoundManagerImpl.ActiveImpl.PlayMusic(name);
	public static void PauseMusic() => SoundManagerImpl.ActiveImpl.PauseMusic();
	public static void ResumeMusic() => SoundManagerImpl.ActiveImpl.ResumeMusic();
	public static void StopMusic() => SoundManagerImpl.ActiveImpl.StopMusic();
	public static bool IsMusicPlaying() => SoundManagerImpl.ActiveImpl.IsMusicPlaying();

	internal static void LoadSoundsAndMusic() => SoundManagerImpl.ActiveImpl.LoadSoundsAndMusic();
}
