using SDL2;

namespace Rokuro.Core;

public class LoggerImpl
{
	public static LoggerImpl ActiveImpl { get; set; } = new();

	public virtual void LogInfo(string message) =>
		SDL.SDL_LogInfo((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);

	public virtual void LogWarning(string message) =>
		SDL.SDL_LogWarn((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);

	public virtual void ThrowError(Exception exception)
	{
		SDL.SDL_LogCritical((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, exception.Message);
		SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, "An error has occurred",
			exception.Message, IntPtr.Zero);
		throw exception;
	}
}
