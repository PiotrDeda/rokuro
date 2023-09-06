namespace Rokuro.Graphics;

public class AnimatedSprite : ISprite
{
	public AnimatedSprite(AnimatedSpriteTemplate template)
	{
		Template = template;
	}

	public int State { get; set; }

	AnimatedSpriteTemplate Template { get; }
	int CurrentFrame { get; set; }

	public int Width() => Template.Width;
	public int Height() => Template.Height;
	public IntPtr Texture() => Template.Texture;

	public IntPtr Clip() => Template.Clips[CurrentFrame++ / Template.Delay % Template.FrameCount
										   + State * Template.FrameCount];
}
