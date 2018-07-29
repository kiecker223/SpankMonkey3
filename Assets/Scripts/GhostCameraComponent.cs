using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCameraComponent : MonoBehaviour
{
	public GameObject p1;
	public GameObject p2;

	void Start()
	{
	}
	
	void Update()
	{
		if (p2.activeInHierarchy)
		{
			float distance = Vector3.Distance(p1.transform.position, p2.transform.position);
			Vector3 target = Vector3.Lerp(p1.transform.position, p2.transform.position, 0.5f);
			transform.position = target + new Vector3(0f, Mathf.Clamp(distance, 5f, 30f), Mathf.Clamp(-Mathf.Sqrt(distance), -30f, -4));
			transform.LookAt(target);
		}
		else
		{
			transform.position = p1.transform.position + new Vector3(0f, 15f, -3f);
			transform.LookAt(p1.transform.position);
		}
	}
}
