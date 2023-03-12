using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIStateMachine : MonoBehaviour
{
	[SerializeField] protected AIState currentState;
	[SerializeField] protected CharacterStats user;
	[SerializeField] protected NavMeshAgent navAgent;

	protected CharacterStats target;

	protected virtual void Update()
	{
		currentState = GetNextState();
		RunCurrentState();
	}

	protected virtual AIState GetNextState()
	{
		var newState = GetTranstionFromAnyState();

		if (newState == AIState.None)
			newState = GetTransitionFromCurrentState();

		if (newState != currentState)
		{
			ExitOldState();
			EnterNewState(newState);
		}

		return newState;
	}

	protected abstract void EnterNewState(AIState newState);
	protected abstract void ExitOldState();
	protected abstract void RunCurrentState();
	protected abstract AIState GetTranstionFromAnyState();
	protected abstract AIState GetTransitionFromCurrentState();

	protected virtual float DetermineDistance()
	{
		CharacterStats currentTarget = null;
		var vectorDistance = Vector3.zero;
		var closestDistance = Mathf.Infinity;

		foreach (CharacterStats possibleTarget in CharacterTracker.Instance.AllCharacters)
		{
			if (possibleTarget?.Faction == user?.Faction || possibleTarget?.IsDead == true) continue;

			vectorDistance = possibleTarget.transform.position - transform.position;
			if (vectorDistance.magnitude < closestDistance)
			{
				closestDistance = vectorDistance.magnitude;
				currentTarget = possibleTarget;
			}
		}

		target = currentTarget;
		return closestDistance;
	}
}
