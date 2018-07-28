using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BarrierController : MonoBehaviour {

	UnityEngine.AI.NavMeshObstacle obstacle;
	Rigidbody rb;
	public int health = 5;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody>();
		//rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		this.GetComponent<Renderer>().material.color = Color.black;
		Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "Enemy") {
			health--;
			if(health <= 0) {
				Destroy(this.gameObject);
			} else {
				Destroy(other.gameObject);
			}
		}
	}
}
