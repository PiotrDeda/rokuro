using JetBrains.Annotations;
using Rokuro.Sound;

namespace Rokuro.Objects.Library.SoundPlayer;

public class PlayMusic : InteractableObject
{
	public string? TrackName { get; [UsedImplicitly] set; }

	public override void OnClick()
	{
		if (TrackName != null)
			SoundManager.PlayMusic(TrackName);
	}
}
