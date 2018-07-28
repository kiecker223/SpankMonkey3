using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int checkPointIdx;
	public int checkPointValue;

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
		}
	}

	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
}
