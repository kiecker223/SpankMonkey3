using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
	public bool bIsInIFrames = false;
	private int m_Health = 1;
	public List<Action> thingsToDoWhenKilled = new List<Action>();

	void Start()
	{
		
	}

	void DecrementHealth()
	{
		if (!bIsInIFrames)
		{
			m_Health--;
		}
	}

	void IncrementHealth()
	{
		m_Health++;
	}
	
	public int Health { get { return m_Health; } }

	void Update()
	{
		if (m_Health == 0)
		{
			if (thingsToDoWhenKilled.Count > 0)
			{
				foreach (var thingToDo in thingsToDoWhenKilled)
				{
					thingToDo();
				}
			}
		}
	}
}
