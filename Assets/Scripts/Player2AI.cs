using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player2AI : MonoBehaviour
{
	NavMeshAgent m_NavMesh;
	public GameObject player1;
	public Transform spawner;
	void Start()
	{
		// gross, but as far as I'm aware player 1 is always first in the heirarchy
		player1 = GameObject.FindGameObjectsWithTag("Player")[0];
		m_NavMesh = gameObject.AddComponent<NavMeshAgent>();
	}
	
	void Update()
	{
	}
}
