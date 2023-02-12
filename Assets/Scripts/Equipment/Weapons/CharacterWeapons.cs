using System;

public class CharacterWeapons : ItemContainer
{
	public Action<Item> OnWeaponEquippedEvent;
	public Action<Item> OnWeaponUnequippedEvent;

	protected WeaponItem _weaponItem;

	public override bool AddItem(Item item)
	{
		_weaponItem = (WeaponItem)item;

		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item == null && _weaponItem && itemSlots[i].CanRecieveItem(_weaponItem))
			{
				itemSlots[i].Item = _weaponItem;
				OnWeaponEquippedEvent?.Invoke(_weaponItem);

				return true;
			}
		}

		return false;
	}

	public override bool ReplaceItem(Item item, out Item previousItem)
	{
		for (int i = 0; i < itemSlots.Count; i++)
		{
			_weaponItem = (WeaponItem)item;

			if (_weaponItem && itemSlots[i].CanRecieveItem(_weaponItem))
			{
				previousItem = itemSlots[i].Item;

				if (previousItem)
					OnWeaponUnequippedEvent?.Invoke(previousItem);

				itemSlots[i].Item = _weaponItem;
				OnWeaponEquippedEvent?.Invoke(_weaponItem);
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
				_weaponItem = (WeaponItem)itemSlots[i].Item;
				itemSlots[i].Item = null;
				OnWeaponUnequippedEvent?.Invoke(_weaponItem);
				return true;
			}
		}

		return false;
	}
}
