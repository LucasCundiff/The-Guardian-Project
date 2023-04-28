using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
	public Dictionary<Type, List<Transition>> transitions { get; private set; } = new Dictionary<Type, List<Transition>>();
	public List<Transition> currentTransitions { get; private set; } = new List<Transition>();
	public List<Transition> anyTransitions { get; private set; } = new List<Transition>();
	public List<Transition> emptyTranstions { get; private set; } = new List<Transition>();

	public Action<IState> OnStateChangedEvent;

	private IState _currentState;
	public IState CurrentState
	{
		get { return _currentState; }
		private set
		{
			_currentState = value;
			OnStateChangedEvent?.Invoke(_currentState);
			Debug.Log("Current state is " + _currentState);
		}
	}

	public void TickStateMachine()
	{
		var transition = GetNextTransition();

		if (transition != null)
			SetState(transition.TargetState);

		CurrentState?.TickState();
	}

	public void SetState(IState newState)
	{
		if (CurrentState == newState) return;

		CurrentState?.OnExitState();
		CurrentState = newState;
		CurrentState?.OnEnterState();
		var stateKey = CurrentState.GetType();

		currentTransitions = transitions[stateKey] != null ? transitions[stateKey] : emptyTranstions;
	}

	public void AddStateTransition(IState sourceState, IState targetState, Func<bool> condition)
	{
		var stateKey = sourceState.GetType();
		if (!transitions.ContainsKey(stateKey))
			transitions.Add(stateKey, new List<Transition>());

		transitions[stateKey].Add(new Transition(targetState, condition));
	}

	public void AddAnyTransition(IState targetState, Func<bool> condition)
	{
		anyTransitions.Add(new Transition(targetState, condition));
	}

	public Transition GetNextTransition()
	{
		foreach (Transition transition in anyTransitions)
		{
			if (transition.TransitionCondtion())
				return transition;
		}

		foreach (Transition transition in currentTransitions)
		{
			if (transition.TransitionCondtion())
				return transition;
		}

		return null;
	}
}
