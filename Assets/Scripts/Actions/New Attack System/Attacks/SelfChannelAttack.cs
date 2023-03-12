using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfChannelAttack : BaseChannelAttack
{
	protected override void DuringChannel()
	{
		base.DuringChannel();
		var power = DeterminePower();

		foreach (OnHitAttackEffect onHitAttackEffect in OnTargetHitEffects)
		{
			onHitAttackEffect.InitializeEffect(CurrentUser, power, CurrentUser);
		}
	}
}
