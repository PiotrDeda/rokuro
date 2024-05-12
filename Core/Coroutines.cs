using System.Collections;

namespace Rokuro.Core;

public class Coroutines
{
	List<IEnumerator> CoroutineList { get; } = new();

	public int Start(IEnumerator coroutine)
	{
		CoroutineList.Add(coroutine);
		return CoroutineList.Count - 1;
	}

	public void Stop(int index)
	{
		CoroutineList.RemoveAt(index);
	}

	public void StopAll()
	{
		CoroutineList.Clear();
	}

	internal void Execute()
	{
		for (int i = 0; i < CoroutineList.Count; i++)
		{
			if (!CoroutineList[i].MoveNext())
				CoroutineList.RemoveAt(i--);
		}
	}
}
