using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class StopMusic : InteractableObject
{
	public override void OnClick() => SoundManager.StopMusic();
}
