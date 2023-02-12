using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyAttackTableData
{
	public float Weight;
	public EnemyAttack Attack;
}

public class WeightTable : MonoBehaviour
{
	/*
	static public WeightTable Instance;

	public void Awake()
	{
		if (Instance != null)
			Destroy(Instance);

		Instance = this;
	}
	*/

	public static EnemyAttack RunEnemyAttackWeightTable(List<EnemyAttackTableData> tableData)
	{
		var totalWeight = 0f;

		foreach (EnemyAttackTableData table in tableData)
			totalWeight += table.Weight;

		var randomNumber = Random.Range(0, totalWeight);

		foreach (EnemyAttackTableData table in tableData)
		{
			if (randomNumber <= table.Weight)
				return table.Attack;
			else
				randomNumber -= table.Weight;
		}

		return tableData[tableData.Count - 1].Attack;
	}
}
