using JetBrains.Annotations;
using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class PlaySound : InteractableObject
{
	public string? SoundName { get; [UsedImplicitly] set; }

	public override void OnClick()
	{
		if (SoundName != null)
			SoundManager.PlaySound(SoundName);
	}
}
