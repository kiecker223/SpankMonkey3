﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunComponent : MonoBehaviour
{
	// This is represented as how many milliseconds until it fires again
	[Range(0,1000)]
	public float rateOfFire = 1;
	float rofTimer = 0;
	public AudioClip soundClip;
	[Range(1,5)]
	public float bulletSpeed = 2500;

	// probably set this in Start() as it will not work if we plan on using more than one type of gun
	public Transform gunTransform;

	public int totalAmmo;
	public int magazineSize;
	public int ammoLeftInMag;
	public float reloadTime;
	private float reloadTimer;
	public string weaponName;
	public Text infoText;

	void Start()
	{
	}

	void Reload()
	{

	}
	
	void Update()
	{
		float dtMilli = Time.deltaTime * 1000f;
		rofTimer -= dtMilli;
		reloadTimer -= dtMilli;

		if (ControllerMappings.GetButton(ControllerButtons.RightTrigger))
		{
			if(rofTimer <= 0) {
				GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				bullet.GetComponent<Renderer>().material.color = Color.blue;
				bullet.transform.position = gunTransform.position + Vector3.up * 3;
				bullet.transform.rotation = gunTransform.rotation;
				bullet.AddComponent<BulletController>().bulletSpeed = bulletSpeed;
				
				rofTimer = rateOfFire;                                  // this is the cooldown for the rate of fire of the gun.
				ammoLeftInMag--;
				if (ammoLeftInMag == 0)
				{
					Reload();
				}
			}
		}
		if (ControllerMappings.GetButtonDown(ControllerButtons.Button_X))
		{
			Reload();
		}
	//	infoText.text = 
	}
}
