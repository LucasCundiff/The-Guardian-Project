public class CharacterInventory : ItemContainer
{ 
	public override bool AddItem(Item item)
	{
		if (!item) return false;

		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item == null)
			{
				itemSlots[i].Item = item;
				return true;
			}
		}

		return false;
	}

	public override bool RemoveItem(Item item)
	{
		if (!item) return false;

		for (int i = 0; i < itemSlots.Count; i++)
		{
			if (itemSlots[i].Item && itemSlots[i].Item.ID == item.ID)
			{
				itemSlots[i].Item = null;
				return true;
			}
		}

		return false;
	}
}
