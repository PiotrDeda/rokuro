using Rokuro.Graphics;
using Rokuro.MathUtils;
using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class ResumeMusic : InteractableObject
{
	public ResumeMusic(Vector2D position, Sprite sprite, Camera camera) : base(position, sprite, camera) {}

	public override void OnClick() => SoundManager.ResumeMusic();
}
