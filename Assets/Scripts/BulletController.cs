using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	void Start() {
		Destroy(this.gameObject, 10);
	}

	void OnTriggerEnter(Collider other) {
		print("I've hit something!");
		if(other.gameObject.tag == "Enemy") {
			int enemyHealth = other.GetComponent<EnemyController>().health--;
			if(enemyHealth <= 0) {
				Destroy(other.gameObject);
				EnemySpawner.totalEnemies--;
			}
		}
		else if(other.gameObject.tag == "Spawner") {
			int spawnerHealth = other.GetComponent<EnemySpawner>().health--;
			if(spawnerHealth <= 0) {
				Destroy(other.gameObject);
			}
		}
		Destroy(this.gameObject);		// if it runs into anything
	}
}
