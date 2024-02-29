using Rokuro.MathUtils;

namespace Rokuro.Graphics;

public static class Drawer
{
	public static int BaseWidth
	{
		get => DrawerImpl.ActiveImpl.BaseWidth;
		set => DrawerImpl.ActiveImpl.BaseWidth = value;
	}

	public static int BaseHeight
	{
		get => DrawerImpl.ActiveImpl.BaseHeight;
		set => DrawerImpl.ActiveImpl.BaseHeight = value;
	}

	public static float WidthMultiplier
	{
		get => DrawerImpl.ActiveImpl.WidthMultiplier;
		set => DrawerImpl.ActiveImpl.WidthMultiplier = value;
	}

	public static float HeightMultiplier
	{
		get => DrawerImpl.ActiveImpl.HeightMultiplier;
		set => DrawerImpl.ActiveImpl.HeightMultiplier = value;
	}

	public static int WidthOffset
	{
		get => DrawerImpl.ActiveImpl.WidthOffset;
		set => DrawerImpl.ActiveImpl.WidthOffset = value;
	}

	public static int HeightOffset
	{
		get => DrawerImpl.ActiveImpl.HeightOffset;
		set => DrawerImpl.ActiveImpl.HeightOffset = value;
	}

	public static Color BgColor
	{
		get => DrawerImpl.ActiveImpl.BgColor;
		set => DrawerImpl.ActiveImpl.BgColor = value;
	}

	public static void Draw(ISprite sprite, Vector2D position, float scale) =>
		DrawerImpl.ActiveImpl.Draw(sprite, position, scale);

	internal static (IntPtr texture, int width, int height) GetTextTexture(string text, Font font, Color color) =>
		DrawerImpl.ActiveImpl.GetTextTexture(text, font, color);

	internal static void RenderStart() => DrawerImpl.ActiveImpl.RenderStart();
	internal static void RenderComplete() => DrawerImpl.ActiveImpl.RenderComplete();
}
