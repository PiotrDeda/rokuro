namespace Rokuro.Inputs;

public class KeyDownEventArgs(KeyEvent keyEvent) : EventArgs
{
	public KeyEvent KeyEvent { get; } = keyEvent;
}
