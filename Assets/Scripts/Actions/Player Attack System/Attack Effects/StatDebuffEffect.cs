using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Stat Debuff")]
public class StatDebuffEffect : OnHitAttackEffect
{
	[Range(0, 14)]
	[Tooltip("0. = Health, 1 = Mana, 2 = Stamina, 3 = Melee Proficiency, 4 = Ranged Proficiency, 5 = Mana Proficiency, 6 = Stamina Proficiency, 7 = Attack Speed, 8 = Cooldown Reduction, 9 = Armor, 10 = Resistance," +
	" 11 = Regeneration, 12 = Movement Speed, 13 = Damage Multipler, 14 = Damage Received")]
	public int StatToDebuff;

	[Range(0, 300)]
	public float EffectDuration;

	public bool IgnorePower = false;
	[Range(0, 1000)]
	[Tooltip("Only use if ignoring power")]
	public float DebuffAmount;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;

		if (cTarget)
		{
			var debuffAmount = IgnorePower ? DebuffAmount : power;
			debuffAmount = Mathf.Clamp(debuffAmount - debuffAmount * cTarget.Stats[10].CurrentValue * 0.002f, 1f, Mathf.Infinity);
			var modifier = new StatModifier(-debuffAmount, StatModType.Flat, source);
			cTarget.Stats[StatToDebuff].AddModifier(modifier);
			cTarget.StartCoroutine(RemoveModifier(cTarget, modifier));
		}
	}

	private IEnumerator RemoveModifier(CharacterStats character, StatModifier modifier)
	{
		yield return new WaitForSeconds(EffectDuration);

		character.Stats[StatToDebuff].RemoveModifier(modifier);
	}
}
