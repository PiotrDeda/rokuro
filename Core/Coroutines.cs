using System.Collections;

namespace Rokuro.Core;

public class Coroutines
{
	List<Coroutine> CoroutineList { get; } = new();

	public int Start(IEnumerator coroutine)
	{
		CoroutineList.Add(new(coroutine));
		return CoroutineList.Count - 1;
	}

	public void Stop(int index)
	{
		CoroutineList[index].IsEnabled = false;
	}

	public void StopAll()
	{
		foreach (Coroutine coroutine in CoroutineList)
			coroutine.IsEnabled = false;
	}

	internal void Execute()
	{
		for (int i = 0; i < CoroutineList.Count; i++)
		{
			if (CoroutineList[i].IsEnabled && !CoroutineList[i].CoroutineEnumerator.MoveNext())
				Stop(i--);
		}
	}
}
