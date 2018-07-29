using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int checkPointIdx;
	public int checkPointValue;
	
	void Awake()
	{
		BFScoreKeeper.checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			BFScoreKeeper.checkPointIdx = checkPointIdx;
			BFScoreKeeper.score += checkPointValue;
			BFScoreKeeper.SaveData();
		}
	}
}
