using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class ResumeMusic : InteractableObject
{
	public override void OnClick() => SoundManager.ResumeMusic();
}
