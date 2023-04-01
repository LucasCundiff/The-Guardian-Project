using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effect/Modify Stat")]
public class ModifyStatEffect : ConsumableItemEffect
{
	[Range(10, 120)]
	[SerializeField] float EffectDuration;
	[Space]
	[Tooltip("0. = Health, 1 = Mana, 2 = Stamina, 3 = Melee Proficiency, 4 = Ranged Proficiency, 5 = Mana Proficiency, 6 = Stamina Proficiency, 7 = Attack Speed, 8 = Cooldown Reduction, 9 = Armor, 10 = Resistance, 11 = Regeneration, 12 = Movement Speed")]
	[SerializeField] int[] StatsModifyAmount = new int[13];

	public override void UseEffect(CharacterStats user)
	{
		if (!user) return;

		for (int i = 0; i < user.Stats.Count; i++)
		{
			if (StatsModifyAmount[i] != 0)
			{
				user.Stats[i].AddModifier(new StatModifier(StatsModifyAmount[i], StatModType.Flat, this));
				user.StartCoroutine(RemoveModifier(user.Stats[i]));
			}
		}
	}

	private IEnumerator RemoveModifier(Stat stat)
	{
		yield return new WaitForSeconds(EffectDuration);

		stat.RemoveAllModifiersFromSource(this);
	}
}
