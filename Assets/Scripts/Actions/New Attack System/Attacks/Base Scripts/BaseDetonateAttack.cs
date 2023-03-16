using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class BaseDetonateAttack : BaseAttack
{
	[SerializeField] GameObject DetonateObject;


	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		base.InitializeAttack(input, action, user);

		attackInput.performed += DetermineUse;
	}

	private void DetermineUse(InputAction.CallbackContext context)
	{
		if (context.interaction is PressInteraction)
			Debug.Log("Pressed");
		else if (context.interaction is HoldInteraction)
			Debug.Log("Held");
	}
}