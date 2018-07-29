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
	bool bNeedsUpdate = true;

	public Vector3 FindNearestPlayer()
	{
		Vector3 p1 = players[0].transform.position;
		if (!players[1])
		{
			return p1;
		}
		Vector3 p2 = players[1].transform.position;

		if (Vector3.Distance(transform.position, p1) < Vector3.Distance(transform.position, p2))
		{
			return p1;
		}
		return p2;
	}

	public void ApplyForce(Vector3 direction)
	{
		transform.Translate(direction);
	}

	public void DestroyEnemy()
	{
		StartCoroutine(InternalDestroyEnemy());
	}

	IEnumerator InternalDestroyEnemy()
	{
		bNeedsUpdate = false;
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		var foundPlayers = GameObject.FindGameObjectsWithTag("Player");
		players[0] = foundPlayers[0].transform;
		if (foundPlayers.Length > 1)
			players[1] = foundPlayers[1].transform;
		agent = this.GetComponent<NavMeshAgent>();
		this.gameObject.tag = "Enemy";
		health = Random.Range(1,4);
	}
	
	// Update is called once per frame
	void Update () {
		if (bNeedsUpdate)
			agent.destination = FindNearestPlayer();
	}
}
