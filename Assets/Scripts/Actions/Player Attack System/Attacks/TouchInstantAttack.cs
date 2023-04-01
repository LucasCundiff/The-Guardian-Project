using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInstantAttack : BaseInstantAttack
{
	private void OnTriggerEnter(Collider other)
	{
		PossibleTargetHit(other);
	}
}
