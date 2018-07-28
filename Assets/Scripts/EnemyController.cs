using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	public int id;
	Transform[] players = new Transform[2];
	NavMeshAgent agent;
	public int health = 3;
	public string nameOfPlayer = "Player1";

	public Vector3 FindNearestPlayer()
	{
		Vector3 p1 = players[0].transform.position;
		Vector3 p2 = players[1].transform.position;

		if (Vector3.Distance(transform.position, p1) < Vector3.Distance(transform.position, p2))
		{
			return p1;
		}
		return p2;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Barrier")
		{
			if (--other.gameObject.GetComponent<BarrierController>().health == 0) { Destroy(other.gameObject); }
		}
	}

	// Use this for initialization
	void Start () {
		var foundPlayers = GameObject.FindGameObjectsWithTag("Player");
		players[0] = foundPlayers[0].transform;
		players[1] = foundPlayers[1].transform;
		agent = this.GetComponent<NavMeshAgent>();
		this.gameObject.tag = "Enemy";
		health = Random.Range(1,4);
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = FindNearestPlayer();
	}
}
