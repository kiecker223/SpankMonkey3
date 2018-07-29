using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType
{
	BuildingMaterial,
	Ammo,
	SpecialAmmo
}

public class LootComponent : MonoBehaviour
{
	public LootType lootType;
	public int ammount;
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			var playerComp = other.gameObject.GetComponent<BFPlayerController>();
			switch (lootType)
			{
				case LootType.Ammo:
					playerComp.gunComponent.ammo += ammount;
					break;
				case LootType.BuildingMaterial:
					playerComp.resources += ammount;
					break;
			}
			Destroy(gameObject);
		}
	}
}
