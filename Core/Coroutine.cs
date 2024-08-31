using System.Collections;

namespace Rokuro.Core;

public class Coroutine(IEnumerator coroutine)
{
	public IEnumerator CoroutineEnumerator { get; set; } = coroutine;
	public bool IsEnabled { get; set; } = true;
}
