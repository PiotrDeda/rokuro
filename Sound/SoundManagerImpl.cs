using Rokuro.Core;
using SDL2;

namespace Rokuro.Sound;

class SoundManagerImpl
{
	public static SoundManagerImpl ActiveImpl { get; set; } = new();

	Dictionary<string, IntPtr> Sounds { get; } = new();
	Dictionary<string, IntPtr> Music { get; } = new();

	public void PlaySound(string name, int loops)
	{
		if (Sounds.TryGetValue(name, out IntPtr sound))
			SDL_mixer.Mix_PlayChannel(-1, sound, loops);
		else
			Logger.ThrowError($"Sound {name} not found");
	}

	public void PlayMusic(string name)
	{
		if (Music.TryGetValue(name, out IntPtr music))
			SDL_mixer.Mix_PlayMusic(music, -1);
		else
			Logger.ThrowError($"Music {name} not found");
	}

	public void PauseMusic() => SDL_mixer.Mix_PauseMusic();
	public void ResumeMusic() => SDL_mixer.Mix_ResumeMusic();
	public void StopMusic() => SDL_mixer.Mix_HaltMusic();
	public bool IsMusicPlaying() => SDL_mixer.Mix_PlayingMusic() == 1;

	internal void LoadSoundsAndMusic()
	{
		if (Directory.Exists(Path.Combine("assets", "sounds")))
		{
			string[] files = Directory.GetFiles(Path.Combine("assets", "sounds"), "*.wav", SearchOption.AllDirectories);
			foreach (string file in files)
				AddSound(file.Split(Path.DirectorySeparatorChar).Skip(2).Aggregate((a, b) => Path.Combine(a, b)));
		}
		if (Directory.Exists(Path.Combine("assets", "music")))
		{
			string[] files = Directory.GetFiles(Path.Combine("assets", "music"), "*.wav", SearchOption.AllDirectories);
			foreach (string file in files)
				AddMusic(file.Split(Path.DirectorySeparatorChar).Skip(2).Aggregate((a, b) => Path.Combine(a, b)));
		}
	}

	void AddSound(string filename) =>
		Sounds.Add(filename.Replace(".wav", "").Replace('\\', '/'),
			SDL_mixer.Mix_LoadWAV(Path.Combine("assets", "sounds", filename)));

	void AddMusic(string filename) =>
		Music.Add(filename.Replace(".wav", "").Replace('\\', '/'),
			SDL_mixer.Mix_LoadMUS(Path.Combine("assets", "music", filename)));
}
