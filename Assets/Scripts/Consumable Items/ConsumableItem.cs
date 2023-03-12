using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable Item")]
public class ConsumableItem : Item
{
	[SerializeField] List<ConsumableItemEffect> itemEffects = new List<ConsumableItemEffect>();

	public void UseItem(CharacterStats user)
	{
		foreach (ConsumableItemEffect itemEffect in itemEffects)
		{
			itemEffect.UseEffect(user);
		}
	}
}