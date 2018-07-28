using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		print("I've hit something!");
		if(other.gameObject.tag == "Enemy") {
			int enemyHealth = other.GetComponent<EnemyController>().health--;
			if(enemyHealth <= 0) {
				Destroy(other.gameObject);
				EnemySpawner.totalEnemies--;
			}
		}
		Destroy(this.gameObject);
	}
}
