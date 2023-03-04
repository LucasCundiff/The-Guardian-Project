using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseAttack : MonoBehaviour
{
	[Range(0f, 100f)]
	[SerializeField] protected float StaminaCost;
	[Range(0f, 100f)]
	[SerializeField] protected float ManaCost;
	[Range(0f, 5f)]
	[SerializeField] protected float attackDuration;
	[Range(.05f, 12.5f)]
	[SerializeField] protected float PowerMultiplier;
	[SerializeField] protected List<OnHitAttackEffect> onTargetHitEffect = new List<OnHitAttackEffect>();
	public CharacterStats CurrentUser { get; private set; }
	protected BaseAction currentAction;
	protected InputAction attackInput;

	public virtual void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		CurrentUser = user;
		currentAction = action;
		attackInput = input;
	}

	public virtual void DeinitializeAttack()
	{

	}

	public virtual float DeterminePower()
	{
		var attackPower = currentAction.Power * PowerMultiplier;

		var critRoll = Random.Range(0, 100);
		if (critRoll <= currentAction.CriticalChance)
			attackPower *= currentAction.CriticalMultiplier;

		return attackPower;
	}

	public virtual bool CanPayAttackCost()
	{
		if (CurrentUser.CurrentStamina < StaminaCost)
			return false;
		if (CurrentUser.CurrentMana < ManaCost)
			return false;

		return true;
	}

	public virtual void PayAttackCost()
	{
		CurrentUser.CurrentStamina -= StaminaCost;
		CurrentUser.CurrentMana -= ManaCost;
	}
}
