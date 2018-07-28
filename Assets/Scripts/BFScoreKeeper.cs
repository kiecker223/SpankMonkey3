using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFScoreKeeper : MonoBehaviour {

	public float score;
	public Transform start, player;

	// Use this for initialization
	void Start () {
		player = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		score = Vector3.Distance(start.position, player.position);
		
	}
}
