using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effect/Modify Stat")]
public class ModifyStatEffect : ConsumableItemEffect
{
	[Range(10, 120)]
	[SerializeField] float EffectDuration; 
	[Space]
	[SerializeField] float HealthModifyAmount;
	[SerializeField] float ManaModifyAmount;
	[SerializeField] float StaminaModifyAmount;
	[SerializeField] float MeleeProficiencyModifyAmount;
	[SerializeField] float RangedProficiencyModifyAmount;
	[SerializeField] float ManaProficiencyModifyAmount;
	[SerializeField] float StaminaProficiencyModifyAmount;
	[SerializeField] float ArmorModifyAmount;
	[SerializeField] float ResistanceModifyAmount;
	[SerializeField] float RegenerationModifyAmount;
	[SerializeField] float MovementSpeedModifyAmount;

	public override void UseEffect(CharacterStats user)
	{
		if (HealthModifyAmount != 0)
		{
			user.Stats[0].AddModifier(new StatModifier(HealthModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[0]));
		}
		if (ManaModifyAmount != 0)
		{
			user.Stats[1].AddModifier(new StatModifier(ManaModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[1]));
		}
		if (StaminaModifyAmount != 0)
		{
			user.Stats[2].AddModifier(new StatModifier(StaminaModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[2]));
		}
		if (MeleeProficiencyModifyAmount != 0)
		{
			user.Stats[3].AddModifier(new StatModifier(MeleeProficiencyModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[3]));
}
		if (RangedProficiencyModifyAmount != 0)
		{
			user.Stats[4].AddModifier(new StatModifier(RangedProficiencyModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[4]));
}
		if (ManaProficiencyModifyAmount != 0)
		{
			user.Stats[5].AddModifier(new StatModifier(ManaProficiencyModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[5]));
}
		if (StaminaProficiencyModifyAmount != 0)
		{
			user.Stats[6].AddModifier(new StatModifier(StaminaProficiencyModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[6]));
}
		if (ArmorModifyAmount != 0)
		{
			user.Stats[7].AddModifier(new StatModifier(ArmorModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[7]));
}
		if (ResistanceModifyAmount != 0)
		{
			user.Stats[8].AddModifier(new StatModifier(ResistanceModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[8]));
}
		if (RegenerationModifyAmount != 0)
		{
			user.Stats[9].AddModifier(new StatModifier(RegenerationModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[9]));
}
		if (MovementSpeedModifyAmount != 0)
		{
			user.Stats[10].AddModifier(new StatModifier(MovementSpeedModifyAmount, StatModType.Flat, this));
			user.StartCoroutine(RemoveModifier(user.Stats[10]));
		}
	}

	private IEnumerator RemoveModifier(Stat stat)
	{
		yield return new WaitForSeconds(EffectDuration);

		stat.RemoveAllModifiersFromSource(this);
	}
}
