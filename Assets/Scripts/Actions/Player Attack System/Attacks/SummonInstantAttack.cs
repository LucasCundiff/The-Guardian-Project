using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SummonInstantAttack : BaseInstantAttack
{
	[SerializeField] BaseSummon summon;
	[SerializeField] float summonDistance = 10f;
	[SerializeField] LayerMask spawnableLayers;

	protected bool unsummonFirstOnSuccess = false;

	protected override void StartAttack()
	{
		base.StartAttack();

		if (PlayerSummonTracker.Instance.AtSummonLimit(summon.SummonName))
			unsummonFirstOnSuccess = true;

		var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		var hitSomething = Physics.Raycast(ray, out var hit, summonDistance, spawnableLayers, QueryTriggerInteraction.Ignore);
		if (hitSomething && NavMesh.SamplePosition(hit.point, out var summonLocation, summonDistance, NavMesh.AllAreas))
		{
			var currentSummon = Instantiate(summon, summonLocation.position, Quaternion.identity, null).GetComponent<BaseSummon>();

			if (unsummonFirstOnSuccess)
				PlayerSummonTracker.Instance.CurrentSummons[summon.SummonName][0].Unsummon();

			currentSummon.Summon(User, this);
		}
		else
		{
			RefundAttackCost();
			unsummonFirstOnSuccess = false;
		}
	}
}
