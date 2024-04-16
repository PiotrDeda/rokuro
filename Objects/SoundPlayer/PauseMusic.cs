using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class PauseMusic : InteractableObject
{
	public override void OnClick() => SoundManager.PauseMusic();
}
