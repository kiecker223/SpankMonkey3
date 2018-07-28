using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public int startingEnemies = 5;
	public static int totalEnemies = 0;
	public static float MAX_ENEMIES = 20;
	public static List<EnemySpawner> ActiveSpawners = new List<EnemySpawner>();
	public static List<GameObject> SpawnedEnemies = new List<GameObject>();
	
	public float interval = 10;

	public int health = 10;

	void Start() {
		ActiveSpawners.Add(this);
		startingEnemies = Random.Range(2,7);
		MAX_ENEMIES = Random.Range(15,40);
		health = Random.Range(8,12);
	}

	public void SpawnEnemies() {
		for(int i = 0; i < Random.Range(startingEnemies / 2, startingEnemies); i++) {
			if(totalEnemies < MAX_ENEMIES) {
				SpawnedEnemies.Add(Instantiate(enemy, transform.position, transform.rotation));
				totalEnemies++;
			}
		}
		startingEnemies++;
	}
}
