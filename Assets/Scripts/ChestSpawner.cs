﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
	public GameObject chest;
	
	void Start()
	{
		DayTimeCycle.SwitchToDaytimeActions.Add(SpawnChests);
	}

	void SpawnChests()
	{
		int spawnChance = Mathf.FloorToInt(Random.value * 3);
		if (spawnChance == 0 && transform.childCount == 0)
		{
			Instantiate(chest, transform);
		}
	}
}
