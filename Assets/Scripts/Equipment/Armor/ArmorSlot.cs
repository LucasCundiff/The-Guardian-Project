public class ArmorSlot : ItemSlot
{
	public ArmorType ArmorType;

	public override bool CanRecieveItem(Item item)
	{
		if (item is ArmorItem armorItem)
			return armorItem.ArmorType == ArmorType;
		else
			return item == null;
	}
}
