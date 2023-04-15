using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerSummon : BaseSummon
{
	[SerializeField] CharacterStats summonStats;
	[SerializeField] BaseSummonAI summonAI;

	public override void Summon(CharacterStats currentUser, BaseAttack attack)
	{
		summonStats.Faction = currentUser.Faction;
		summonStats.OnDeathEvent += Unsummon;
		summonAI.SetNewSummonCharacter(currentUser);

		var power = attack.DeterminePower();

		foreach (Stat stat in summonStats.Stats)
		{
			stat.BaseValue *= power;
		}

		SummonDuration *= power;
		base.Summon(currentUser, attack);
	}

	private IEnumerator UnsummonCoroutine()
	{
		yield return new WaitForSeconds(SummonDuration);
		Unsummon();
	}

	public override void Unsummon()
	{
		base.Unsummon();
	}
}
