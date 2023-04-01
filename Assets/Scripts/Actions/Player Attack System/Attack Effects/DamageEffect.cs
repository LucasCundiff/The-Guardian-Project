using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Effects/Damage")]
public class DamageEffect : OnHitAttackEffect
{
	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var cTarget = (CharacterStats)target;
		var damage = cTarget ? Mathf.Clamp(power - power * cTarget.Stats[9].CurrentValue * 0.002f, 1f, Mathf.Infinity) : power;
		target.TakeDamage(damage);
	}
}
