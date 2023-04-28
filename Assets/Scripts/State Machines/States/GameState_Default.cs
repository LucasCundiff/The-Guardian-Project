using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameState_Default : IState
{
	protected GameObject defaultStateUI;
	protected GameObject characterWindowUI;

	public GameState_Default(GameObject defaultUI)
	{
		defaultStateUI = defaultUI;
	}

	public void OnEnterState()
	{
		defaultStateUI.SetActive(true);
	}

	public void OnExitState()
	{
		defaultStateUI.SetActive(true);
	}

	public void TickState()
	{
	}


}
