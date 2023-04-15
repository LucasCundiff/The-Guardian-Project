using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Damage")]
public class DamageEffect : OnHitAttackEffect
{
	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;

		if (cTarget.Faction == source.Faction) return;

		var truePower = power * source.Stats[13].CurrentValue;
		var damage = cTarget ? Mathf.Clamp(truePower - truePower * cTarget.Stats[9].CurrentValue * 0.002f, 1f, Mathf.Infinity) : truePower;
		target.TakeDamage(damage);
	}
}
