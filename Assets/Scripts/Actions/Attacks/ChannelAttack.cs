using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChannelAttack : Attack
{
	[Range(0, 50)]
	public int ManaPerSecondCost;
	[Range(0f, 5f)]
	public float CostIncreasePerSecond;

	protected float channelTime;
	protected bool isChanneling;

	protected float _floatCache;

	public void Update()
	{
		if (isChanneling)
		{
			if (PayAttackCost())
				DuringChannel();
			else
				EndChannel();
		}
	}

	protected virtual void DuringChannel()
	{
		channelTime += Time.deltaTime;
	}

	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		currentAction = action;
		currentUser = user;
		attackInput = input;

		attackInput.performed += StartChannel;
		attackInput.canceled += EndChannel;
	}

	public override void DeinitializeAttack()
	{
		attackInput.performed -= StartChannel;
		attackInput.canceled -= EndChannel;
	}

	private void StartChannel(InputAction.CallbackContext obj) => StartChannel();
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

	private void EndChannel(InputAction.CallbackContext obj) => EndChannel();
	protected virtual void EndChannel()
	{
		isChanneling = false;
		currentAction.ToggleAttackCooldown(false);
		gameObject.SetActive(false);
	}

	public override bool PayAttackCost()
	{
		_floatCache = (ManaPerSecondCost + (CostIncreasePerSecond * (int)channelTime)) * Time.deltaTime;
		if (currentUser && currentUser.CurrentMana >= _floatCache)
		{
			currentUser.CurrentMana -= _floatCache;
			return true;
		}

		return false;
	}
}
