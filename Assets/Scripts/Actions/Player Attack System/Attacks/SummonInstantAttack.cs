using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonInstantAttack : BaseInstantAttack
{
	[SerializeField] GameObject summon;
	[Range(3f, 10f)]
	[SerializeField] float summonRadius;

	protected int itterationCap = 10;
	protected int currentItterationCount = 0;

	protected override void StartAttack()
	{
		base.StartAttack();

		currentItterationCount = 0;
		NavMeshHit summonLocation;

		while (!NavMesh.SamplePosition(transform.position + GetRandomSpawnPosition(), out summonLocation, summonRadius, NavMesh.AllAreas))
		{
			currentItterationCount++;

			if (currentItterationCount > itterationCap)
				return;
		}

		var currentSummon = Instantiate(summon, summonLocation.position, Quaternion.identity, null).GetComponent<BaseSummon>();
		var summonPower = DeterminePower();
		currentSummon.Summon(User, summonPower);
	}

	private Vector3 GetRandomSpawnPosition()
	{
		var randomRadius = Random.Range(3f, summonRadius);
		var randomPosition = Random.insideUnitSphere * randomRadius;

		return randomPosition;
	}
}
