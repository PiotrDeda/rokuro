using SDL2;

namespace Rokuro.Core;

public static class Logger
{
	public static void LogInfo(string message) => LoggerImpl.ActiveImpl.LogInfo(message);
	public static void LogWarning(string message) => LoggerImpl.ActiveImpl.LogWarning(message);
	public static void ThrowError(Exception exception) => LoggerImpl.ActiveImpl.ThrowError(exception);
	public static void ThrowError(string message) => ThrowError(new Exception(message));

	internal static void ThrowSDLError(string message, ErrorSource source = ErrorSource.None) =>
		ThrowError(source switch {
			ErrorSource.None => message,
			ErrorSource.SDL => $"{message}\n[SDL_Error] {SDL.SDL_GetError()}",
			ErrorSource.IMG => $"{message}\n[IMG_Error] {SDL_image.IMG_GetError()}",
			ErrorSource.TTF => $"{message}\n[TTF_Error] {SDL_ttf.TTF_GetError()}",
			_ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
		});
}
