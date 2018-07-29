using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MainMenuControls : MonoBehaviour {

	public int playerId = 0;
	Player player;
	bool start, select;
	public Animator anim;

	// Use this for initialization
	void Start () {
		player = ReInput.players.GetPlayer(playerId);
		anim.SetBool("MainMenu", true);
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		ProcessInput();
	}

	void GetInput() {
		start = player.GetButtonDown("Start");
		select = player.GetButtonDown("Select");
	}

	void ProcessInput() {
		if(start) Application.LoadLevel(1);
		if(select) Application.LoadLevel(0);
		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}
}
