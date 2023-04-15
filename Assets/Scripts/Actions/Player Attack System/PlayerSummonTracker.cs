using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummonTracker : MonoBehaviour
{
	public static PlayerSummonTracker Instance;
	public List<BaseSummon> AllSummons { get; private set; } = new List<BaseSummon>();
	public Dictionary<string, List<BaseSummon>> CurrentSummons { get; private set; } = new Dictionary<string, List<BaseSummon>>();
	public List<Vector3> SummonOffsets = new List<Vector3>
	{
		new Vector3(0, 0, -2),
		new Vector3(2, 0, -2),
		new Vector3(-2, 0, -2),
		new Vector3(0, 0, -4),
		new Vector3(2, 0, -4),
		new Vector3(-2, 0, -4),
		new Vector3(0, 0, -6),
		new Vector3(2, 0, -6),
		new Vector3(-2, 0, -6),
		new Vector3(0, 0, -8),
		new Vector3(2, 0, -8),
		new Vector3(-2, 0, -8),
		new Vector3(0, 0, -10),
		new Vector3(2, 0, -10),
		new Vector3(-2, 0, -10),
	};

	protected int universalSummonMax = 15;

	public void Start()
	{
		if (Instance != null)
			Destroy(Instance);

		Instance = this;
	}

	public bool AtSummonLimit(string summonName)
	{
		if (AllSummons.Count >= universalSummonMax) return true;

		if (CurrentSummons.ContainsKey(summonName))
			if (CurrentSummons[summonName].Count < CurrentSummons[summonName][0].SummonLimit)
				return false;
			else
				return true;
		else
			return false;
	}

	public void AddSummon(BaseSummon summon)
	{
		if (CurrentSummons.ContainsKey(summon.SummonName))
		{
			var summonList = CurrentSummons[summon.SummonName];
			summonList.Add(summon);
			CurrentSummons[summon.SummonName] = summonList;
		}
		else
		{
			var newSummonList = new List<BaseSummon>() { summon };
			CurrentSummons.Add(summon.SummonName, newSummonList);
		}

		AllSummons.Add(summon);
	}

	public void RemoveSummon(BaseSummon summon)
	{
		if (CurrentSummons.ContainsKey(summon.SummonName))
		{
			var summonList = CurrentSummons[summon.SummonName];
			summonList.Remove(summon);

			if (summonList.Count <= 0)
				CurrentSummons.Remove(summon.SummonName);
			else
				CurrentSummons[summon.SummonName] = summonList;

			AllSummons.Remove(summon);
		}
	}
}
