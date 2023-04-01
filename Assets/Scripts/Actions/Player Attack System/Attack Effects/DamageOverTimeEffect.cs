using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Damage Over Time")]
public class DamageOverTimeEffect : OnHitAttackEffect
{

	[Range(2, 12)]
	public int EffectDuration = 6;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;
		var damage = cTarget ? Mathf.Clamp(power - power * cTarget.Stats[9].CurrentValue * 0.002f, 1f, Mathf.Infinity) : power;
		var damagePerTick = damage / EffectDuration;

		source?.StartCoroutine(DamageOverTime(target, damagePerTick));
	}

	protected IEnumerator DamageOverTime(IDamageable target, float damagePerTick)
	{
		int currentDuration = 0;

		while (currentDuration < EffectDuration)
		{
			target?.TakeDamage(damagePerTick);
			currentDuration++;

			yield return new WaitForSeconds(1f);
		}
	}
}
