using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyKiller : MonoBehaviour {

	public EnemySpawner spawner;

	// Use this for initialization
	void Start () {
		print("I exist!");
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		print("Something's hit me!");
		if(other.gameObject.tag == "Enemy") {
			spawner.totalEnemiesAlive--;
			spawner.totalEnemiesKilled++;
			Destroy(other.gameObject);
		}
	}
}
