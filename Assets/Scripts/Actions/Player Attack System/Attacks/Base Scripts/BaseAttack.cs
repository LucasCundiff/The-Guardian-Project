using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseAttack : MonoBehaviour
{
	[Range(0f, 255f)]
	[SerializeField] protected float StaminaCost = 0f;
	[Range(0f, 255f)]
	[SerializeField] protected float ManaCost = 0f;
	[Range(0f, 255f)]
	[SerializeField] protected float HealthCost = 0f;
	[Range(0f, 5f)]
	[SerializeField] protected float defaultAttackSpeed = 1f;
	[Range(0f, 60)]
	[SerializeField] protected float attackCooldown = 0f;
	[SerializeField] protected bool UseMeleeProficency;
	[SerializeField] protected bool UseRangedProficency;
	[Range(0f, 255f)]
	public float Power;
	[SerializeField] protected List<OnHitAttackEffect> onTargetHitEffects = new List<OnHitAttackEffect>();
	public List<OnHitAttackEffect> OnTargetHitEffects { get { return onTargetHitEffects; } }
	public CharacterStats User { get; private set; }
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

		if (UseMeleeProficency) attackPower *= 1 + User.Stats[3].CurrentValue * 0.01f;
		if (UseRangedProficency) attackPower *= 1 + User.Stats[4].CurrentValue * 0.01f;

		return attackPower;
	}

	public virtual bool HasAttackPrerequisite()
	{
		if (attackCooldown > 0 && PlayerCooldownTracker.Instance.IsAttackOnCooldown(this))
		{
			return false;
		}

		if (User.CurrentStamina < GetStaminaCost())
			return false;
		if (User.CurrentMana < GetManaCost())
			return false;
		if (User.CurrentHealth < GetHealthCost())
			return false;

		return true;
	}

	public virtual void UseAttackPrerequisites()
	{
		if (attackCooldown > 0)
		{
			PlayerCooldownTracker.Instance.StartAttackCooldown(this, attackCooldown * (1 - (User.Stats[8].CurrentValue * 0.01f)));
		}

		User.CurrentStamina -= GetStaminaCost();
		User.CurrentMana -= GetManaCost();
		User.CurrentHealth -= GetHealthCost();
	}

	public virtual void RefundAttackCost()
	{
		if (attackCooldown > 0)
		{
			PlayerCooldownTracker.Instance.EndCooldown(this);
		}

		User.CurrentStamina += GetStaminaCost();
		User.CurrentMana += GetManaCost();
		User.CurrentHealth += GetHealthCost();
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
		return defaultAttackSpeed / (1 + (User.Stats[7].CurrentValue * 0.01f)) ;
	}

	public virtual float GetHealthCost()
	{
		return HealthCost;
	}

	public virtual float GetStaminaCost()
	{
		return StaminaCost * (1 - (User.Stats[6].CurrentValue * 0.01f));
	}

	public virtual float GetManaCost()
	{
		return ManaCost * (1 - (User.Stats[5].CurrentValue * 0.01f));
	}
}

