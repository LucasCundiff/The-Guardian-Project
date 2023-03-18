using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseChannelAttack : BaseAttack
{
	[Range(0f, 10f)]
	public float CostIncreasePerSecond;
	[Range(0f, 1f)]
	public float PowerMultiplierPerSecond;

	protected float channelTime;
	protected bool isChanneling;

	protected float manaPerSecondCostCache;
	protected float staminaPerSecondCostCache;
	protected float healthPerSecondCostCache;

	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		base.InitializeAttack(input, action, user);

		attackInput.started += StartChannel;
		attackInput.canceled += EndChannel;
	}

	public override void DeinitializeAttack()
	{
		base.DeinitializeAttack();

		attackInput.started -= StartChannel;
		attackInput.canceled -= EndChannel;
	}

	public void Update()
	{
		if (isChanneling)
		{
			if (CanPayAttackCost())
			{
				PayAttackCost();
				DuringChannel();
			}
			else
				EndChannel();
		}
	}

	protected virtual void DuringChannel()
	{
		channelTime += Time.deltaTime;
	}

	protected void StartChannel(InputAction.CallbackContext obj) => StartChannel();
	protected virtual void StartChannel()
	{
		if (currentAction && !currentAction.IsAttacking)
		{
			isChanneling = true;
			channelTime = 0f;
			currentAction.ToggleAttackCooldown(true);
			gameObject.SetActive(true);
		}
	}

	protected void EndChannel(InputAction.CallbackContext obj) => EndChannel();
	protected virtual void EndChannel()
	{
		isChanneling = false;
		currentAction.ToggleAttackCooldown(false);
		gameObject.SetActive(false);
	}

	public override void PayAttackCost()
	{
		CurrentUser.CurrentMana -= manaPerSecondCostCache;
		CurrentUser.CurrentStamina -= staminaPerSecondCostCache;
		CurrentUser.CurrentHealth -= healthPerSecondCostCache;
	}

	public override bool CanPayAttackCost()
	{
		if (ManaCost != 0)
		{
			manaPerSecondCostCache = (ManaCost + (CostIncreasePerSecond * (int)channelTime)) * Time.deltaTime;

			if (CurrentUser.CurrentMana < manaPerSecondCostCache)
				return false;
		}

		if (StaminaCost != 0)
		{
			staminaPerSecondCostCache = (StaminaCost + (CostIncreasePerSecond * (int)channelTime)) * Time.deltaTime;

			if (CurrentUser.CurrentStamina < staminaPerSecondCostCache)
				return false;
		}
		
		if (HealthCost != 0)
		{
			healthPerSecondCostCache = (HealthCost + (CostIncreasePerSecond * (int)channelTime)) * Time.deltaTime;

			if (CurrentUser.CurrentHealth < healthPerSecondCostCache)
				return false;
		}

		return true;
	}

	public override float DeterminePower()
	{
		var attackPower = base.DeterminePower();
		attackPower *= 1 + PowerMultiplierPerSecond * channelTime;

		return attackPower *= Time.deltaTime;
	}
}
