using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHitEffect : OnHitAttackEffect
{
	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		target.Heal(power);
	}
}
