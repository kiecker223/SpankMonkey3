using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public int id;
	Transform[] players = new Transform[2];
	NavMeshAgent agent;
	public int health = 3;
	public string nameOfPlayer = "Player1";
	public Material[] materials;
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
		StartCoroutine(SpawnLoot());
		StartCoroutine(InternalDestroyEnemy());
	}

	IEnumerator SpawnLoot()
	{
		int chance = Mathf.FloorToInt(Random.value * 25f);
		if (chance == 1)
		{
			int totalLootDropCount = Mathf.FloorToInt(Random.value * 5);
			for (int i = 0; i < totalLootDropCount; i++)
			{
				int type = Mathf.RoundToInt(Random.value);
				Debug.Log(type);
				GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				obj.GetComponent<Renderer>().material = materials[type];
				int ammount = 0;
				switch (type)
				{
					case 0:
						ammount = Random.Range(10, 50);
						break;
					case 1:
						ammount = Random.Range(1, 70);
						break;
				}
				var loot = obj.AddComponent<LootComponent>();
				loot.lootType = (LootType)type;
				loot.ammount = ammount;
				obj.AddComponent<SphereCollider>().isTrigger = true;
				var rb = obj.AddComponent<Rigidbody>();
				rb.AddForce(new Vector3(Random.Range(-0.3f, 0.3f), 9f, Random.Range(-0.3f, 0.3f)), ForceMode.Impulse);
				obj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				obj.transform.position = transform.position + new Vector3(0f, 1f, 0f);
				yield return new WaitForSeconds(0.3f);
			}
		}
	}

	IEnumerator InternalDestroyEnemy()
	{
		bNeedsUpdate = false;
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start()
	{
		var foundPlayers = GameObject.FindGameObjectsWithTag("Player");
		players[0] = foundPlayers[0].transform;
		if (foundPlayers.Length > 1)
			players[1] = foundPlayers[1].transform;
		agent = this.GetComponent<NavMeshAgent>();
		this.gameObject.tag = "Enemy";
		health = Random.Range(1, 4);
	}

	// Update is called once per frame
	void Update()
	{
		if (bNeedsUpdate) { agent.destination = FindNearestPlayer(); }
	}
}


