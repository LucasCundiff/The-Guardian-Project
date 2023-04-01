using System;
using UnityEngine;

[Serializable]
public class EquipmentModifier
{
	public float ModifierValue;
	public StatModType ModifierType;
	[Range(0, 14)]
	[Tooltip("0. = Health, 1 = Mana, 2 = Stamina, 3 = Melee Proficiency, 4 = Ranged Proficiency, 5 = Mana Proficiency, 6 = Stamina Proficiency, 7 = Attack Speed, 8 = Cooldown Reduction, 9 = Armor, 10 = Resistance," +
		" 11 = Regeneration, 12 = Movement Speed, 13 = Damage Multipler, 14 = Damage Received")]
	public int StatIndex;
	[Range(1, 100)]
	public int ChanceToRoll;

	public void ApplyModifier(CharacterStats user, object source)
	{
		if (user)
			user.Stats[StatIndex].AddModifier(new StatModifier(ModifierValue, ModifierType, source));
	}

	public void RemoveModifier(CharacterStats user, object source)
	{
		if (user)
			user.Stats[StatIndex].RemoveAllModifiersFromSource(source);
	}

	public bool CanAddModifier()
	{
		var modifierChanceRoll = UnityEngine.Random.Range(0, 101);

		return ChanceToRoll > modifierChanceRoll;
	}
}
