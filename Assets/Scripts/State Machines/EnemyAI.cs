using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;

public enum EnemyState
{
	Idle,
	Searching,
	Wander,
	Chase,
	Attack,
	Dead,
	None,
}

public class EnemyAI : MonoBehaviour
{
	[SerializeField] protected EnemyState currentState;
	[SerializeField] protected CharacterStats user;
	[SerializeField] protected NavMeshAgent navAgent;
	[SerializeField] protected MeshRenderer meshRenderer;
	[SerializeField] protected Material aliveMaterial, deadMaterial;
	[SerializeField] protected float maxIdleTime;
	[SerializeField] protected float wanderRadius;
	[SerializeField] protected float targetChaseDistance;
	[SerializeField] protected float targetAttackDistance;
	[SerializeField] protected List<EnemyAttackTableData> attackTable = new List<EnemyAttackTableData>();

	protected float currentIdleTime;
	protected float currentMaxIdleTime;
	protected bool isAttacking;
	protected CharacterStats target;

	protected virtual void Start()
	{
		if (!user)
			user = GetComponent<CharacterStats>();

		if (!navAgent)
			navAgent = GetComponent<NavMeshAgent>();

		if (!meshRenderer)
			meshRenderer = GetComponent<MeshRenderer>();

		foreach (EnemyAttackTableData tableData in attackTable)
		{
			tableData.Attack.InitializeAttack(user);
		}
	}

	protected virtual void Update()
	{
		currentState = GetNextState();
		RunCurrentState();
	}

	protected virtual void EnterNewState(EnemyState newState)
	{
		switch (newState)
		{
			case EnemyState.Idle:
				currentMaxIdleTime = Random.Range(1, maxIdleTime);
				break;
			case EnemyState.Searching:
				break;
			case EnemyState.Wander:
				break;
			case EnemyState.Chase:
				break;
			case EnemyState.Attack:
				break;
			case EnemyState.Dead:
				meshRenderer.material = deadMaterial;
				break;
			case EnemyState.None:
				break;
			default:
				break;
		}
	}

	protected virtual void ExitOldState()
	{
		switch (currentState)
		{
			case EnemyState.Idle:
				currentIdleTime = 0f;
				break;
			case EnemyState.Searching:
				break;
			case EnemyState.Wander:
				break;
			case EnemyState.Chase:
				navAgent.destination = transform.position;
				break;
			case EnemyState.Attack:
				break;
			case EnemyState.Dead:
				meshRenderer.material = aliveMaterial;
				break;
			case EnemyState.None:
				break;
			default:
				break;
		}
	}

	protected virtual EnemyState GetNextState()
	{
		var newState = GetTranstionFromAnyState();

		if (newState == EnemyState.None)
			newState = GetTransitionFromCurrentState();

		if (newState != currentState)
		{
			ExitOldState();
			EnterNewState(newState);
		}

		return newState;
	}

	protected virtual void RunCurrentState()
	{
		switch (currentState)
		{
			case EnemyState.Idle:
				RunIdleState();
				break;
			case EnemyState.Searching:
				RunSearchState();
				break;
			case EnemyState.Wander:
				RunWanderState();
				break;
			case EnemyState.Chase:
				RunChaseState();
				break;
			case EnemyState.Attack:
				RunAttackState();
				break;
			case EnemyState.None:
				break;
			default:
				break;
		}
	}

	protected virtual void RunIdleState()
	{
		currentIdleTime += Time.deltaTime;
	}

	protected virtual void RunSearchState()
	{
		var radius = Random.Range(1, wanderRadius);
		var randomDirection = Random.insideUnitSphere * radius;
		NavMeshHit hit;
		NavMesh.SamplePosition(transform.position + randomDirection, out hit, wanderRadius, NavMesh.AllAreas);
		navAgent.SetDestination(hit.position);
	}

	protected virtual void RunWanderState()
	{

	}

	protected virtual void RunChaseState()
	{
		navAgent.SetDestination(target.transform.position);
	}

	protected virtual void RunAttackState()
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

	protected virtual void RunDeadState()
	{

	}

	protected virtual EnemyAttack GetNextAttack()
	{
		return WeightTable.RunEnemyAttackWeightTable(attackTable);
	}

	protected IEnumerator AttackCooldown(float cooldownTime)
	{
		isAttacking = true;

		yield return new WaitForSeconds(cooldownTime);

		isAttacking = false;
	}

	protected virtual EnemyState GetTranstionFromAnyState()
	{
		if (DeadAnyTransition()) return EnemyState.Dead;
		if (AttackAnyTransition()) return EnemyState.Attack;
		if (ChaseAnyTransition()) return EnemyState.Chase;
		return EnemyState.None;
	}

	protected virtual EnemyState GetTransitionFromCurrentState()
	{
		switch (currentState)
		{
			case EnemyState.Idle:
				return IdleStateTransition();

			case EnemyState.Wander:
				return WanderStateTransition();

			case EnemyState.Searching:
				return SearchStateTransition();

			default:
				return EnemyState.Idle;
		}
	}

	protected virtual EnemyState IdleStateTransition()
	{
		if (currentIdleTime > currentMaxIdleTime)
			return EnemyState.Searching;

		return EnemyState.Idle;
	}

	protected virtual EnemyState SearchStateTransition()
	{
		if (navAgent.hasPath)
			return EnemyState.Wander;
		else
			return EnemyState.Searching;
	}

	protected virtual EnemyState WanderStateTransition()
	{
		if (navAgent.isPathStale || !navAgent.hasPath)
			return EnemyState.Idle;

		return EnemyState.Wander;
	}

	protected virtual bool ChaseAnyTransition()
	{
		return DetermineDistance() < targetChaseDistance;
	}

	protected virtual bool AttackAnyTransition()
	{
		return DetermineDistance() < targetAttackDistance || isAttacking;
	}

	protected virtual bool DeadAnyTransition()
	{
		return user.IsDead;
	}

	protected float DetermineDistance()
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
