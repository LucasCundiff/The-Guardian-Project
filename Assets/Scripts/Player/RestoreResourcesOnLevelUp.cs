using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreResourcesOnLevelUp : MonoBehaviour
{
	[SerializeField] PlayerLevel playerLevel;
	[SerializeField] CharacterStats CharacterStats;

	public void Start()
	{
		playerLevel.OnLevelUpEvent += RestoreResources;
	}

	private void RestoreResources()
	{
		CharacterStats.CurrentHealth = CharacterStats.MaxHealth;
		CharacterStats.CurrentMana = CharacterStats.MaxMana;
		CharacterStats.CurrentStamina = CharacterStats.MaxStamina;
	}
}
