using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarSetup : MonoBehaviour
{
	[SerializeField] CharacterWeapons characterWeapons;
	[SerializeField] PlayerSkills playerSkills;

	public Action OnActionBarSetupUpdated;

	public List<ActionSetupSlot> ActionSetupSlots = new List<ActionSetupSlot>();

	public void Awake()
	{
		characterWeapons.OnWeaponEquippedEvent += EventHelper;
		characterWeapons.OnWeaponUnequippedEvent += EventHelper;

		foreach (ActionSetupSlot setupSlot in ActionSetupSlots)
			setupSlot.OnSkillDroppedEvent += DropSkillOnActionSlot;

	}

	private void EventHelper(Item obj)
	{
		UpdateActionSlotsWeapons();
	}

	public void UpdateActionSlotsWeapons()
	{
		for (int i = 0; i < characterWeapons.ItemSlots.Count; i++)
		{
			ActionSetupSlots[i].SetWeapon((WeaponItem)characterWeapons.ItemSlots[i].Item);
		}

		OnActionBarSetupUpdated?.Invoke();
	}

	public void DropSkillOnActionSlot(ActionSetupSlot setupSlot)
	{
		if (!playerSkills || !playerSkills.DragSlot.Skill) return;

		setupSlot.SetSkill(playerSkills.DragSlot.Skill);
		OnActionBarSetupUpdated?.Invoke();
	}
}
