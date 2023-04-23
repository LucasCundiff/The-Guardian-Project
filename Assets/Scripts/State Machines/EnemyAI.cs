using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;


public class EnemyAI : AIStateMachine
{
	[SerializeField] protected MeshRenderer meshRenderer;
	[SerializeField] protected Rigidbody rb;
	[SerializeField] protected Material aliveMaterial, deadMaterial;
	[SerializeField] protected float maxIdleTime;
	[SerializeField] protected float wanderRadius;
	[SerializeField] protected float targetChaseDistance;
	[SerializeField] protected float targetAttackDistance;
	[SerializeField] protected List<EnemyAttackTableData> attackTable = new List<EnemyAttackTableData>();

	protected float currentIdleTime;
	protected float currentMaxIdleTime;
	protected bool isAttacking;

	protected virtual void Start()
	{
		if (!user)
			user = GetComponent<CharacterStats>();

		if (!navAgent)
			navAgent = GetComponent<NavMeshAgent>();

		if (!meshRenderer)
			meshRenderer = GetComponent<MeshRenderer>();

		if (!rb)
			rb = GetComponent<Rigidbody>();

		foreach (EnemyAttackTableData tableData in attackTable)
		{
			tableData.Attack.InitializeAttack(user);
		}
	}

	protected override void Update()
	{
		currentState = GetNextState();
		RunCurrentState();
	}

	protected override void EnterNewState(AIState newState)
	{
		switch (newState)
		{
			case AIState.Idle:
				currentMaxIdleTime = Random.Range(1, maxIdleTime);
				break;
			case AIState.Searching:
				break;
			case AIState.Wander:
				break;
			case AIState.Chase:
				break;
			case AIState.Attack:
				break;
			case AIState.Dead:
				meshRenderer.material = deadMaterial;
				navAgent.isStopped = true;
				break;
			case AIState.None:
				break;
			default:
				break;
		}
	}

	protected override void ExitOldState()
	{
		switch (currentState)
		{
			case AIState.Idle:
				currentIdleTime = 0f;
				break;
			case AIState.Searching:
				break;
			case AIState.Wander:
				break;
			case AIState.Chase:
				navAgent.destination = transform.position;
				break;
			case AIState.Attack:
				break;
			case AIState.Dead:
				meshRenderer.material = aliveMaterial;
				navAgent.isStopped = false;
				break;
			case AIState.None:
				break;
			default:
				break;
		}
	}

	protected override AIState GetNextState()
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

	protected override void RunCurrentState()
	{
		switch (currentState)
		{
			case AIState.Idle:
				RunIdleState();
				break;
			case AIState.Searching:
				RunSearchState();
				break;
			case AIState.Wander:
				RunWanderState();
				break;
			case AIState.Chase:
				RunChaseState();
				break;
			case AIState.Attack:
				RunAttackState();
				break;
			case AIState.None:
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

	protected virtual void RunWanderState() { }

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

	protected virtual void RunDeadState() { }

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

	protected override AIState GetTranstionFromAnyState()
	{
		if (DeadAnyTransition()) return AIState.Dead;
		if (AttackAnyTransition()) return AIState.Attack;
		if (ChaseAnyTransition()) return AIState.Chase;
		return AIState.None;
	}

	protected override AIState GetTransitionFromCurrentState()
	{
		switch (currentState)
		{
			case AIState.Idle:
				return IdleStateTransition();

			case AIState.Wander:
				return WanderStateTransition();

			case AIState.Searching:
				return SearchStateTransition();

			default:
				return AIState.Idle;
		}
	}

	protected virtual AIState IdleStateTransition()
	{
		if (currentIdleTime > currentMaxIdleTime)
			return AIState.Searching;

		return AIState.Idle;
	}

	protected virtual AIState SearchStateTransition()
	{
		if (navAgent.hasPath)
			return AIState.Wander;
		else
			return AIState.Searching;
	}

	protected virtual AIState WanderStateTransition()
	{
		if (navAgent.isPathStale || !navAgent.hasPath)
			return AIState.Idle;

		return AIState.Wander;
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
}
