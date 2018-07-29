using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rewired;

public class OutcomeController : MonoBehaviour {

	Player player;
	public int playerId = 0;

	public bool win = false, start;
	public TextMeshProUGUI text;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(playerId);

		if(PlayerPrefs.GetInt("Win") == 0) {
			text.text = "You have been touched!\nFinal score = " + PlayerPrefs.GetFloat("Score");
		} else {
			text.text = "You have escaped! Final score = " + PlayerPrefs.GetFloat("Score");
		}
	}

	void Update() {
		start = player.GetButtonDown("Start");
		if(start) Application.LoadLevel(0);
	}
}
