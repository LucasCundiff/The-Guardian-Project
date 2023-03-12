using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalEffect : OnHitAttackEffect
{
	[Range(.1f, 2f)]
	public float DamageMultipler;

	public override void InitializeEffect(IDamageable target, float damage, CharacterStats source)
	{
		if (target != null)
			target.TakeDamage(damage * DamageMultipler);
	}
}
