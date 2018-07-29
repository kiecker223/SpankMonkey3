using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public int startingEnemies = 5;
	public int enemiesSpawned = 0;
	public static int totalEnemies = 0;
	public static float MAX_ENEMIES = 100;
	public static List<EnemySpawner> ActiveSpawners = new List<EnemySpawner>();
	
	public float interval = 10;

	public int health = 10;

	void Start() {
		ActiveSpawners.Add(this);
		startingEnemies = Random.Range(2,7);
		health = Random.Range(8,12);
	}
	
	public void SpawnEnemies() {
		for(int i = 0; i < Mathf.FloorToInt(Random.value * startingEnemies / 2) + startingEnemies / 2; i++) {
			if(totalEnemies < MAX_ENEMIES) {
				Debug.Log("Spawning enemy");
				Instantiate(enemy, transform.position, transform.rotation);
				totalEnemies++;
			}
		}
		startingEnemies++;
	}
}
