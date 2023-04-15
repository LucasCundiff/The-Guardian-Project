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
			if (HasAttackPrerequisite())
			{
				UseAttackPrerequisites();
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

	public override float DeterminePower()
	{
		var attackPower = base.DeterminePower();
		var channelPowerIncrease = 1 + PowerMultiplierPerSecond * channelTime;
		attackPower *= channelPowerIncrease * (1 + (User.Stats[7].CurrentValue * 0.01f))* Time.deltaTime;
		return attackPower;
	}

	public override float GetHealthCost()
	{
		if (HealthCost == 0) return 0f;

		return (HealthCost + (CostIncreasePerSecond * (int)channelTime)) * Time.deltaTime * (1 + (User.Stats[7].CurrentValue * 0.01f));
	}

	public override float GetManaCost()
	{
		if (ManaCost == 0) return 0f;

		return (ManaCost + (CostIncreasePerSecond * (int)channelTime)) * (1 - (User.Stats[5].CurrentValue * 0.01f)) * Time.deltaTime * (1 + (User.Stats[7].CurrentValue * 0.01f));
	}

	public override float GetStaminaCost()
	{
		if (StaminaCost == 0) return 0f;

		return (StaminaCost + (CostIncreasePerSecond * (int)channelTime)) * (1 - (User.Stats[6].CurrentValue * 0.01f)) * Time.deltaTime * (1 + (User.Stats[7].CurrentValue * 0.01f));
	}
}
