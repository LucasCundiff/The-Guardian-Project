using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChannelAttack : BaseChannelAttack
{
	protected void OnTriggerStay(Collider other)
	{
		PossibleTargetHit(other);
	}
}
