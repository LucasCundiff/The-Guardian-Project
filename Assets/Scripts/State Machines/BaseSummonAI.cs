using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BaseSummonAI : AIStateMachine
{
	[SerializeField] protected float targetChaseDistance;
	[SerializeField] protected float targetAttackDistance;
	[SerializeField] protected List<EnemyAttackTableData> attackTable = new List<EnemyAttackTableData>();

	protected CharacterStats followCharacter;
	protected Vector3 previousFollowCharacterPosition;
	protected bool isAttacking;
	protected Vector3 followOffset;
	protected float zFollowDistance = -3f;
	protected Vector2 xFollowDistanceRange = new Vector2(-4f, 4f);
	protected float catchupDistance = 25f;

	protected void Start()
	{
		var xOffset = Random.Range(xFollowDistanceRange.x, xFollowDistanceRange.y);

		followOffset = new Vector3(xOffset, 0, zFollowDistance);
	}

	public void SetNewSummonCharacter(CharacterStats newCharacter) => followCharacter = newCharacter;

	protected override void EnterNewState(AIState newState)
	{
		switch (currentState)
		{
			case AIState.Follow:
				break;
			case AIState.Chase:
				break;
			case AIState.Attack:
				break;
			default:
				break;
		}
	}

	protected override void ExitOldState()
	{
		switch (currentState)
		{
			case AIState.Follow:
				break;
			case AIState.Chase:
				navAgent.destination = transform.position;
				break;
			case AIState.Attack:
				break;
			default:
				break;
		}
	}

	protected override AIState GetTransitionFromCurrentState()
	{
		switch (currentState)
		{
			case AIState.Idle:
				return IdleStateTransition();
			case AIState.Follow:
				return FollowStateTransition();
			default:
				return AIState.Idle;
		}
	}

	protected AIState IdleStateTransition()
	{
		if (FollowTargetHasMoved())
			return AIState.Follow;

		return AIState.Idle;
	}

	protected AIState FollowStateTransition()
	{
		if (!FollowTargetHasMoved() && navAgent.path.status == NavMeshPathStatus.PathComplete)
			return AIState.Idle;

		return AIState.Follow;
	}

	protected bool FollowTargetHasMoved()
	{
		return previousFollowCharacterPosition != followCharacter.transform.position;
	}

	protected override AIState GetTranstionFromAnyState()
	{
		if (CatchupAnyTransition()) return AIState.Catchup;
		if (AttackAnyTransition()) return AIState.Attack;
		if (ChaseAnyTransition()) return AIState.Chase;
		return AIState.None;
	}

	protected bool CatchupAnyTransition()
	{
		var currentDistance = followCharacter.transform.position - transform.position;

		return currentDistance.magnitude > catchupDistance;
	}

	protected bool ChaseAnyTransition()
	{
		return DetermineDistance() < targetChaseDistance;
	}

	protected bool AttackAnyTransition()
	{
		return DetermineDistance() < targetAttackDistance || isAttacking;
	}

	protected override void RunCurrentState()
	{
		switch (currentState)
		{
			case AIState.Follow:
				RunFollowState();
				break;
			case AIState.Catchup:
				RunCatchupState();
				break;
			case AIState.Chase:
				RunChaseState();
				break;
			case AIState.Attack:
				RunAttackState();
				break;
			default:
				RunFollowState();
				break;
		}
	}

	protected void RunCatchupState()
	{
		var offset = followCharacter.transform.InverseTransformDirection(followOffset);

		if (NavMesh.SamplePosition(followCharacter.transform.position + offset, out var navMeshHit, catchupDistance, NavMesh.AllAreas))
			transform.position = navMeshHit.position;
	}

	protected void RunFollowState()
	{
		previousFollowCharacterPosition = followCharacter.transform.position;
		var offset = followCharacter.transform.InverseTransformDirection(followOffset);

		//Add ways to avoid other characters in the scene
		navAgent.SetDestination(followCharacter.transform.position + offset);
	}

	protected void RunChaseState()
	{
		navAgent.SetDestination(target.transform.position);
	}

	protected void RunAttackState()
	{
		if (target) transform.LookAt(target.transform);
		if (!isAttacking)
		{
			var currentAttack = GetNextAttack();

			if (currentAttack)
			{
				currentAttack.UseAttack();
				StartCoroutine(AttackCooldown(currentAttack.AttackTime));
			}
		}
	}

	protected virtual EnemyAttack GetNextAttack()
	{
		//Rename enemy stuff to AI 
		return WeightTable.RunEnemyAttackWeightTable(attackTable);
	}

	protected IEnumerator AttackCooldown(float cooldownTime)
	{
		isAttacking = true;

		yield return new WaitForSeconds(cooldownTime);

		isAttacking = false;
	}
}
