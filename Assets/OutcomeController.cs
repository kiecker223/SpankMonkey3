using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutcomeController : MonoBehaviour {

	public bool win = false;
	public TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Win") == 0) {
			text.text = "You have been touched!\nFinal score = " + PlayerPrefs.GetFloat("Score");
		} else {
			text.text = "You have escaped! Final score = " + PlayerPrefs.GetFloat("Score");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
