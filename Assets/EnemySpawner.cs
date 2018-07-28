using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	// BF = the idea is that this counterkeeps track of how many enemies there are and adds one for every 5 that are killed. as one is killed another is spawned.
	static public int totalEnemiesAlive = 0, totalEnemiesKilled = 0, desiredEnemies = 5;
	[Range(2,20)]
	public int difficultyScale = 5;


	public GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		desiredEnemies = (totalEnemiesKilled / 5) + 5;		// minimum of 5 enemies, for every 5 killed, add another.
		print("desired enemies = " + 	desiredEnemies);
		if(totalEnemiesAlive < desiredEnemies) {
			Instantiate(enemy, this.transform.position, this.transform.rotation);
			totalEnemiesAlive++;
		}
		
	}
}
