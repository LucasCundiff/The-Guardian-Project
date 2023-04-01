using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsIncrease : MonoBehaviour
{
	[SerializeField] CharacterStats playerStats;
	[SerializeField] PlayerLevel levelController;
	[SerializeField] float[] statsIncreasePerLevel = new float[13];

	public void Start()
	{
		levelController.OnLevelUpEvent += AddStatPoints;
	}

	private void AddStatPoints()
	{
		if (!playerStats) return;

		for (int i = 0; i < playerStats.Stats.Count; i++)
		{
			if (statsIncreasePerLevel.Length < i) return;

			playerStats.Stats[i].BaseValue += statsIncreasePerLevel[i];
		}
	}
}
