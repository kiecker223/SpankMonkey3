using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFTestShooter : MonoBehaviour {

	public Rigidbody bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0)) Shoot();
	}

	void Shoot() {
		Rigidbody rb = Instantiate(bullet, transform.position, transform.rotation);
		rb.AddRelativeForce(Vector3.forward * 5000);
	}
}
