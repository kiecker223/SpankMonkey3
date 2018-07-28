using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameTime
{
	public float CurrentTime;
	
	public bool IsDayTime
	{
		get
		{
			return CurrentTime <= 12;
		}
	}

	public bool IsNightTime
	{
		get
		{
			return CurrentTime > 12;
		}
	}
}

public static class DayTimeCycle
{
	private static GameTime time = new GameTime();
	public static List<System.Action> SwitchToDaytimeActions = new List<System.Action>();
	public static List<System.Action<float>> ToDaytimeTransitions = new List<System.Action<float>>();
	public static List<System.Action> SwitchToNightTimeActions = new List<System.Action>();
	public static List<System.Action<float>> ToNightTimeTransitions = new List<System.Action<float>>();
	public static GameTime GetTime()
	{
		return time;
	}

	public static void Initialize()
	{
		if (SwitchToDaytimeActions.Count > 0)
		{
			foreach (var action in SwitchToDaytimeActions)
			{
				action();
			}
		}
		time.CurrentTime = 0f;
	}

	static void CallDayTimeTransitions(float f)
	{
		if (ToDaytimeTransitions.Count > 0)
		{
			foreach (var action in ToDaytimeTransitions)
			{
				action(f);
			}
		}
	}

	static void CallNightTimeTransitions(float f)
	{
		if (ToNightTimeTransitions.Count > 0)
		{
			foreach (var action in ToNightTimeTransitions)
			{
				action(f);
			}
		}
	}

	static float secondsToFullTransition = 4f;

	public static void Tick(float dt)
	{
		if (Mathf.Floor(time.CurrentTime) == 12f)
		{
			if (secondsToFullTransition > 0f)
			{
				secondsToFullTransition -= dt;
				CallNightTimeTransitions(secondsToFullTransition / 4f);
				return;
			}
			if (SwitchToNightTimeActions.Count > 0)
			{
				foreach (var action in SwitchToNightTimeActions)
				{
					action();
				}
			}
			// force to skip
			time.CurrentTime = 13f;
			secondsToFullTransition = 4f;
		}
		if (time.CurrentTime > 24f - 1e-1f)
		{
			if (secondsToFullTransition > 0f)
			{
				secondsToFullTransition -= dt;
				CallDayTimeTransitions(secondsToFullTransition / 4f);
				return;
			}
			if (SwitchToDaytimeActions.Count > 0)
			{
				foreach (var action in SwitchToDaytimeActions)
				{
					action();
				}
			}
			time.CurrentTime = 0f;
			secondsToFullTransition = 4f;
		}
		time.CurrentTime += dt;
	}


}
