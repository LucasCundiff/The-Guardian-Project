using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionBar : MonoBehaviour
{
	public ActionBarSetup ActionBarSetup;
	public PlayerInputManager PlayerInputManager;

	public int CurrentActionIndex;
	public List<ActionSlot> ActionSlots = new List<ActionSlot>();

	public Action<ActionSlot> OnActionSlotChanged;

	public void Start()
	{
		ActionBarSetup.OnActionBarSetupUpdated += UpdateActionBar;

		PlayerInputManager.PlayerInput.Player.Action1.performed += input => SetCurrentActionSlot(input, 0);
		PlayerInputManager.PlayerInput.Player.Action2.performed += input => SetCurrentActionSlot(input, 1);
		PlayerInputManager.PlayerInput.Player.Action3.performed += input => SetCurrentActionSlot(input, 2);
		PlayerInputManager.PlayerInput.Player.Action4.performed += input => SetCurrentActionSlot(input, 3);
	}

	private void UpdateActionBar()
	{
		for (int i = 0; i < ActionBarSetup.ActionSetupSlots.Count; i++)
		{
			ActionSlots[i].SetWeapon(ActionBarSetup.ActionSetupSlots[i].CurrentWeapon);
			ActionSlots[i].SetSkill(ActionBarSetup.ActionSetupSlots[i].CurrentSkill);
		}
	}

	public void SetCurrentActionSlot(InputAction.CallbackContext input, int index)
	{
		ActionSlots[CurrentActionIndex].ToggleBorder(false);

		CurrentActionIndex = index;

		ActionSlots[CurrentActionIndex].ToggleBorder(true);
		OnActionSlotChanged?.Invoke(ActionSlots[CurrentActionIndex]);
	}
}
