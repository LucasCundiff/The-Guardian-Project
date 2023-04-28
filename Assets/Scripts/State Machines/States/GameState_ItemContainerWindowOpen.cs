using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_ItemContainerWindowOpen : IState
{
	protected GameObject itemContainerWindowUI;

	public GameState_ItemContainerWindowOpen(GameObject itemContainerWindow)
	{
		itemContainerWindowUI = itemContainerWindow;
	}

	public void OnEnterState()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed += CloseItemContainerWindow;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void OnExitState()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed -= CloseItemContainerWindow;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void TickState()
	{
	}

	private void CloseItemContainerWindow(InputAction.CallbackContext obj)
	{
		itemContainerWindowUI.SetActive(false);
	}
}
