using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFBarrierController : MonoBehaviour {

	public GameObject barrier;
	public Transform playerModel, spawnPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")) CreateBarrier();
	}

	void CreateBarrier() {
		Instantiate(barrier, spawnPosition.position, playerModel.rotation);
	}
}
