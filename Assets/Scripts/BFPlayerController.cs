using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFPlayerController : MonoBehaviour {

	public string playerName = "Jupiter";		// jupiter = player1, saturn = player2

	Rigidbody rb;
	public float speed = 50;
	Vector3 direction;
	public GameObject playerObj;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis(playerName + " Horizontal");
		float vertical = Input.GetAxis(playerName + " Vertical");
		rb.AddForce(horizontal * speed, 0, vertical * speed);


		Vector2 controllerDir = ControllerMappings.GetRightStickDirection();
		if (controllerDir.magnitude > 1e-1f)
		{
			direction = new Vector3(controllerDir.x, 0f, -controllerDir.y).normalized;
			playerObj.transform.forward = direction;
		}
	}
}
