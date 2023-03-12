using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoreObject : MonoBehaviour
{
	public GameObject healthActiveIndicator;
	public Faction factionToHeal;
	public int healAmount;
	public float cooldownTime;
	public bool onCooldown;

	private void OnCollisionEnter(Collision collision)
	{
		if (onCooldown) return;

		var target = collision.gameObject.GetComponent<CharacterStats>();

		if (target?.Faction == factionToHeal)
		{
			target.Heal(healAmount);
			StartCoroutine(HealthRestoreCooldown());
		}
	}

	private IEnumerator HealthRestoreCooldown()
	{
		onCooldown = true;
		healthActiveIndicator.SetActive(false);

		yield return new WaitForSeconds(cooldownTime);

		onCooldown = false;
		healthActiveIndicator.SetActive(true);
	}
}
