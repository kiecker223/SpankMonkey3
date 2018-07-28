using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TestingControllers : MonoBehaviour {

	public int playerId = 0;

	public Player player;
	Vector3 moveVector, lookVector;
	bool fire, dash, drop;

	Rigidbody rb;

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
		moveVector.y = player.GetAxis("MoveVertical");
		lookVector.x = player.GetAxis("LookHorizontal");
		lookVector.y = player.GetAxis("LookVertical");

		fire = player.GetButtonDown("Fire");
		dash = player.GetButtonDown("Dash");
		drop = player.GetButtonDown("DropBarrier");
	}

	void ProcessInput() {
		rb.AddForce(moveVector);
		rb.transform.forward = lookVector;
	}
}
