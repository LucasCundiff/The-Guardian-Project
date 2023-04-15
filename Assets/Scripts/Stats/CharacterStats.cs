using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable
{
	public Faction Faction;

	/// <summary>
	/// 0. = Health, 1 = Mana,  2 = Stamina, 3 = Melee Proficiency, 4 = Ranged Proficiency, 5 = Mana Proficiency, 6 = Stamina Proficiency, 7 = Attack Speed, 8 = Cooldown Reduction, 9 = Armor, 10 = Resistance,
	/// 11 = Regeneration, 12 = Movement Speed, 13 = Damage Multipler, 14 = Damage Received
	/// </summary>
	public List<Stat> Stats = new List<Stat>
	{
		new Stat(100f, Mathf.Infinity,"Health", "This is the maximum damage you can take before you die"),
		new Stat(200f, Mathf.Infinity,"Mana", "This is the maximum amount of mana your character can have"),
		new Stat(200f, Mathf.Infinity,"Stamina", "This is the maximum amount of stamina your character can have"),
		new Stat(1f, 500f,"Melee Proficiency", "Each point you acquire will increase the power of your melee attack by 1%"),
		new Stat(1f, 500f,"Ranged Proficiency", "Each point you acquire will increase the power of your ranged attack by 1%"),
		new Stat(1f, 100f,"Mana Proficiency", "Each point you acquire will reduce the cost of your mana attacks by 1%"),
		new Stat(1f, 100f,"Stamina Proficiency", "Each point you acquire will reduce the cost of your stamina attacks by 1%"),
		new Stat(1f, 500f,"Attack Speed", "Each point you acquire will increase the speed of your attacks by 1%"),
		new Stat(1f, 100f,"Cooldown Reduction", "Each point you acquire will reduce your cooldown times by 1%"),
		new Stat(0f, 500f,"Armor", "Each point you acquire will reduce incoming damage by 0.002%"),
		new Stat(0f, 500f,"Resistance", "Decreases the power of negative effects by 0.002% for each point"),
		new Stat(0.5f, 500f,"Regeneration", "Health regenerates by this amount per second, while Mana and Stamina regenerate at three times this rate"),
		new Stat(5f, 50f,"Movement Speed", "This is the speed at which your character moves through the world"),
		new Stat(1f, 100f,"Damage Multiplier", "This is a direct multiplier to your damage"),
		new Stat(1f, 100f,"Damage Received", "This is a direct multiplier to damage you take"),
	};

	public Action<float, float> HealthChangedEvent;
	public Action<float, float> HealthShieldChangedEvent;
	public Action<float, float> ManaChangedEvent;
	public Action<float, float> StaminaChangedEvent;
	public Action OnDeathEvent;

	public float MaxHealth { get; private set; }
	public float MaxMana { get; private set; }
	public float MaxStamina { get; private set; }

	protected float currentHealth;
	public float CurrentHealth
	{
		get { return currentHealth; }
		set
		{
			currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
			HealthChangedEvent?.Invoke(currentHealth, MaxHealth);

			if (!isRegenerating)
				StartCoroutine(RegenerateStats());
		}
	}

	protected float healthShield;
	public float HealthShield
	{
		get { return healthShield; }
		set
		{
			healthShield = Mathf.Clamp(value, 0f, MaxHealth);
			HealthShieldChangedEvent?.Invoke(healthShield, MaxHealth);

			if (!isRegenerating)
				StartCoroutine(RegenerateStats());
		}
	}

	protected float currentMana;
	public float CurrentMana
	{
		get { return currentMana; }
		set
		{
			currentMana = Mathf.Clamp(value, 0f, MaxMana);
			ManaChangedEvent?.Invoke(currentMana, MaxMana);

			if (!isRegenerating)
				StartCoroutine(RegenerateStats());
		}
	}

	protected float currentStamina;
	public float CurrentStamina
	{
		get { return currentStamina; }
		set
		{
			currentStamina = Mathf.Clamp(value, 0f, MaxStamina);
			StaminaChangedEvent?.Invoke(currentStamina, MaxStamina);

			if (!isRegenerating)
				StartCoroutine(RegenerateStats());
		}
	}

	public bool IsDead = false;
	protected bool isRegenerating;
	protected float resourceRegenerationMuliplier = 3;

	protected void OnEnable() => CharacterTracker.Instance?.RegisterCharacterToTracker(this);
	protected void OnDisable() => CharacterTracker.Instance?.UnregisterCharacterToTracker(this);
	protected void OnDestroy() => CharacterTracker.Instance?.UnregisterCharacterToTracker(this);

	public void Awake()
	{
		Stats[0].StatModifiedEvent += HealthStatChanged;
		Stats[1].StatModifiedEvent += ManaStatChanged;
		Stats[2].StatModifiedEvent += StaminaStatChanged;

		MaxHealth = Stats[0].CurrentValue;
		MaxMana = Stats[1].CurrentValue;
		MaxStamina = Stats[2].CurrentValue;

		HealthShield = 0f;
		CurrentHealth = MaxHealth;
		CurrentMana = MaxMana;
		CurrentStamina = MaxStamina;
	}

	protected virtual IEnumerator RegenerateStats()
	{
		isRegenerating = true;

		while (CurrentHealth < MaxHealth || CurrentMana < MaxMana || CurrentStamina < MaxStamina)
		{
			if (IsDead) break;

			var regenerationAmount = Mathf.Clamp(Stats[11].CurrentValue, 0, Stats[11].CurrentValue);

			if (CurrentHealth < MaxHealth)
				CurrentHealth += regenerationAmount;

			if (CurrentMana < MaxMana)
				CurrentMana += regenerationAmount * resourceRegenerationMuliplier;

			if (CurrentStamina < MaxStamina)
				CurrentStamina += regenerationAmount * resourceRegenerationMuliplier;

			yield return new WaitForSeconds(1f);
		}

		isRegenerating = false;

	}

	public void HealthStatChanged(Stat healthStat)
	{
		var healthChangeAmount = healthStat.CurrentValue - MaxHealth;
		MaxHealth = healthStat.CurrentValue;
		CurrentHealth += healthChangeAmount;
	}

	public void ManaStatChanged(Stat manaStat)
	{
		var manaChangeAmount = manaStat.CurrentValue - MaxMana;
		MaxMana = manaStat.CurrentValue;
		CurrentMana += manaChangeAmount;
	}

	public void StaminaStatChanged(Stat staminaStat)
	{
		var staminaChangeAmount = staminaStat.CurrentValue - MaxStamina;
		MaxStamina = staminaStat.CurrentValue;
		CurrentStamina += staminaChangeAmount;
	}

	public bool TakeDamage(float damage)
	{
		if (damage > 0)
		{
			var damageToTake = damage * Stats[14].CurrentValue;

			if (HealthShield > 0)
			{
				damageToTake -= healthShield;
				HealthShield -= damage;
			}

			CurrentHealth -= damageToTake;

			if (CurrentHealth <= 0)
			{
				Die();
			}

			return true;
		}

		return false;
	}

	[Obsolete]
	public bool Heal(float healAmount)
	{
		if (healAmount > 0)
		{
			CurrentHealth += healAmount;
			return true;
		}

		return false;
	}

	public virtual void Die()
	{
		if (!IsDead)
		{
			IsDead = true;
			OnDeathEvent?.Invoke();
		}
	}

}
