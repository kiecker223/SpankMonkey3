﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BFScoreKeeper
{
	public static int checkPointIdx;
	public static int score;
	public static GameObject lastCheckpoint;
	
	public static void SaveData()
	{
		string data = "$checkpointidx:" + checkPointIdx.ToString() + "\n$currentscore:" + score.ToString() + "\nlevel:unused";
		PlayerPrefs.SetString("SaveData", data);
	}

	public static void LoadData()
	{
		var loadedData = PlayerPrefs.GetString("SaveData", string.Empty);
		if (!string.IsNullOrEmpty(loadedData))
		{
			string[] lines = loadedData.Split(new char[] { '\n' });
			string checkPointIdxStr = lines[0].Split(':')[1];
			if (int.TryParse(checkPointIdxStr, out checkPointIdx))
			{
				//????
			}
		}
	}
}
