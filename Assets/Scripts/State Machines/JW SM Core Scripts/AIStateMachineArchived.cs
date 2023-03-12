using System;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachineArchived 
{
	protected Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
	protected List<Transition> currentTransitions = new List<Transition>();
	protected List<Transition> anyTransitions = new List<Transition>();
	protected List<Transition> emptyTranstions = new List<Transition>();

	public Action<IState> OnStateChangedEvent;

	private IState _currentState;
	private IState CurrentState
	{
		get { return _currentState; }
		set
		{
			_currentState = value;
			OnStateChangedEvent?.Invoke(_currentState);
			Debug.Log("Current state is " + _currentState);
		}
	}

	Transition _transitionCache = null;
	List<Transition> _transitionListCache = new List<Transition>();

	public void TickStateMachine()
	{
		_transitionCache = GetNextTransition();

		if (_transitionCache != null)
			SetState(_transitionCache.TargetState);

		CurrentState?.TickState();
	}

	public void SetState(IState newState)
	{
		if (CurrentState == newState) return;

		CurrentState?.OnExitState();
		CurrentState = newState;
		CurrentState?.OnEnterState();

		currentTransitions = transitions[CurrentState.GetType()] != null ? transitions[CurrentState.GetType()] : emptyTranstions;
	}

	public void AddTransition(IState targetState, IState sourceState, Func<bool> condition)
	{
		if (transitions.TryGetValue(sourceState.GetType(), out _transitionListCache) == false)
			transitions[sourceState.GetType()] = new List<Transition>();

		transitions[sourceState.GetType()].Add(new Transition(targetState, condition));
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
