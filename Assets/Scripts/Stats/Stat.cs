using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
	public string StatName;
	public float baseValue;
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

	private bool _foundSource;
	private float _finalValue;
	private float _sumOfPercentAddModifiers;

	public Stat(float baseValue, string statName)
	{
		StatName = statName;
		BaseValue = baseValue;
}

	private float CalculateFinalValue()
	{
		_finalValue = BaseValue;
		_sumOfPercentAddModifiers = 0;

		for (int i = 0; i < StatModifiers.Count; i++)
		{
			StatModifier mod = StatModifiers[i];

			if (mod.Type == StatModType.Flat)
			{
				_finalValue += mod.Value;
			}
			else if (mod.Type == StatModType.PercentAdd)
			{
				_sumOfPercentAddModifiers += mod.Value;

				if (i + 1 >= StatModifiers.Count || StatModifiers[i + 1].Type != StatModType.PercentAdd)
				{
					_finalValue *= 1 + _sumOfPercentAddModifiers * 0.01f;
					_sumOfPercentAddModifiers = 0;
				}
			}
			else if (mod.Type == StatModType.PercentMult)
			{
				_finalValue *= 1 + mod.Value * 0.01f;
			}
		}

		return Mathf.Round(_finalValue * 100f) / 100f;
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
		_foundSource = false;

		for (int i = StatModifiers.Count - 1; i >= 0; i--)
		{
			if (StatModifiers[i].Source == source)
			{
				hasChanged = true;
				_foundSource = true;
				StatModifiers.RemoveAt(i);
			}
		}

		StatModifiedEvent?.Invoke(this);

		return _foundSource;
	}
}
