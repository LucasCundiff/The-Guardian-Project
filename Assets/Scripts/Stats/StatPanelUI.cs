using System.Collections.Generic;
using UnityEngine;

public class StatPanelUI : MonoBehaviour
{
	[SerializeField] Transform statsParent;

	private List<StatUI> statUIs = new List<StatUI>();

	public void Awake()
	{
		GetComponentsInChildren(statUIs);
	}

	public void InitializeStatUIs(CharacterStats userStats)
	{
		for (int i = 0; i < statUIs.Count; i++)
		{
			if (userStats.Stats.Count < i) return;

			if (userStats.Stats[i] != null)
			{
				statUIs[i].SetStat(userStats.Stats[i]);
			}
		}
	}
}
