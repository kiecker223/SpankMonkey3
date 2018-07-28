using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour {

	public float bulletSpeed;

	void Start() {
		Destroy(this.gameObject, 10);
		GetComponent<Collider>().isTrigger = true;		// to make it a trigger
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.collisionDetectionMode = CollisionDetectionMode.Continuous;		// never forgets to collide with an enemy
		rb.interpolation = RigidbodyInterpolation.Interpolate;
	}

	void Update()
	{
		transform.Translate(new Vector3(0f, 0f, bulletSpeed));
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
		//Destroy(this.gameObject);		// if it runs into anything
	}
}
