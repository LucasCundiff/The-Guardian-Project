using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : Attack
{
	[Range(0, 50)]
	public float StaminaCost;
	[Range(0.5f, 10f)]
	public float DamageMultiplier;
	[Range(0, 100)]
	public int ElementalProcChance;
	public List<StatusEffect> StatusEffects;

	protected float currentDamage;
	protected IDamageable currentTarget;

	protected int _intCache;

	public override void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		currentAction = action;
		currentUser = user;
		attackInput = input;
		input.performed += UseAttack;
	}

	public override void DeinitializeAttack()
	{
		attackInput.performed -= UseAttack;
	}

	public void UseAttack(InputAction.CallbackContext context)
	{
		if (currentAction && !currentAction.IsAttacking && PayAttackCost())
		{
			gameObject.SetActive(true);
			currentAction.StartCoroutine(currentAction.StartAttackCooldown(AttackTime));
			StartCoroutine(DisableGameobject());
		}
	}

	private IEnumerator DisableGameobject()
	{
		yield return new WaitForSeconds(0.2f);

		gameObject.SetActive(false);
	}

	public override bool PayAttackCost()
	{
		if (currentUser && currentUser.CurrentStamina >= StaminaCost)
		{
			currentUser.CurrentStamina -= StaminaCost;
			return true;
		}

		return false;
	}

	private void OnTriggerEnter(Collider other)
	{
		currentTarget = other.GetComponent<IDamageable>();
		var characterTarget = (CharacterStats)currentTarget;

		if (currentTarget != null && characterTarget == null)
			DealDamage();
		else if (characterTarget && characterTarget.Faction != currentUser.Faction)
			DealDamage();
	}

	private void DealDamage()
	{
		CalculateDamage();
		RollElementalEffect();
		currentTarget.TakeDamage(currentDamage);
	}

	protected virtual void CalculateDamage()
	{
		currentDamage = currentAction.Damage * DamageMultiplier;

		if (Random.Range(0, 101) <= currentAction.CriticalChance)
		{
			currentDamage *= currentAction.CriticalMultiplier;
		}

		if (currentTarget is CharacterStats character)
			currentDamage -= currentDamage * character.Stats[7].CurrentValue * .01f;
	}

	private void RollElementalEffect()
	{
		_intCache = Random.Range(0, 101);

		if (currentTarget is CharacterStats character)
			_intCache += (int)character.Stats[8].CurrentValue;

		if (_intCache <= ElementalProcChance)
		{
			foreach (StatusEffect effect in StatusEffects)
			{
				effect.InitializeEffect(currentTarget, currentDamage, currentUser);
			}
		}
	}
}
