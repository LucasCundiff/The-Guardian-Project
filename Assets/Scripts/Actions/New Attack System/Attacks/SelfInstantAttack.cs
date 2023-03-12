using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfInstantAttack : BaseInstantAttack
{
	protected override void StartAttack()
	{
		base.StartAttack();
		var power = DeterminePower();

		foreach (OnHitAttackEffect hitAttackEffect in OnTargetHitEffects)
		{
			hitAttackEffect.InitializeEffect(CurrentUser, power, CurrentUser);
		}
	}
}
