using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
	// This is represented as how many milliseconds until it fires again
	public float rateOfFire;
	public AudioClip soundClip;

	// probably set this in Start() as it will not work if we plan on using more than one type of gun
	public Transform gunTransform;

	void Start()
	{
	}
	
	void Update()
	{
		if (ControllerMappings.GetButton(ControllerButtons.RightTrigger))
		{
			GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			bullet.GetComponent<Renderer>().material.color = Color.blue;
			bullet.transform.position = gunTransform.position;
			bullet.transform.rotation = gunTransform.rotation;
			bullet.AddComponent<BulletController>().bulletSpeed = 4f;
		}
	}
}
