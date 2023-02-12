using System;

public class CharacterArmor : ItemContainer
{
	public event Action<Item> OnArmorEquipped;
	public event Action<Item> OnArmorUnequipped;

	protected ArmorItem _armorItem;

	public override bool ReplaceItem(Item item, out Item previousItem)
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			_armorItem = (ArmorItem)item;

			if (_armorItem && itemSlots[i].CanRecieveItem(_armorItem))
			{
				previousItem = itemSlots[i].Item;
				if (previousItem)
					OnArmorUnequipped?.Invoke(previousItem);

				itemSlots[i].Item = _armorItem;
				OnArmorEquipped?.Invoke(_armorItem);
				return true;
			}
		}

		previousItem = null;
		return false;
	}

	public override bool RemoveItem(Item item)
	{
		if (!item) return false;

		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item && itemSlots[i].Item.ID == item.ID)
			{
				_armorItem = (ArmorItem)itemSlots[i].Item;
				OnArmorUnequipped?.Invoke(_armorItem);

				itemSlots[i].Item = null;
				return true;
			}
		}

		return false;
	}
}
