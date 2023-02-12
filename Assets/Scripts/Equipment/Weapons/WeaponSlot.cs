public class WeaponSlot : ItemSlot
{

	public override bool CanRecieveItem(Item item)
	{
		if (item is WeaponItem weaponItem)
			return true;
		else
			return item == null;
	}
}
