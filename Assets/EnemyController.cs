using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("PlayerPrefabTemplate").transform;
		agent = this.GetComponent<NavMeshAgent>();
		this.gameObject.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = player.position;
	}
}
