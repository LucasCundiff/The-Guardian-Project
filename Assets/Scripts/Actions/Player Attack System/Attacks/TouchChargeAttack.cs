using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchChargeAttack : BaseChargeAttack
{
	[SerializeField] BoxCollider attackCollider;
	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		base.InitializeAttack(input, action, user);
		attackCollider.enabled = false;
	}

	protected override IEnumerator ExecuteCharge()
	{
		attackCollider.enabled = true;

		yield return new WaitForSeconds(0.2f);

		currentAction.StartCoroutine(currentAction.StartAttackCooldown(GetAttackSpeed()));
		attackCollider.enabled = false;
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		PossibleTargetHit(other);
	}
}
