using Rokuro.Sound;

namespace Rokuro.Objects.Library.SoundPlayer;

public class StopMusic : InteractableObject
{
	public override void OnClick() => SoundManager.StopMusic();
}
