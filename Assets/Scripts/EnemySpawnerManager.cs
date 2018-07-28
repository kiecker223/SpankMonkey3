using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
	public GameObject Player;
	public float minDistance;
	public bool bCanSpawnEnemies = false;

	void Awake()
	{
		DayTimeCycle.SwitchToDaytimeActions.Add(DeactivateSpawners);
		DayTimeCycle.SwitchToNightTimeActions.Add(ActivateSpawners);
	}

	void Start()
	{
		
	}
	
	private List<EnemySpawner> FindSpawners(float distance)
	{
		List<EnemySpawner> result = new List<EnemySpawner>();
		List<EnemySpawner> gEnemySpawners = EnemySpawner.ActiveSpawners;
		float nearestSpawnerDist = distance;
		foreach (var spawner in gEnemySpawners)
		{
			float dist = Vector3.Distance(Player.transform.position, spawner.transform.position);
			if (dist < distance)
			{
				result.Add(spawner);
			}
		}
		if (result.Count > 3)
		{
			return FindSpawners(distance + 5f);
		}
		return result;
	}

	void KillAllEnemies()
	{
		foreach (var enemy in EnemySpawner.SpawnedEnemies)
		{
			Object.Destroy(enemy);
		}
		EnemySpawner.SpawnedEnemies.Clear();
	}

	void ActivateSpawners()
	{
		bCanSpawnEnemies = true;
	}

	void DeactivateSpawners()
	{
		bCanSpawnEnemies = false;
		KillAllEnemies();
	}

	void Update()
	{
		if (bCanSpawnEnemies)
		{
			var spawners = FindSpawners(minDistance);

			foreach (var spawner in spawners)
			{
				spawner.SpawnEnemies();
			}
		}
	}
}
