using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHitEffect : OnHitAttackEffect
{
	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		Debug.Log($"{source} is dealing {power} damage to {target}");
		target.TakeDamage(power);
	}
}
