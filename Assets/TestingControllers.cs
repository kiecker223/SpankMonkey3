using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingControllers : MonoBehaviour {

	float p1RVert, p1RHori, p1LVert, p1LHori;
	float p2RVert, p2RHori, p2LVert, p2LHori;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey) print(p1LVert);
	}
}
