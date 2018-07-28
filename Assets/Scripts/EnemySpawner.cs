using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public int startingEnemies = 5;
	public static int totalEnemies = 0;
	public static int MAX_ENEMIES = 20;

	float timer = 0;
	public float interval = 10;

	void Start() {
		startingEnemies = Random.Range(2,7);
		MAX_ENEMIES = Random.Range(15,40);
	}

	void Update() {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			SpawnEnemies();
			timer += interval;
		}
	}

	void SpawnEnemies() {
		for(int i = 0; i < Random.Range(startingEnemies / 2, startingEnemies); i++) {
			if(totalEnemies < MAX_ENEMIES) {
				Instantiate(enemy, this.transform.position, this.transform.rotation);
				totalEnemies++;
			}
		}
		startingEnemies++;
	}
}
