using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Attack Effects/Displace")]
public class DisplaceEffect : OnHitAttackEffect
{
	[Tooltip("If not selected the target that was hit will be pulled instead of pushed")]
	[SerializeField] bool Push;

	public bool IgnorePower = false;
	[Range(1, 20)]
	[Tooltip("Only use if ignoring power")]
	public float ForceMultiplier;

	public override void InitializeEffect(IDamageable target, float power, CharacterStats source)
	{
		var viableTarget = (CharacterStats)target;
		var targetRb = viableTarget.GetComponent<Rigidbody>();
		var forceMultipler = IgnorePower ? ForceMultiplier : power;

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
