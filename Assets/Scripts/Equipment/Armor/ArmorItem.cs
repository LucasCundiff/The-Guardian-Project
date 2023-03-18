using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Armor Item")]
public class ArmorItem : EquipmentItem
{
	public List<ArmorModifier> PossibleModifiers = new List<ArmorModifier>();
	public List<ArmorModifier> ActualModifiers = new List<ArmorModifier>();
	public ArmorType ArmorType;

	public override Item GetCopy()
	{
		var armorItem = (ArmorItem)base.GetCopy();

		armorItem.ActualModifiers.Clear();
		for (int i = 0; i < PossibleModifiers.Count; i++)
		{
			if (armorItem.PossibleModifiers[i].CanAddModifier())
				armorItem.ActualModifiers.Add(armorItem.PossibleModifiers[i]);
		}

		armorItem.GenerateItemDescription();

		return armorItem;
	}

	public override void Equip(CharacterStats user)
	{
		foreach (ArmorModifier modifier in ActualModifiers)
			modifier.ApplyModifier(user, this);
	}

	public override void Unequip(CharacterStats user)
	{
		foreach (ArmorModifier modifier in ActualModifiers)
			modifier.RemoveModifier(user, this);
	}

	public override void GenerateItemDescription()
	{
		sb.Clear();

		for (int i = 0; i < ActualModifiers.Count; i++)
		{
			if (ActualModifiers[i].ModifierValue > 0)
				sb.Append("+");

			sb.Append(ActualModifiers[i].ModifierValue);

			if (ActualModifiers[i].ModifierType == StatModType.Flat)
				sb.Append(" ");
			else
				sb.Append("% ");

			sb.Append(ActualModifiers[i].StatName);

			if (sb.Length > 0)
				sb.AppendLine();
		}

		ItemDescription = sb.ToString();
	}
}
