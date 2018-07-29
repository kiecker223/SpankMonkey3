using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
	public GameObject playerCam;
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
		var gEnemySpawners = GameObject.FindGameObjectsWithTag("Spawner");
		float nearestSpawnerDist = distance;
		foreach (var obj in gEnemySpawners)
		{
			var spawner = obj.GetComponent<EnemySpawner>();
			if (!spawner) continue;
			float dist = Vector3.Distance(new Vector3(playerCam.transform.position.x, 0f, playerCam.transform.position.z), spawner.transform.position);
			if (dist < distance)
			{
				result.Add(spawner);
			}
		}
		if (result.Count < 3)
		{
			return FindSpawners(distance + 5f);
		}
		return result;
	}

	void KillAllEnemies()
	{
		var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (var enemy in enemies)
		{
			enemy.GetComponent<EnemyController>().DestroyEnemy();
		}
	}

	void ActivateSpawners()
	{
		bCanSpawnEnemies = true;
	}

	void DeactivateSpawners()
	{
		bCanSpawnEnemies = false;
		EnemySpawner.totalEnemies = 0;
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
