using Rokuro.Sound;

namespace Rokuro.Objects.Library.SoundPlayer;

public class PauseMusic : InteractableObject
{
	public override void OnClick() => SoundManager.PauseMusic();
}
