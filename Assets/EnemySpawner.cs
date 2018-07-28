using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

	// BF = the idea is that this counterkeeps track of how many enemies there are and adds one for every 5 that are killed. as one is killed another is spawned.
	public int totalEnemiesAlive = 0, totalEnemiesKilled = 0, desiredEnemies = 5;
	[Range(2,20)]
	public int difficultyScale = 5;
	public List<Text> textObjects = new List<Text>(3);
	public GameObject enemy;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1)) desiredEnemies++;
		//desiredEnemies = (totalEnemiesKilled / difficultyScale) + difficultyScale;		// minimum of 5 enemies, for every 5 killed, add another.

		// BF - right now it doubles the enemies every time one dies.
		if(totalEnemiesAlive < desiredEnemies) {
			Instantiate(enemy, this.transform.position, this.transform.rotation);
			textObjects[0].text = "Desired Enemies = " + desiredEnemies;
			textObjects[1].text = "Total Enemies Alive = " + totalEnemiesAlive;
			textObjects[2].text = "Total Enemies Killed = " + totalEnemiesKilled;
		}
		
	}
}
