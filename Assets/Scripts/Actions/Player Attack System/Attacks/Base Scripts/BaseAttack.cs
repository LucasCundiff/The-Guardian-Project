using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseAttack : MonoBehaviour
{
	[Range(-50f, 100f)]
	[SerializeField] protected float StaminaCost = 0f;
	[Range(-50f, 100f)]
	[SerializeField] protected float ManaCost = 0f;
	[Range(-50f, 100f)]
	[SerializeField] protected float HealthCost = 0f;
	[Range(0f, 5f)]
	[SerializeField] protected float defaultAttackSpeed = 1f;
	[Range(0f, 60)]
	[SerializeField] protected float attackCooldown = 0f;
	[Range(0f, 255f)]
	public float Power;
	[SerializeField] protected List<OnHitAttackEffect> onTargetHitEffects = new List<OnHitAttackEffect>();
	public CharacterStats User { get; private set; }
	public List<OnHitAttackEffect> OnTargetHitEffects { get { return onTargetHitEffects; } }
	protected BaseAction currentAction;
	protected InputAction attackInput;

	public virtual void InitializeAttack(InputAction input, BaseAction action, CharacterStats user)
	{
		User = user;
		currentAction = action;
		attackInput = input;
	}

	public virtual void DeinitializeAttack()
	{

	}

	public virtual float DeterminePower()
	{
		var attackPower = currentAction.PowerMultiplier * Power;

		var critRoll = Random.Range(0, 100);
		if (critRoll <= currentAction.CriticalChance)
			attackPower *= currentAction.CriticalMultiplier;

		return attackPower;
	}

	public virtual bool CanUseAttack()
	{
		if (attackCooldown > 0 && PlayerCooldownTracker.Instance.IsAttackOnCooldown(this))
		{
			return false;
		}

		if (User.CurrentStamina < StaminaCost)
			return false;
		if (User.CurrentMana < ManaCost)
			return false;
		if (User.CurrentHealth < HealthCost)
			return false;

		return true;
	}

	public virtual void PayAttackCost()
	{
		if (attackCooldown > 0)
		{
			PlayerCooldownTracker.Instance.StartAttackCooldown(this, attackCooldown * User.Stats[8].CurrentValue);
		}

		User.CurrentStamina -= StaminaCost;
		User.CurrentMana -= ManaCost;
		User.CurrentHealth -= HealthCost;
	}

	public virtual void PossibleTargetHit(Collider colliderHit)
	{
		var target = colliderHit.GetComponent<IDamageable>();

		if (target != null)
		{
			if (target is CharacterStats cTarget && cTarget?.Faction == User.Faction) return;
			var power = DeterminePower();

			foreach (OnHitAttackEffect onHitAttackEffect in OnTargetHitEffects)
			{
				onHitAttackEffect.InitializeEffect(target, power, User);
			}
		}
	}

	public virtual float GetAttackSpeed()
	{
		return defaultAttackSpeed / User.Stats[7].CurrentValue; ;
	}
}

