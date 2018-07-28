using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFPlayerController : MonoBehaviour {

	Rigidbody rb;
	public float speed = 50;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		rb.AddForce(horizontal * speed, 0, vertical * speed) ;
	}
}
