using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSummon : MonoBehaviour
{
	[SerializeField] CharacterStats stats;
	[SerializeField] BaseSummonAI summonAI;
	[SerializeField] float summonDuration;

	internal void Summon(CharacterStats currentUser, float power)
	{
		stats.Faction = currentUser.Faction;
		stats.OnDeathEvent += Unsummon;

		summonAI.SetNewSummonCharacter(currentUser);

		foreach (Stat stat in stats.Stats)
		{
			stat.BaseValue *= power;
		}

		summonDuration *= power;
		StartCoroutine(UnsummonCoroutine());
	}

	private IEnumerator UnsummonCoroutine()
	{
		yield return new WaitForSeconds(summonDuration);
		Unsummon();
	}

	protected virtual void Unsummon() => Destroy(gameObject);
}
