using JetBrains.Annotations;
using Rokuro.Graphics;
using Rokuro.MathUtils;
using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class PlaySound : InteractableObject
{
	public string? SoundName { get; [UsedImplicitly] set; }
	
	public PlaySound(Vector2D position, Sprite sprite, Camera camera) : base(position, sprite, camera) {}

	public override void OnClick()
	{
		if (SoundName != null)
			SoundManager.PlaySound(SoundName);
	}
}
