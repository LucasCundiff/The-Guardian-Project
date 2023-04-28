using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	[SerializeField] List<GameObject> pauseWindows;
	[SerializeField] GameObject defaultGameUI;

	public static GameStateManager Instance;
	public IState CurrentState;

	protected StateMachine stateMachine = new StateMachine();

	public void Update()
	{
		stateMachine.TickStateMachine();
		CurrentState = stateMachine.CurrentState;
	}

	public void Awake()
	{
		if (Instance != null)
			Destroy(Instance);

		Instance = this;

		var defaultState = new GameState_Default(defaultGameUI);
		var pausedState = new GameState_Paused(pauseWindows);


		Func<bool> PauseWindowIsOpen = () => { return pauseWindows.Any(x => x.activeSelf); };
		Func<bool> PauseWindowIsClosed = () => { return pauseWindows.All(x => x.activeSelf == false); };

		//stateMachine.AddStateTransition(defaultState, itemContainerWindowOpenState, ItemContainerOpen);
		//stateMachine.AddStateTransition(itemContainerWindowOpenState, defaultState, ItemContainerClosed);
		//stateMachine.AddStateTransition(defaultState, characterWindowOpenState, CharacterWindowOpen);
		//stateMachine.AddStateTransition(characterWindowOpenState, defaultState, CharacterWindowClosed);

		stateMachine.AddStateTransition(defaultState, pausedState, PauseWindowIsOpen);
		stateMachine.AddStateTransition(pausedState, defaultState, PauseWindowIsClosed);

		stateMachine.SetState(defaultState);
	}
}
