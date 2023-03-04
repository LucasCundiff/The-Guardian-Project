using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseInstantAttack : BaseAttack
{
	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		base.InitializeAttack(input, action, user);
		input.performed += UseAttack;
	}

	private void UseAttack(InputAction.CallbackContext obj)
	{
		if (currentAction && !currentAction.IsAttacking && CanPayAttackCost())
		{
			Debug.Log("using new attack");
			PayAttackCost();
			StartAttack();
			StartCoroutine(EndAttack());
		}

	}

	protected virtual void StartAttack()
	{
		gameObject.SetActive(true);
	}

	protected virtual IEnumerator EndAttack()
	{
		currentAction.StartCoroutine(currentAction.StartAttackCooldown(attackDuration));

		yield return new WaitForSeconds(0.2f);
		gameObject.SetActive(false);
	}
}
