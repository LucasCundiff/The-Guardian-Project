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
		input.started += UseAttack;
	}

	private void UseAttack(InputAction.CallbackContext obj)
	{
		if (currentAction && !currentAction.IsAttacking && HasAttackPrerequisite())
		{
			UseAttackPrerequisites();
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
		currentAction.StartCoroutine(currentAction.StartAttackCooldown(GetAttackSpeed()));

		yield return new WaitForSeconds(0.2f);
		gameObject.SetActive(false);
	}
}
