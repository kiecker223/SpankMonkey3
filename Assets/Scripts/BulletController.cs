using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour {

	public float bulletSpeed = 2500;
	public int damage = 1;

	public BFPlayerController refPlayer;

	void Start() {
		Destroy(this.gameObject, 10);
		//GetComponent<Collider>().isTrigger = true;		// to make it a trigger
		//Rigidbody rb = GetComponent<Rigidbody>();
		//rb.collisionDetectionMode = CollisionDetectionMode.Continuous;		// never forgets to collide with an enemy
		//rb.interpolation = RigidbodyInterpolation.Interpolate;
		//rb.AddRelativeForce(Vector3.forward * bulletSpeed);
	}

	void Update()
	{
		//transform.Translate(new Vector3(0f, 0f, bulletSpeed));
	}

	void OnCollisionEnter(Collision other) {
		print("I've hit something!");
		if(other.gameObject.tag == "Enemy") {
			var component = other.gameObject.GetComponent<EnemyController>();
			component.ApplyForce(transform.forward / 2);
			if((component.health -= damage) <= 0) {
				EnemySpawner.totalEnemies--;
				refPlayer.kills++;
				Destroy(other.gameObject);
			}
		}
		else if(other.gameObject.tag == "Spawner") {
			int spawnerHealth = (other.gameObject.GetComponent<EnemySpawner>().health -= damage);
			if(spawnerHealth <= 0) {
				Destroy(other.gameObject);
			}
		}
		Destroy(this.gameObject);		// if it runs into anything
	}
}
