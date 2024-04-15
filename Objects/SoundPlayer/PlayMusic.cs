using JetBrains.Annotations;
using Rokuro.Graphics;
using Rokuro.MathUtils;
using Rokuro.Sound;

namespace Rokuro.Objects.SoundPlayer;

public class PlayMusic : InteractableObject
{
	public PlayMusic(Vector2D position, Sprite sprite, Camera camera) : base(position, sprite, camera) {}

	public string? TrackName { get; [UsedImplicitly] set; }

	public override void OnClick()
	{
		if (TrackName != null)
			SoundManager.PlayMusic(TrackName);
	}
}
