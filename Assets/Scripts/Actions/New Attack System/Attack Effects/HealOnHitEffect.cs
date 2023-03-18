using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHitEffect : OnHitAttackEffect
{
	[SerializeField] bool OnlyHealFriendly;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		if (OnlyHealFriendly)
		{
			var cTarget = (CharacterStats)target;
			if (cTarget != null && cTarget.Faction == source.Faction)
				target.Heal(power);
		}
		else
		{
			target.Heal(power);
		}
	}
}
