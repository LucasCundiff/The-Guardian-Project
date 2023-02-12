using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Faction")]
public class Faction : ScriptableObject
{
	/*
		Expand this later when more factions are added
	*/
	[SerializeField] List<Faction> AllyFactions = new List<Faction>();

	public bool IsAllyFaction(Faction faction)
	{
		return AllyFactions.Contains(faction);
	}
}
