using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AOEProjectile : ProjectileAmmo
{
	[SerializeField] AoeSummon summon;

	protected float summonDistance = 15f;
	protected bool unsummonFirstOnSuccess;

	protected override void EndProjectile()
	{
		SpawnAOE();
		base.EndProjectile();
	}

	private void SpawnAOE()
	{
		if (PlayerSummonTracker.Instance.AtSummonLimit(summon.SummonName))
			unsummonFirstOnSuccess = true;

		if (NavMesh.SamplePosition(transform.position, out var summonLocation, summonDistance, NavMesh.AllAreas))
		{
			var currentSummon = Instantiate(summon, summonLocation.position, Quaternion.identity, null).GetComponent<BaseSummon>();

			if (unsummonFirstOnSuccess)
				PlayerSummonTracker.Instance.CurrentSummons[summon.SummonName][0].Unsummon();

			currentSummon.Summon(user, currentAttack);
		}
	}
}
