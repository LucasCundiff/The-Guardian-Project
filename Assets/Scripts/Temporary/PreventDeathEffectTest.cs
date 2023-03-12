using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventDeathEffectTest : MonoBehaviour
{
	[SerializeField] CharacterStats user;

	void Start()
	{
		user.HealthChangedEvent += PreventDeath;	
	}

	private void PreventDeath(float currentHealth, float maxHealth)
	{
		if (currentHealth <= 0)
		{
			Debug.Log("Prevent Death triggerd");
			currentHealth = maxHealth;
		}
	}
}
