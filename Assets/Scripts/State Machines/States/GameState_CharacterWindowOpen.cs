using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_CharacterWindowOpen : IState
{
	protected GameObject characterWindowUI;

	public GameState_CharacterWindowOpen(GameObject characterWindow)
	{
		characterWindowUI = characterWindow;
	}

	public void OnEnterState()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed += ToggleCharacterWindow;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0f;
	}

	public void OnExitState()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed -= ToggleCharacterWindow;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		Time.timeScale = 1f;
	}

	public void TickState()
	{
	}

	private void ToggleCharacterWindow(InputAction.CallbackContext obj)
	{
		characterWindowUI.SetActive(true);
	}
}
