using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : EquipmentItem
{
	public BaseAction WeaponObject;

	public override Item GetCopy()
	{
		var weaponItem = (WeaponItem)base.GetCopy();
		weaponItem.GenerateItemDescription();
		return weaponItem;
	}

	public override void GenerateItemDescription()
	{
		if (!WeaponObject) { ItemDescription = ""; return; }

		sb.Clear();

		if (WeaponObject.Primary)
		{
			sb.Append(WeaponObject.Primary.gameObject.name);
			sb.AppendLine();
		}
		if (WeaponObject.Secondary)
		{
			sb.Append(WeaponObject.Secondary.gameObject.name);
			sb.AppendLine();
		}
		if (WeaponObject.Tertiary)
		{
			sb.Append(WeaponObject.Tertiary.gameObject.name);
			sb.AppendLine();
		}
		if (WeaponObject.Quaternary)
			sb.Append(WeaponObject.Quaternary.gameObject.name);

		ItemDescription = sb.ToString();
	}

	public override GameObject GetWorldItem()
	{
		return WeaponObject.gameObject ? WeaponObject.gameObject : null;
	}

	public override void Equip(CharacterStats user)
	{

	}

	public override void Unequip(CharacterStats user)
	{

	}
}
