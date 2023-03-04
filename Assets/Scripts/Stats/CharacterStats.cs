using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable
{
	public Faction Faction;

	/// <summary>
	/// 0. = Health, 1 = Mana,  2 = Stamina, 3 = Melee Proficiency, 4 = Ranged Proficiency, 5 = Mana Proficiency, 6 = Stamina Proficiency, 7 = Armor, 8 = Resistance, 9 = Regeneration, 10 = Movement Speed
	/// </summary>
	public List<Stat> Stats = new List<Stat>
	{
		new Stat(100f, "Health"),
		new Stat(200f, "Mana"),
		new Stat(200f, "Stamina"),
		new Stat(5f, "Melee Proficiency"),
		new Stat(5f, "Ranged Proficiency"),
		new Stat(5f, "Mana Proficiency"),
		new Stat(5f, "Stamina Proficiency"),
		new Stat(0f, "Armor"),
		new Stat(0f, "Resistance"),
		new Stat(0.5f, "Regeneration"),
		new Stat(5f, "Movement Speed"),
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

	protected float _floatCache;

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

			_floatCache = Mathf.Clamp(Stats[9].CurrentValue, 0, Stats[9].CurrentValue);

			if (CurrentHealth < MaxHealth)
				CurrentHealth += _floatCache;

			if (CurrentMana < MaxMana)
				CurrentMana += _floatCache * 2;

			if (CurrentStamina < MaxStamina)
				CurrentStamina += _floatCache * 2;

			yield return new WaitForSeconds(1f);
		}

		isRegenerating = false;

	}

	public void HealthStatChanged(Stat healthStat)
	{
		_floatCache = healthStat.CurrentValue - MaxHealth;
		MaxHealth = healthStat.CurrentValue;
		CurrentHealth += _floatCache;
	}

	public void ManaStatChanged(Stat manaStat)
	{
		_floatCache = manaStat.CurrentValue - MaxMana;
		MaxMana = manaStat.CurrentValue;
		CurrentMana += _floatCache;
	}

	public void StaminaStatChanged(Stat staminaStat)
	{
		_floatCache = staminaStat.CurrentValue - MaxStamina;
		MaxStamina = staminaStat.CurrentValue;
		CurrentStamina += _floatCache;
	}

	public bool TakeDamage(float damage)
	{
		if (damage > 0)
		{
			float damageToTake = damage;

			if (HealthShield > 0)
			{
				damageToTake -= healthShield;
				HealthShield -= damage;
			}

			damageToTake = Mathf.Clamp(damageToTake - damageToTake * Stats[7].CurrentValue * 0.002f, 1f, Mathf.Infinity);
			CurrentHealth -= damage;

			if (currentHealth <= 0)
				Die();

			return true;
		}

		return false;
	}

	public virtual void Die()
	{
		IsDead = true;
		OnDeathEvent?.Invoke();
	}
}
