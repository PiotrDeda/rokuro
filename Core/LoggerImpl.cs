using SDL2;

namespace Rokuro.Core;

class LoggerImpl
{
	public static LoggerImpl ActiveImpl { get; set; } = new();

	protected StreamWriter? LogFile { get; set; }

	public virtual void StartFileLogging(string filename)
	{
		Directory.CreateDirectory("logs");
		File.WriteAllText(Path.Combine("logs", filename), string.Empty);
		LogFile = new(Path.Combine("logs", filename), true);
		LogFile.AutoFlush = true;
	}

	public virtual void LogInfo(string message)
	{
		LogToFile(GetFormattedLog(message, LogLevel.Info));
		SDL.SDL_LogInfo((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);
	}

	public virtual void LogWarning(string message)
	{
		LogToFile(GetFormattedLog(message, LogLevel.Warning));
		SDL.SDL_LogWarn((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, message);
	}

	public virtual void ThrowError(Exception exception)
	{
		LogToFile(GetFormattedLog(exception.Message, LogLevel.Error));
		SDL.SDL_LogCritical((int)SDL.SDL_LogCategory.SDL_LOG_CATEGORY_APPLICATION, exception.Message);
		SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, "An error has occurred", exception.Message, IntPtr.Zero);
		throw exception;
	}

	protected virtual string GetFormattedLog(string message, LogLevel logLevel) => $"[{logLevel.ToString().ToUpper()}] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

	protected virtual void LogToFile(string message) => LogFile?.WriteLine(message);

	protected enum LogLevel
	{
		Info,
		Warning,
		Error
	}
}
