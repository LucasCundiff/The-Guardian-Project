using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PushPullEffect : OnHitAttackEffect
{
	[SerializeField] float forceMultipler = 2f;
	[SerializeField] bool Push;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var viableTarget = (CharacterStats)target;
		var targetRb = viableTarget.GetComponent<Rigidbody>();

		if (targetRb != null)
		{
			var knockbackDirection = source.transform.position - viableTarget.transform.position;
			knockbackDirection = source.transform.InverseTransformVector(knockbackDirection);
			knockbackDirection.Normalize();
			knockbackDirection *= forceMultipler;

			var agent = viableTarget.GetComponent<NavMeshAgent>();
			if (agent)
				agent.enabled = false;
			var usedForce = Push ? -knockbackDirection : knockbackDirection;
			targetRb.AddForce(usedForce, ForceMode.Impulse);
			source.StartCoroutine(EndKnockback(targetRb));

			if (agent)
				agent.enabled = true;
		}
	}

	private IEnumerator EndKnockback(Rigidbody targetRb)
	{
		yield return new WaitForSeconds(0.5f);
		targetRb.velocity = Vector3.zero;
	}
}
