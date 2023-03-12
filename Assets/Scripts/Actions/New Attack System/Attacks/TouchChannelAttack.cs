using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChannelAttack : BaseChannelAttack
{
	protected void OnTriggerStay(Collider other)
	{
		var target = other.GetComponent<IDamageable>();
		var power = DeterminePower();

		if (target != null)
		{
			foreach (OnHitAttackEffect onHitAttackEffect in OnTargetHitEffects)
			{
				onHitAttackEffect.InitializeEffect(target, power, CurrentUser);
			}
		}
	}
}
