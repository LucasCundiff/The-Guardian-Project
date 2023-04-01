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
		new Stat(100f, Mathf.Infinity,"Health", "The total amount of damage you can take before dying"),
		new Stat(200f, Mathf.Infinity,"Mana", "Used to pay mana cost of certain attacks"),
		new Stat(200f, Mathf.Infinity,"Stamina", "Used to pay stamina cost of certain attacks"),
		new Stat(1f, 50f,"Melee Proficiency", "A direct multipler to the power of melee attacks"),
		new Stat(1f, 50f,"Ranged Proficiency", "A direct multipler to the power of ranged attacks"),
		new Stat(1f, 50f,"Mana Proficiency", "Divides the cost of mana skills by this amount"),
		new Stat(1f, 50f,"Stamina Proficiency", "Divides the cost of stamina skills by this amount"),
		new Stat(1f, 5f,"Attack Speed", "Divides the time it takes for an attack by this amount"),
		new Stat(1f, 5f,"Cooldown Reduction", "A direct multipler to your cooldown times"),
		new Stat(0f, 500f,"Armor", "Decreases incoming damage by 0.002% for each point"),
		new Stat(0f, 500f,"Resistance", "Decreases the power of hostile attack effects by 0.002% for each point"),
		new Stat(0.5f, 50f,"Regeneration", "Regenerates Health by this amount per second, 3x as much for Mana and Stamina"),
		new Stat(5f, 50f,"Movement Speed", "The speed at which you move through the world"),
		new Stat(1f, 100f,"Damage Multipler", "A direct multipler to your outgoing damage"),
		new Stat(1f, 100f,"Damage Received", "A direct multipler to incoming damage"),
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
				CurrentMana += regenerationAmount * 2;

			if (CurrentStamina < MaxStamina)
				CurrentStamina += regenerationAmount * 2;

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
			var damageToTake = damage;

			if (HealthShield > 0)
			{
				damageToTake -= healthShield;
				HealthShield -= damage;
			}

			CurrentHealth -= damageToTake;

			if (CurrentHealth <= 0)
				Die();

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
