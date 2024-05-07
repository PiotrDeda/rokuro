using Rokuro.Sound;

namespace Rokuro.Objects.Library.SoundPlayer;

public class ResumeMusic : InteractableObject
{
	public override void OnClick() => SoundManager.ResumeMusic();
}
