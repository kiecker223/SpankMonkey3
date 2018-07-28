using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class BFPlayerController : MonoBehaviour {

	public int playerId = 0;

	Rigidbody rb;
	public float speed = 50;
	public Player player;
	Vector3 moveVector, lookVector;
	bool fire, dash, drop;
	public GameObject playerObj;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(playerId);
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
	}

	void GetInput() {
		moveVector.x = player.GetAxis("MoveHorizontal");
		moveVector.z = player.GetAxis("MoveVertical");
		lookVector.x = player.GetAxis("LookHorizontal");
		lookVector.z = player.GetAxis("LookVertical");

		fire = player.GetButtonDown("Fire");
		dash = player.GetButtonDown("Dash");
		drop = player.GetButtonDown("DropBarrier");
	}

	void ProcessInput() {
		rb.AddForce(moveVector * speed);
		rb.transform.forward = lookVector;
		//playerObj.transform.forward = lookVector;

		if(fire) print("Pow!");
		if(dash) print("Swoosh");
		if(drop) print("Barrier Dropped!");
	}
}
