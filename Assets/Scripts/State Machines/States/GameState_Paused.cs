using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_Paused : IState
{
	protected List<GameObject> pauseWindows = new List<GameObject>();

	public GameState_Paused(List<GameObject> windows)
	{
		pauseWindows = windows;
	}

	public void OnEnterState()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed += DisablePauseWindows;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Time.timeScale = 0f;
	}

	public void OnExitState()
	{
		PlayerInputManager.Instance.PlayerInput.UI.WindowToggle.performed -= DisablePauseWindows;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1f;
	}

	public void TickState()
	{

	}

	private void DisablePauseWindows(InputAction.CallbackContext context)
	{
		foreach (GameObject window in pauseWindows)
		{
			window.SetActive(false);
		}
	}
}
