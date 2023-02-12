using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridEnemyAI : EnemyAI
{
	[SerializeField] float meleeAttackDistance;
	[SerializeField] List<EnemyAttackTableData> meleeAttackTable = new List<EnemyAttackTableData>();

	protected bool useMelee;

	protected override void Start()
	{
		base.Start();

		foreach (EnemyAttackTableData tableData in meleeAttackTable)
		{
			tableData.Attack.InitializeAttack(user);
		}
	}

	protected override bool AttackAnyTransition()
	{
		var currentDistance = DetermineDistance();

		if (currentDistance <= targetAttackDistance)
		{
			useMelee = currentDistance <= meleeAttackDistance ? true : false;
			return true;
		}

		return false;
	}

	protected override EnemyAttack GetNextAttack()
	{
		var usedList = useMelee ? meleeAttackTable : attackTable;

		return WeightTable.RunEnemyAttackWeightTable(usedList);
	}
}
