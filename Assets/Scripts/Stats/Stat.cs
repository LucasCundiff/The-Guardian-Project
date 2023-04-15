using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
	public string StatName;
	public float StatValueCap;
	public string StatDescription = "";

	[SerializeField] protected float baseValue;
	public float BaseValue
	{
		get { return baseValue; }
		set
		{
			baseValue = value;
			StatModifiedEvent?.Invoke(this);
		}
	}
	public List<StatModifier> StatModifiers { get; private set; } = new List<StatModifier>();

	public Action<Stat> StatModifiedEvent;

	private float currentValue;
	public float CurrentValue
	{
		get
		{
			if (hasChanged || BaseValue != lastBaseValue)
			{
				lastBaseValue = BaseValue;
				currentValue = CalculateFinalValue();
				hasChanged = false;
			}
			return currentValue;
		}
	}

	private bool hasChanged = true;
	private float lastBaseValue = 0;

	public Stat(float baseValue, float valueCap, string statName, string statDescription)
	{
		StatName = statName;
		StatValueCap = valueCap;
		BaseValue = baseValue;
		StatDescription = statDescription;
	}

	private float CalculateFinalValue()
	{
		var finalValue = BaseValue;
		var sumOfPercentAddModifiers = 0f;

		for (int i = 0; i < StatModifiers.Count; i++)
		{
			StatModifier mod = StatModifiers[i];

			if (mod.Type == StatModType.Flat)
			{
				finalValue += mod.Value;
			}
			else if (mod.Type == StatModType.PercentAdd)
			{
				sumOfPercentAddModifiers += mod.Value;

				if (i + 1 >= StatModifiers.Count || StatModifiers[i + 1].Type != StatModType.PercentAdd)
				{
					finalValue *= 1 + sumOfPercentAddModifiers * 0.01f;
					sumOfPercentAddModifiers = 0;
				}
			}
			else if (mod.Type == StatModType.PercentMult)
			{
				finalValue *= 1 + mod.Value * 0.01f;
			}
		}

		return Mathf.Clamp(Mathf.Round(finalValue * 100f) / 100f, -100, StatValueCap);
	}

	private int CompareModifierOrder(StatModifier a, StatModifier b)
	{
		if (a.Order < b.Order)
			return -1;
		else if (a.Order > b.Order)
			return 1;
		return 0;
	}

	public void AddModifier(StatModifier mod)
	{
		if (mod != null)
		{
			hasChanged = true;
			StatModifiers.Add(mod);
			StatModifiers.Sort(CompareModifierOrder);
			StatModifiedEvent?.Invoke(this);
		}
	}

	public bool RemoveModifier(StatModifier mod)
	{
		if (mod != null && StatModifiers.Remove(mod))
		{
			hasChanged = true;
			StatModifiedEvent?.Invoke(this);

			return true;
		}

		return false;
	}

	public bool RemoveAllModifiersFromSource(object source)
	{
		var foundSource = false;

		for (int i = StatModifiers.Count - 1; i >= 0; i--)
		{
			if (StatModifiers[i].Source == source)
			{
				hasChanged = true;
				foundSource = true;
				StatModifiers.RemoveAt(i);
			}
		}

		StatModifiedEvent?.Invoke(this);

		return foundSource;
	}
}
