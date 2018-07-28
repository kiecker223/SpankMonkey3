using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestComponent : MonoBehaviour
{
	public float ammoChance;
	public float materialChance;
	public float specialAmmoChance;
	public int totalLootDropCount;
	private bool hasSpawnedLoot = false;
	public Transform[] players;
	public GameObject activateLootBoxOptionText;
	public Material[] materials;

	bool PlayerIsNear()
	{
		return (Vector3.Distance(transform.position, players[0].position) < 3f || Vector3.Distance(transform.position, players[1].position) < 3f);
	}

	void Start()
	{
		players = new Transform[2];
		{
			var objs = GameObject.FindGameObjectsWithTag("Player");
			players[0] = objs[0].transform;
			players[1] = objs[1].transform;
		}
	}
	
	IEnumerator SpawnLoot()
	{
		totalLootDropCount = Random.Range(3, 12);
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
					ammount = Random.Range(30, 120);
					break;
				case 1:
					ammount = Random.Range(50, 300);
					break;
			}
			var loot = obj.AddComponent<LootComponent>();
			loot.lootType = (LootType)type;
			loot.ammount = ammount;
			obj.AddComponent<SphereCollider>().isTrigger = true;
			var rb = obj.AddComponent<Rigidbody>();
			rb.AddForce(new Vector3(0f, 4f, 0f), ForceMode.Impulse);
			obj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			obj.transform.position = transform.position + new Vector3(0f, 1f, 0f);
			yield return new WaitForSeconds(0.3f);
		}
	}

	void Update()
	{
		if (!hasSpawnedLoot)
		{
			if (PlayerIsNear())
			{
				activateLootBoxOptionText.gameObject.SetActive(true);
				if ((Rewired.ReInput.players.GetPlayer(0).GetButtonDown("Select") || Rewired.ReInput.players.GetPlayer(1).GetButtonDown("Select")))
				{
					StartCoroutine(SpawnLoot());
				}
			}
			else
			{
				activateLootBoxOptionText.gameObject.SetActive(false);
			}
		}
	}
}
