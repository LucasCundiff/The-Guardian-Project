using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInstantAttack : BaseInstantAttack
{
	private void OnTriggerEnter(Collider other)
	{
		var target = other.GetComponent<IDamageable>();
		if (target != null)
		{
			var attackPower = DeterminePower();

			foreach (OnHitAttackEffect hitEffect in onTargetHitEffects)
			{
				hitEffect.InitializeEffect(target, attackPower, CurrentUser);
			}
		}
	}
}
