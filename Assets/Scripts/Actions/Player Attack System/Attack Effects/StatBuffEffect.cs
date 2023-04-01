using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Stat Buff")]
public class StatBuffEffect : OnHitAttackEffect
{
	[Range(0, 14)]
	[Tooltip("0. = Health, 1 = Mana, 2 = Stamina, 3 = Melee Proficiency, 4 = Ranged Proficiency, 5 = Mana Proficiency, 6 = Stamina Proficiency, 7 = Attack Speed, 8 = Cooldown Reduction, 9 = Armor, 10 = Resistance," +
	" 11 = Regeneration, 12 = Movement Speed, 13 = Damage Multipler, 14 = Damage Received")]
	public int StatToBuff;
	[Range(0, 300)]
	public float EffectDuration;

	public bool IgnorePower = false;
	[Range(0, 1000)]
	[Tooltip("Only use if ignoring power")]
	public float BuffAmount;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		if (source)
		{
			var buffAmount = IgnorePower ? BuffAmount : power;
			var modifier = new StatModifier(buffAmount, StatModType.Flat, source);
			source.Stats[StatToBuff].AddModifier(modifier);
			source.StartCoroutine(RemoveModifier(source, modifier));
		}
	}

	private IEnumerator RemoveModifier(CharacterStats character, StatModifier modifier)
	{
		yield return new WaitForSeconds(EffectDuration);

		character.Stats[StatToBuff].RemoveModifier(modifier);
	}
}
 