using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("TestPlayerTemplate").transform;
		agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = player.position;
	}
}
