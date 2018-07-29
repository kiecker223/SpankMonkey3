using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Player2PlugAndPlayComponent : MonoBehaviour
{
	public GameObject player2;
	
	void Start()
	{
		player2.SetActive(false);
	}

	void Update()
	{
		if (ReInput.players.GetPlayer(1).GetButtonDown("StartButton"))
		{
			player2.SetActive(true);
		}
	}
	
}
