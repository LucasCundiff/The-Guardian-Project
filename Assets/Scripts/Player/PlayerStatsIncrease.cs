using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsIncrease : MonoBehaviour
{
	[SerializeField] CharacterStats playerStats;
	[SerializeField] PlayerLevel levelController;
	[SerializeField] int resourceStatsUpgrade = 5;
	[SerializeField] int proficienyStatsUpgrade = 1;

	public void Start()
	{
		levelController.OnLevelUpEvent += AddStatPoints;
	}

	private void AddStatPoints()
	{
		for (int i = 0; i < 6; i++)
		{
			var pointIncrease = i < 3 ? resourceStatsUpgrade : proficienyStatsUpgrade;

			playerStats.Stats[i].BaseValue += pointIncrease;
		}
	}
}
