using System.Collections;
using UnityEngine;

public class HolyEffect : OnHitAttackEffect
{
	[Range(.05f, 0.2f)]
	public float DrainArmorPercent;

	[Range(10, 120)]
	public int EffectDuration;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		CharacterStats targetCast = (CharacterStats)target;

		if (target != null)
		{
			float ArmorSteal = targetCast.Stats[7].BaseValue * DrainArmorPercent;
			if (ArmorSteal > 0)
			{
				StatModifier statModifier = new StatModifier(-ArmorSteal, StatModType.Flat, source);

				targetCast.Stats[7].AddModifier(statModifier);
				targetCast.StartCoroutine(RemoveDebuff(targetCast.Stats[7], statModifier));

				source.HealthShield += ArmorSteal;
			}
		}
	}

	private IEnumerator RemoveDebuff(Stat stat, StatModifier statModifier)
	{
		yield return new WaitForSeconds(EffectDuration);

		stat.RemoveModifier(statModifier);
	}
}
