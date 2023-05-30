using SDL2;

namespace Rokuro.Core;

public static class Logger
{
	public static void LogInfo(string message)
	{
		SDL.SDL_LogInfo((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);
	}

	public static void LogWarning(string message)
	{
		SDL.SDL_LogWarn((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);
	}

	public static void ThrowError(string message)
	{
		SDL.SDL_LogCritical((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);
		SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, "An error has occurred", message,
			IntPtr.Zero);
		throw new Exception(message);
	}

	internal static void ThrowSDLError(string message, ErrorSource source = ErrorSource.None) =>
		ThrowError(source switch {
			ErrorSource.None => message,
			ErrorSource.SDL => $"{message}\n[SDL_Error] {SDL.SDL_GetError()}",
			ErrorSource.IMG => $"{message}\n[IMG_Error] {SDL_image.IMG_GetError()}",
			ErrorSource.TTF => $"{message}\n[TTF_Error] {SDL_ttf.TTF_GetError()}",
			_ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
		});
}
