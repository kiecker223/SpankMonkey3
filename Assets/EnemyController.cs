using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;
	public EnemySpawner spawner;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerPrefabTemplate").transform;
		agent = this.GetComponent<NavMeshAgent>();
		this.gameObject.tag = "Enemy";
		spawner = GameObject.Find("EnemySpawnerTemplate").GetComponent<EnemySpawner>();
		spawner.totalEnemiesAlive++;
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = player.position;
	}
}
