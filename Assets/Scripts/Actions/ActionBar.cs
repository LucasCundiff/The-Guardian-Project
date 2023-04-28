using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionBar : MonoBehaviour
{
	public ActionBarSetup ActionBarSetup;

	public int CurrentActionIndex;
	public List<ActionSlot> ActionSlots = new List<ActionSlot>();

	public Action<ActionSlot> OnActionSlotChanged;

	public void Start()
	{
		ActionBarSetup.OnActionBarSetupUpdated += UpdateActionBar;

		PlayerInputManager.Instance.PlayerInput.Player.Action1.performed += input => SetCurrentActionSlot(input, 0);
		PlayerInputManager.Instance.PlayerInput.Player.Action2.performed += input => SetCurrentActionSlot(input, 1);
		PlayerInputManager.Instance.PlayerInput.Player.Action3.performed += input => SetCurrentActionSlot(input, 2);
		PlayerInputManager.Instance.PlayerInput.Player.Action4.performed += input => SetCurrentActionSlot(input, 3);

		UpdateActionBar();
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
