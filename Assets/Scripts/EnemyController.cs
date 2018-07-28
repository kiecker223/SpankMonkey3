using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	public int id;
	Transform player;
	NavMeshAgent agent;
	public int health = 3;
	public string nameOfPlayer = "PlayerPrefabTemplate";

	// Use this for initialization
	void Start () {
		player = GameObject.Find(nameOfPlayer).transform;
		agent = this.GetComponent<NavMeshAgent>();
		this.gameObject.tag = "Enemy";
		health = Random.Range(1,4);
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = player.position;
		BFScoreKeeper.score += 20;
	}
}
