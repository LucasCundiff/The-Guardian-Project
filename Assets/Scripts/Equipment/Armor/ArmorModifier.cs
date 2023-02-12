using System;
using UnityEngine;

[Serializable]
public class ArmorModifier
{
	public float ModifierValue;
	public StatModType ModifierType;
	public string StatName;
	[Range(1, 100)]
	public int ChanceToRoll;

	private int _modifierChanceRoll;

	public void ApplyModifier(CharacterStats user, object source)
	{
		if (user)
		{
			for (int i = 0; i < user.Stats.Count; i++)
			{
				if (user.Stats[i].StatName == StatName)
					user.Stats[i].AddModifier(new StatModifier(ModifierValue, ModifierType, source));
			}
		}
	}

	public void RemoveModifier(CharacterStats user, object source)
	{
		if (user)
		{
			for (int i = 0; i < user.Stats.Count; i++)
			{
				if (user.Stats[i].StatName == StatName)
					user.Stats[i].RemoveAllModifiersFromSource(source);
			}
		}
	}

	public bool CanAddModifier()
	{
		_modifierChanceRoll = UnityEngine.Random.Range(0, 101);

		return ChanceToRoll > _modifierChanceRoll;
	}
}
