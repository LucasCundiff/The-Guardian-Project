using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseChargeAttack : BaseAttack
{
	[Range(1f, 10f)]
	[SerializeField] protected float RequiredChargeTime;

	protected float chargeTime;
	protected bool isCharging;

	protected void Update()
	{
		DuringCharge();
	}

	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		base.InitializeAttack(input, action, user);

		attackInput.started += StartCharge;
		attackInput.canceled += FailCharge;
	}

	public override void DeinitializeAttack()
	{
		base.DeinitializeAttack();

		attackInput.started -= StartCharge;
		attackInput.canceled -= FailCharge;
	}

	protected virtual void DuringCharge()
	{
		if (isCharging)
		{
			chargeTime += Time.deltaTime;

			if (ChargeComplete())
				CompleteCharge();
		}
	}

	private bool ChargeComplete()
	{
		return chargeTime >= RequiredChargeTime;
	}

	protected void StartCharge(InputAction.CallbackContext obj) => StartCharge();
	protected virtual void StartCharge()
	{
		if (currentAction && !currentAction.IsAttacking && HasAttackPrerequisite())
		{
			UseAttackPrerequisites();

			isCharging = true;
			chargeTime = 0f;
			gameObject.SetActive(true);
		}
	}

	protected void FailCharge(InputAction.CallbackContext obj)
	{
		if (isCharging)
		{
			RefundAttackCost();
			gameObject.SetActive(false);
		}
	}

	protected virtual void CompleteCharge()
	{
		isCharging = false;
		StartCoroutine(ExecuteCharge());
	}

	protected virtual IEnumerator ExecuteCharge()
	{
		yield return new WaitForSeconds(0.2f);

		currentAction.StartCoroutine(currentAction.StartAttackCooldown(GetAttackSpeed()));
		gameObject.SetActive(false);
	}


}
